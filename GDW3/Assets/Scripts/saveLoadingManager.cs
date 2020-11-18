using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveLoadingManager : MonoBehaviour
{
    //Save manager needs to have a save call
    //Have a load call
    //Anything else? I don't think so


    static float hitAccuracy = 0;
    static float numberOfAttacks = 0;
    static float numberOfTimesHit = 0;
    static float numberOfKills = 0;
    static float healthHealed = 0;
    static float numberOfDeaths = 0;

    enemySpawner[] allSpawnersInScene;
    enemyBehavior[] allEnemiesInScene;

    public GameObject holder;

    public static saveLoadingManager singleton = null;
    public void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            return;
        }
        Destroy(this);
    }

    public void saveFile(string filePath)
    {
        //string filePath = Application.dataPath + "/SaveData/SaveData8.txt";
        //gameObject.GetComponent<>().saveCommand(filePath);
        //currentSave++;


        

        allSpawnersInScene = UnityEngine.Object.FindObjectsOfType<enemySpawner>();

        foreach (enemySpawner objects in allSpawnersInScene)
            if (objects.gameObject.activeInHierarchy)
            {
                //Call the saveObject function in dll to save the objects
                //gameObject.GetComponent<PluginTester>().callSaveObject(objects.tag, objects.transform.position.x, objects.transform.position.y, objects.transform.position.z);
                gameObject.GetComponent<saveLoadCheckPoint>().callSaveObject(objects.name, objects.transform.position.x, objects.transform.position.y, objects.transform.position.z, 0 /*Temp needs to have actual thing*/);
            }
        gameObject.GetComponent<saveLoadCheckPoint>().callSaveObject(BulletPoolManager.singleton.player.tag, BulletPoolManager.singleton.player.transform.position.x, BulletPoolManager.singleton.player.transform.position.y, BulletPoolManager.singleton.player.transform.position.z, 0 /*Temp needs to have actual thing*/);

        //Call the save to file function in dll since all objects are saved
        gameObject.GetComponent<saveLoadCheckPoint>().callSaveObjectsToFile(filePath);

    }                                                                                            
    
    public void savePlayer(string filePath)
    {
        //Player Stats
        gameObject.GetComponent<saveLoadCheckPoint>().callSaveNewPlayerInfo(filePath, hitAccuracy, numberOfAttacks, numberOfTimesHit, numberOfKills, healthHealed, numberOfDeaths);
    }

    public void loadFile(string filePath)                                                        
    {                                                                                            
        allEnemiesInScene = UnityEngine.Object.FindObjectsOfType<enemyBehavior>();               

        foreach (enemyBehavior objects in allEnemiesInScene)
        {
            switch(objects.name){
                case "basicShooter(Clone)":
                    EnemyPoolManager.singleton.ResetSmallShooter(objects.gameObject);
                    break;
                case "HeavyShooter(Clone)":
                    EnemyPoolManager.singleton.ResetLargeRange(objects.gameObject);
                    break;
                case "Melee(Clone)":
                    EnemyPoolManager.singleton.ResetsmallMelee(objects.gameObject);
                    break;
                case "Buffer(Clone)":
                    EnemyPoolManager.singleton.ResetBuffer(objects.gameObject);
                    break;
            }
        }
            //objects.gameObject.GetComponent<ene>;
        


        allSpawnersInScene = Resources.FindObjectsOfTypeAll<enemySpawner>();

        foreach (enemySpawner objects in allSpawnersInScene)
            objects.gameObject.SetActive(false);

        


        gameObject.GetComponent<saveLoadCheckPoint>().callLoadObjectData(filePath);
        Vector3 objectPosition = new Vector3(0, 0, 0);
        string objectName = "";
        int i = 0;
        int questsCompleted = gameObject.GetComponent<saveLoadCheckPoint>().callGetQuestIsAt();
        do
        {
            objectName = gameObject.GetComponent<saveLoadCheckPoint>().callGetLoadedObjectsString();
            objectPosition.x = gameObject.GetComponent<saveLoadCheckPoint>().callGetLoadedObjectsX();
            objectPosition.y = gameObject.GetComponent<saveLoadCheckPoint>().callGetLoadedObjectsY();
            objectPosition.z = gameObject.GetComponent<saveLoadCheckPoint>().callGetLoadedObjectsZ();
            for (int z = 0; z < allSpawnersInScene.Length; z++)
            {
                if (objectName == allSpawnersInScene[z].name)
                {

                    allSpawnersInScene[z].gameObject.GetComponent<enemySpawner>().reActive();
                    i++;
                }
                else if (objectName == "Player")
                {
                    BulletPoolManager.singleton.player.transform.position = objectPosition;
                }
            }
        } while (gameObject.GetComponent<saveLoadCheckPoint>().callPopObject() > 0);

        for (int y = 0; y < questsCompleted; y++)
        {
            QuestManagementSystem.singleton.forceQuestUpdate();
        }

    }

    public void loadPlayer(string filePath)
    {
        gameObject.GetComponent<saveLoadCheckPoint>().callLoadPlayerInfo(filePath);
        float[] temp = gameObject.GetComponent<saveLoadCheckPoint>().callGetPlayerInfo();
        float hitAccuracy = temp[0];
        float numberOfAttacks = temp[1];
        float numberOfTimesHit = temp[2];
        float numberOfKills = temp[3];
        float healthHealed = temp[4];
        float numberOfDeaths = temp[5];
    }
    


    public void changeHitAccuracy(float hit)
    {
        numberOfAttacks++;
        numberOfTimesHit += hit;
        hitAccuracy = numberOfTimesHit / numberOfAttacks;
        hitAccuracy /= 100;
    }

    public void numberOfKilledUpdate()
    {
        numberOfKills++;
    }
    public void numberOfDeathsUpdate() //Death is not implemented yet so this is not updating yet
    {
        numberOfDeaths++;
    }

    public void amountOfHealingUpdate(float amount) //Not implemented yet either
    {
        healthHealed += amount;
    }

    public float[] getData()
    {
        float[] hello = { hitAccuracy, numberOfAttacks, numberOfTimesHit, numberOfKills, healthHealed, numberOfDeaths };
        return hello;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
