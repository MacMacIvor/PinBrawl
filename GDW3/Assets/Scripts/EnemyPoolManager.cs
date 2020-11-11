using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{

    public static EnemyPoolManager singleton = null;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            return;
        }
        Destroy(this);
    }

    public GameObject smallMelee;
    public GameObject smallShooter;
    public GameObject largeRange;
    public GameObject buffer;

    static List<GameObject> enemySmallMelee;
    static List<GameObject> enemySmallRange;
    static List<GameObject> enemyLargeRange;
    static List<GameObject> enemyBuffer;

    private Vector3 initSpawn = new Vector3(100, 100, 100);

    [Range(0, 100)]
    public int maxSmallShooter = 8;
    [Range(0, 100)]
    public int maxSmallMelee = 8;
    [Range(0, 100)]
    public int maxLarge = 8;
    [Range(0, 100)]
    public int maxBuffer = 8;


    static int indexSmallMelee = 0;
    static int indexSmallShooter = 0;
    static int indexLargeRange = 0;
    static int indexBuffer = 0;


    // Start is called before the first frame update
    void Start()
    {
        
        _BuildEnemyPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void _BuildEnemyPool()
    {
        enemySmallMelee = new List<GameObject>();
        for (int i = 0; i < maxSmallMelee; i++)
        {
            GameObject newEnemy = Instantiate(smallMelee, this.transform);
            newEnemy.transform.position = initSpawn;
            enemySmallMelee.Add(newEnemy);
        }

        enemySmallRange = new List<GameObject>();
        for (int i = 0; i < maxSmallShooter; i++)
        {
            GameObject newEnemy = Instantiate(smallShooter, this.transform);
            newEnemy.transform.position = initSpawn;
            enemySmallRange.Add(newEnemy);
        }

        enemyLargeRange = new List<GameObject>();
        for (int i = 0; i < maxLarge; i++)
        {
            GameObject newEnemy = Instantiate(largeRange, this.transform);
            newEnemy.transform.position = initSpawn;
            enemyLargeRange.Add(newEnemy);
        }

        enemyBuffer = new List<GameObject>();
        for (int i = 0; i < maxBuffer; i++)
        {
            GameObject newEnemy = Instantiate(buffer, this.transform);
            newEnemy.transform.position = initSpawn;
            enemyBuffer.Add(newEnemy);
        }
    }


    public GameObject GetSmallShooter(Vector3 pos)
    {
        bool spawned = false;
        int indexStartedAt = indexSmallShooter;
        do
        {

            switch (enemySmallRange[indexSmallShooter].activeInHierarchy)
            {
                case true:
                    indexSmallShooter++;
                    break;
                case false:
                    enemySmallRange[indexSmallShooter].transform.position = pos;
                    enemySmallRange[indexSmallShooter].GetComponent<enemyBehavior>().changeActive();
                    spawned = true;
                    break;
            }

            if (indexSmallShooter >= enemySmallRange.Count)
                indexSmallShooter = 0;
            if (indexSmallShooter == indexStartedAt)
            {
                //Resources used up so we will just not spawn anything
                spawned = true;
            }
        } while (spawned == false);
        return enemySmallRange[indexSmallShooter];
    }

    public void ResetSmallShooter(GameObject enemy)
    {
        for (int i = 0; i < enemySmallRange.Count; i++)
        {
            if (enemy == enemySmallRange[i])
            {
                enemySmallRange[i].GetComponent<enemyBehavior>().changeActive();
            }
        }

    }
    

    public GameObject GetLargeRange(Vector3 pos)
    {
        bool spawned = false;
        int indexStartedAt = indexLargeRange;
        do
        {

            switch (enemyLargeRange[indexLargeRange].activeInHierarchy)
            {
                case true:
                    indexLargeRange++;
                    break;
                case false:
                    enemyLargeRange[indexLargeRange].transform.position = pos;
                    enemyLargeRange[indexLargeRange].GetComponent<enemyBehavior>().changeActive();
                    spawned = true;
                    break;
            }

            if (indexLargeRange >= enemyLargeRange.Count)
                indexLargeRange = 0;
            if (indexLargeRange == indexStartedAt)
            {
                //Resources used up so we will just not spawn anything
                spawned = true;
            }
        } while (spawned == false);
        return enemyLargeRange[indexLargeRange];
    }


    public void ResetLargeRange(GameObject enemy)
    {

        for (int i = 0; i < enemyLargeRange.Count; i++)
        {
            if (enemy == enemyLargeRange[i])
            {
                enemyLargeRange[i].GetComponent<enemyBehavior>().changeActive();
            }
        }

    }

    
    public GameObject GetsmallMelee(Vector3 pos)
    {
        bool spawned = false;
        int indexStartedAt = indexSmallMelee;
        do
        {

            switch (enemySmallMelee[indexSmallMelee].activeInHierarchy)
            {
                case true:
                    indexSmallMelee++;
                    break;
                case false:
                    enemySmallMelee[indexSmallMelee].transform.position = pos;
                    enemySmallMelee[indexSmallMelee].GetComponent<enemyBehavior>().changeActive();
                    spawned = true;
                    break;
            }

            if (indexSmallMelee >= enemySmallMelee.Count)
                indexSmallMelee = 0;
            if (indexSmallMelee == indexStartedAt)
            {
                //Resources used up so we will just not spawn anything
                spawned = true;
            }
        } while (spawned == false);
        return enemySmallMelee[indexSmallMelee];
    }


    public void ResetsmallMelee(GameObject enemy)
    {
        for (int i = 0; i < enemySmallMelee.Count; i++)
        {
            if (enemy == enemySmallMelee[i])
            {
                enemySmallMelee[i].GetComponent<enemyBehavior>().changeActive();
            }
        }


    }

   
    public GameObject GetBuffer(Vector3 pos)
    {
        bool spawned = false;
        int indexStartedAt = indexBuffer;
        do
        {

            switch (enemyBuffer[indexBuffer].activeInHierarchy)
            {
                case true:
                    indexBuffer++;
                    break;
                case false:
                    enemyBuffer[indexBuffer].transform.position = pos;
                    enemyBuffer[indexBuffer].GetComponent<enemyBehavior>().changeActive();
                    spawned = true;
                    break;
            }

            if (indexBuffer >= enemyBuffer.Count)
                indexBuffer = 0;
            if (indexBuffer == indexStartedAt)
            {
                //Resources used up so we will just not spawn anything
                spawned = true;
            }
        } while (spawned == false);
        return enemyBuffer[indexBuffer];
    }


    public void ResetBuffer(GameObject enemy)
    {
        for (int i = 0; i < enemyBuffer.Count; i++)
        {
            if (enemy == enemyBuffer[i])
            {
                enemyBuffer[i].GetComponent<enemyBehavior>().changeActive();
            }
        }
    }
}
