using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

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

    float timerToSpawnSaved;

    bool spawned = false;
    bool lastState = true;
    // Start is called before the first frame update
    void Start()
    {
        timerToSpawnSaved = timerToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        switch (isDistActivOrTime) {
            case 0:
                if (distFromPlayerTillSpawn > Vector3.Distance(BulletPoolManager.singleton.player.transform.position, gameObject.transform.position) && spawned == false)
                {
                    for (int i = 0; i < amountToSpawn; i++)
                    {

                        spawnEnemy(i);
                    }
                    turnOff();
                }
                break;
            case 1:
                if (timerToSpawn == 0 && spawned == false)
                {
                    for (int i = 0; i < amountToSpawn; i++)
                    {

                        spawnEnemy(i);
                    }
                    turnOff();
                }
                else
                {
                    timerToSpawn--;
                }
                break;
        }
        if (lastState == false && gameObject.activeSelf == true)
        {
            timerToSpawn = timerToSpawnSaved;
            spawned = false;
        }
        lastState = gameObject.activeSelf;
    }
    void spawnEnemy(int number)
    {
        switch (typeOfEnemyToSpawn)
        {
            case 0:
                EnemyPoolManager.singleton.GetSmallShooter(gameObject.transform.position + new Vector3(10 * Mathf.Cos((360 / (number + 1))), 0, 10 * Mathf.Sin((360 / (number + 1)))));

                spawned = true;
                break;
            case 1:
                EnemyPoolManager.singleton.GetLargeRange(gameObject.transform.position + new Vector3(10 * Mathf.Cos((360 / (number + 1))), 0, 10 * Mathf.Sin((360 / (number + 1)))));
                
                spawned = true;
                break;
            case 2:
                EnemyPoolManager.singleton.GetsmallMelee(gameObject.transform.position + new Vector3(10 * Mathf.Cos((360 / (number + 1))), 0, 10 * Mathf.Sin((360 / (number + 1)))));
                
                spawned = true;
                break;
            case 3:
                EnemyPoolManager.singleton.GetBuffer(gameObject.transform.position + new Vector3(10 * Mathf.Cos((360 / (number + 1))), 0, 10 * Mathf.Sin((360 / (number + 1)))));
               
                spawned = true;
                break;
        }
    }
    void turnOff()
    {
        this.gameObject.SetActive(false);
    }
    public void reActive()
    {
        this.gameObject.SetActive(true);
    }
}
