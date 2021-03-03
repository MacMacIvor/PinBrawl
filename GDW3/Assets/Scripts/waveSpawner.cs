using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{

    [Range(0, 100000)]
    public float initSpawnTimer = 30;

    [Range(1, 100)]
    public int amountToSpawn = 2;

    [Range(0, 100000)]
    public float difficultyWithTimeMultiplier = 3;

    [Range(0, 1000)]
    public float difficultyWithTimeMax = 2;

    [Range(0, 1)]
    public float smallShooterChance = 0.4f;

    [Range(0, 1)]
    public float largeShooterChance = 0.2f;
    
    [Range(0, 1)]
    public float smallMeleeChance = 0.3f;
    
    [Range(0, 1)]
    public float bufferChance = 0.1f;

    private float randomNumber = 0.0f;
    private float timePassed = 0;
    private float timeLeftUntilSpawn = 0;
    private int enemiesSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeLeftUntilSpawn = initSpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the timer has hit 0
        if (timeLeftUntilSpawn <= 0 || timePassed == 0)
        {
            randomNumber = Random.Range(0.0f, 100.0f) / 100.0f;
            bool success = false;
            do
            {
                Debug.Log(randomNumber);
                if (randomNumber <= smallShooterChance)
                {
                    spawnEnemy(0);
                    enemiesSpawned++;

                }
                else if (randomNumber <= smallShooterChance + largeShooterChance)
                {
                    spawnEnemy(1);
                    enemiesSpawned++;

                }
                else if (randomNumber <= smallShooterChance + largeShooterChance + smallMeleeChance)
                {
                    spawnEnemy(2);
                    enemiesSpawned++;
                }
                else if (randomNumber <= smallShooterChance + largeShooterChance + smallMeleeChance + bufferChance)
                {
                    spawnEnemy(3);
                    enemiesSpawned++;
                }

                if (enemiesSpawned == amountToSpawn)
                {
                    success = true;
                    enemiesSpawned = 0;
                }
                else
                {
                    randomNumber = Random.Range(0.0f, 100.0f) / 100.0f;
                }
                
            } while (!success);
            timeLeftUntilSpawn = initSpawnTimer;
        }
        else
        {
            timeLeftUntilSpawn -= Time.deltaTime;
        }
        timePassed += Time.deltaTime;
    }


    void spawnEnemy(int number)
    {
        Collider[] overlapObjects;
        switch (number)
        {
            case 0:
                GameObject temp = EnemyPoolManager.singleton.GetSmallShooter(gameObject.transform.position);
                Vector3 savedPosition = temp.gameObject.transform.position + new Vector3(0, 1, 0);
                do
                {
                    temp.gameObject.transform.position = savedPosition + (randomLocation(0));
                    overlapObjects = Physics.OverlapBox(temp.gameObject.transform.position, temp.gameObject.GetComponent<Collider>().bounds.size);
                } while (overlapObjects.Length != 0);
                break;
            case 1:
                GameObject temp2 = EnemyPoolManager.singleton.GetLargeRange(gameObject.transform.position);
                Vector3 savedPosition2 = temp2.gameObject.transform.position;
                do
                {
                    temp2.gameObject.transform.position = savedPosition2 + (randomLocation(0));
                    overlapObjects = Physics.OverlapBox(temp2.gameObject.transform.position, temp2.gameObject.GetComponent<Collider>().bounds.size);
                } while (overlapObjects.Length != 0 && !(overlapObjects.Length == 1 && overlapObjects[0].name == "Plane"));
                break;
            case 2:
                GameObject temp3 = EnemyPoolManager.singleton.GetsmallMelee(gameObject.transform.position);
                Vector3 savedPosition3 = temp3.gameObject.transform.position + new Vector3(0, 1, 0);
                do
                {
                    temp3.gameObject.transform.position = savedPosition3 + (randomLocation(0));
                    overlapObjects = Physics.OverlapBox(temp3.gameObject.transform.position, temp3.gameObject.GetComponent<Collider>().bounds.size);
                } while (overlapObjects.Length != 0);
                break;
            case 3:
                GameObject temp4 = EnemyPoolManager.singleton.GetBuffer(gameObject.transform.position);
                Vector3 savedPosition4 = temp4.gameObject.transform.position;
                do
                {
                    temp4.gameObject.transform.position = savedPosition4 + (randomLocation(0));
                    overlapObjects = Physics.OverlapBox(temp4.gameObject.transform.position, temp4.gameObject.GetComponent<Collider>().bounds.size);
                } while (overlapObjects.Length != 0 && !(overlapObjects.Length == 1 && overlapObjects[0].name == "Plane"));
                break;
        }
    }
    Vector3 randomLocation(float specialHeight)
    {
        //Random location in a 50 by 50 square
        Vector3 randomSpot = new Vector3(Random.Range(-25, 25), specialHeight, (Random.Range(-25, 25)));
        return randomSpot;
    }
}
