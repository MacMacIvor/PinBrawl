using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject player;

    [Range(0, 1)]
    public int isDistActivOrTime = 0;

    [Range(0, 10000)]
    public float distFromPlayerTillSpawn = 10;

    [Range(0, 10000)]
    public float timerToSpawn = 10;

    [Range(1, 100)]
    public int amountToSpawn = 2;

    [Range(0, 10)]
    public int typeOfEnemyToSpawn = 0;

    public GameObject enemyA;
    public GameObject enemyB;
    public GameObject enemyC;
    public GameObject enemyD;

    static bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (isDistActivOrTime) {
            case 0:
                if (distFromPlayerTillSpawn > Vector3.Distance(player.transform.position, gameObject.transform.position) && spawned == false)
                {
                    for (int i = 0; i < amountToSpawn; i++)
                    {

                        spawnEnemy(i);
                    }
                    Destroy(gameObject);
                }
                break;
            case 1:
                if (timerToSpawn == 0 && spawned == false)
                {
                    for (int i = 0; i < amountToSpawn; i++)
                    {

                        spawnEnemy(i);
                    }
                    Destroy(gameObject);
                }
                else
                {
                    timerToSpawn--;
                }
                break;
        }
        
    }
    void spawnEnemy(int number)
    {
        switch (typeOfEnemyToSpawn)
        {
            case 0:
                GameObject cloneOfObject = Instantiate(enemyA) as GameObject;
                enemyA.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                enemyA.transform.position = gameObject.transform.position + new Vector3(10 * Mathf.Cos((number + 1) * (360 / (number + 1))), 0, 10 * Mathf.Sin((number + 1) * (360 / (number + 1))));
                spawned = true;
                break;
            case 1:
                GameObject cloneOfObject1 = Instantiate(enemyB) as GameObject;
                enemyB.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                enemyB.transform.position = gameObject.transform.position + new Vector3(10 * Mathf.Cos((number + 1) * (360 / (number + 1))), 0, 10 * Mathf.Sin((number + 1) * (360 / (number + 1))));
                spawned = true;
                break;
            case 2:
                GameObject cloneOfObject2 = Instantiate(enemyC) as GameObject;
                enemyC.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                enemyC.transform.position = gameObject.transform.position + new Vector3(10 * Mathf.Cos((number + 1) * (360 / (number + 1))), 0, 10 * Mathf.Sin((number + 1) * (360 / (number + 1))));
                spawned = true;
                break;
            case 3:
                GameObject cloneOfObject3 = Instantiate(enemyD) as GameObject;
                enemyD.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                enemyD.transform.position = gameObject.transform.position + new Vector3(10 * Mathf.Cos((number + 1) * (360 / (number + 1))), 0, 10 * Mathf.Sin((number + 1) * (360 / (number + 1))));
                spawned = true;
                break;
        }
    }
}
