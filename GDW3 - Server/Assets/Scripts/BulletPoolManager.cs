using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public GameObject player;

    public GameObject bulletSmall;
    public GameObject bulletLarge;

    static List<GameObject> smallBullets;
    static List<GameObject> largeBullets;

    public static BulletPoolManager singleton = null;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            return;
        }
        Destroy(this);
    }

    private Vector3 initSpawn = new Vector3(100, 100, 100);

    [Range(0, 100)]
    public int maxBulletsSmall = 100;
    [Range(0, 100)]
    public int maxBulletsLarge = 50;

    static int indexSmall = 0;

    static int indexLarge = 0;

    // Start is called before the first frame update
    void Start()
    {
       // TODO: add a series of bullets to the Bullet Pool
        _BuildBulletPool();
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    private void _BuildBulletPool()
    {
        
        smallBullets = new List<GameObject>();
        for (int i = 0; i < maxBulletsSmall; i++)
        {
            GameObject newBullet = Instantiate(bulletSmall, this.transform);
            newBullet.transform.position = initSpawn;
            smallBullets.Add(newBullet);
        }

        largeBullets = new List<GameObject>();
        for (int i = 0; i < maxBulletsLarge; i++)
        {
            GameObject newBullet = Instantiate(bulletLarge, this.transform);
            newBullet.transform.position = initSpawn;
            largeBullets.Add(newBullet);
        }
    }

    public GameObject GetBulletSmall(Vector3 pos)
    {
        bool spawned = false;
        int indexStartedAt = indexSmall;
        do
        {
            
            switch(smallBullets[indexSmall].activeInHierarchy)
            {
                case true:
                    indexSmall++;
                    break;
                case false:
                    smallBullets[indexSmall].transform.position = pos;
                    smallBullets[indexSmall].GetComponent<bullet>().changeActive();
                    spawned = true;
                    break;
            }

            if (indexSmall >= smallBullets.Count)
                indexSmall = 0;
            if (indexSmall == indexStartedAt)
            {
                //Resources used up so we will just not spawn anything
                spawned = true;
            }
        } while (spawned == false);
        return smallBullets[indexSmall];
    }


    public void ResetSmallBullet(GameObject bullet)
    {
        for (int i = 0; i < smallBullets.Count; i++)
        {
            if (bullet == smallBullets[i])
            {
                smallBullets[i].GetComponent<bullet>().changeActive();
            }
        }

    }
    public GameObject GetBulletLarge(Vector3 pos)
    {
        bool spawned = false;
        int indexStartedAt = indexLarge;
        do
        {

            switch (largeBullets[indexLarge].activeInHierarchy)
            {
                case true:
                    indexLarge++;
                    break;
                case false:
                    largeBullets[indexLarge].transform.position = pos;
                    largeBullets[indexLarge].GetComponent<LargeBullets>().changeActive();
                    spawned = true;
                    break;
            }

            if (indexLarge >= largeBullets.Count)
                indexLarge = 0;
            if (indexLarge == indexStartedAt)
            {
                //Resources used up so we will just not spawn anything
                spawned = true;
            }
        } while (spawned == false);
        return largeBullets[indexLarge];
    }

    public void ResetLargeBullet(GameObject bullet)
    {
        for (int i = 0; i < largeBullets.Count; i++)
        {
            if (bullet == largeBullets[i])
            {
                largeBullets[i].GetComponent<LargeBullets>().changeActive();
            }
        }
    }

}
