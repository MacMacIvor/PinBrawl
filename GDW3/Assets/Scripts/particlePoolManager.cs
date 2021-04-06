using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlePoolManager : MonoBehaviour
{
    public static particlePoolManager singleton = null;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            return;
        }
        Destroy(this);
    }

    //Pooling the particle holders
    private Vector3 initSpawn = new Vector3(100, 100, 100);

    [Range(0, 100)]
    public int maxParticleSystems = 5;

    public GameObject particleSystemObj;
    static List<GameObject> particleSystems;
    int index = 0;


    // Start is called before the first frame update
    void Start()
    {
        _BuildParticlePool();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void _BuildParticlePool()
    {
        particleSystems = new List<GameObject>();
        for (int i = 0; i < maxParticleSystems; i++)
        {
            GameObject newEnemy = Instantiate(particleSystemObj, this.transform);
            newEnemy.transform.position = initSpawn;
            newEnemy.gameObject.SetActive(false);
            particleSystems.Add(newEnemy);
        }
    }

    public int GetParticle(Vector3 pos)
    {
        bool spawned = false;
        int indexStartedAt = index;
        do
        {

            switch (particleSystems[index].activeInHierarchy)
            {
                case true:
                    index++;
                    break;
                case false:
                    particleSystems[index].transform.position = pos;
                    particleSystems[index].gameObject.SetActive(true);
                    spawned = true;
                    break;
            }

            if (index >= particleSystems.Count)
                index = 0;
            if (index == indexStartedAt)
            {
                //Resources used up so we will just not spawn anything
                spawned = true;
            }
        } while (spawned == false);
        return index;
    }

    public void ResetParticle(int particle)
    {
        
        particleSystems[particle].gameObject.SetActive(false);
    }

    public GameObject getSpecificParticle(int particle)
    {
        return particleSystems[particle];
    }
}
