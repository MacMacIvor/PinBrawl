using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveLoadingManager : MonoBehaviour
{
    //Save manager needs to have a save call
    //Have a load call
    //Anything else? I don't think so
    enemySpawner[] allSpawnersInScene;

    public static saveLoadingManager soundsSingleton = null;
    public void Awake()
    {
        if (soundsSingleton == null)
        {
            soundsSingleton = this;
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
                gameObject.GetComponent<saveLoadCheckPoint>().callSaveObject(objects.tag, objects.transform.position.x, objects.transform.position.y, objects.transform.position.z, 0 /*Temp needs to have actual thing*/);
            }
        gameObject.GetComponent<saveLoadCheckPoint>().callSaveObject(BulletPoolManager.singleton.player.tag, BulletPoolManager.singleton.player.transform.position.x, BulletPoolManager.singleton.player.transform.position.y, BulletPoolManager.singleton.player.transform.position.z, 0 /*Temp needs to have actual thing*/);

        //Call the save to file function in dll since all objects are saved
        gameObject.GetComponent<saveLoadCheckPoint>().callSaveObjectsToFile(filePath);
    }

    public void loadFile(string filePath)
    {
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

            if (objectName == allSpawnersInScene[i].name && objectPosition == allSpawnersInScene[i].transform.position)
            {
                allSpawnersInScene[i].gameObject.SetActive(true);
            }
            i++;
        } while (gameObject.GetComponent<saveLoadCheckPoint>().callPopObject() > 0);

        for (int y = 0; y < questsCompleted; y++)
        {
            QuestManagementSystem.singleton.forceQuestUpdate();
        }

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
