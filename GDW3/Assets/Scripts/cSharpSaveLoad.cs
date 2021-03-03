using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
public class cSharpSaveLoad : MonoBehaviour
{
    public GameObject cube;


    GameObject[] allObjectsInScene;
    List<string> objectsWords = new List<string>();
    private int currentSave = -1;
    //list.Add("Hi");
    //String[] str = list.ToArray();



    enemySpawner[] allSpawnersInScene;
    enemyBehavior[] allEnemiesInScene;

    static public bool loadNextFrame = false;


    public static cSharpSaveLoad singleton = null;
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

    }


    public void loadFile(string filePath)
    {
        allEnemiesInScene = UnityEngine.Object.FindObjectsOfType<enemyBehavior>();

        foreach (enemyBehavior objects in allEnemiesInScene)
        {
            switch (objects.name)
            {
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



    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        string filePath = Application.dataPath + "/SaveData/c#Save.txt";
        if (Input.GetKeyDown(KeyCode.K))
        {
            
       
             //File.Delete(filePath + "/Saves.txt");

             File.WriteAllText(filePath, ""); //Supposed to clear the file, Doesn't work :( - I would say needs to be looked at but we are soon brigning this to c++ anyway

            allSpawnersInScene = UnityEngine.Object.FindObjectsOfType<enemySpawner>();
            int once = 0;
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(filePath, "")))
            {
                foreach (enemySpawner objects in allSpawnersInScene)
                    if (objects.gameObject.activeInHierarchy)
                    {

                        //Call the saveObject function in dll to save the objects
                        //gameObject.GetComponent<PluginTester>().callSaveObject(objects.tag, objects.transform.position.x, objects.transform.position.y, objects.transform.position.z);
                        //gameObject.GetComponent<saveLoadCheckPoint>().callSaveObject(objects.name, objects.transform.position.x, objects.transform.position.y, objects.transform.position.z, QuestManagementSystem.singleton.getAmountOfQuestsCompleted() /*Temp needs to have actual thing*/);

                        //string[] str = { objects.name, objects.transform.position.x, objects.transform.position.y, objects.transform.position.z, QuestManagementSystem.singleton.getAmountOfQuestsCompleted() };



                        if (once == 0)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (i == 0)
                                    outputFile.WriteLine(BulletPoolManager.singleton.player.tag);
                                else if (i == 1)
                                    outputFile.WriteLine(BulletPoolManager.singleton.player.transform.position.x);
                                else if (i == 2)
                                    outputFile.WriteLine(BulletPoolManager.singleton.player.transform.position.y);
                                else if (i == 3)
                                    outputFile.WriteLine(BulletPoolManager.singleton.player.transform.position.z);
                            }
                            once++;
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            if (i == 0)
                                outputFile.WriteLine(objects.name);
                            else if (i == 1)
                                outputFile.WriteLine(objects.transform.position.x);
                            else if (i == 2)
                                outputFile.WriteLine(objects.transform.position.y);
                            else if (i == 3)
                                outputFile.WriteLine(objects.transform.position.z);
                        }

                    }
            }
           
            //gameObject.GetComponent<saveLoadCheckPoint>().callSaveObject(BulletPoolManager.singleton.player.tag, BulletPoolManager.singleton.player.transform.position.x, BulletPoolManager.singleton.player.transform.position.y, BulletPoolManager.singleton.player.transform.position.z, QuestManagementSystem.singleton.getAmountOfQuestsCompleted() /*Temp needs to have actual thing*/);

            //Call the save to file function in dll since all objects are saved
            //gameObject.GetComponent<saveLoadCheckPoint>().callSaveObjectsToFile(filePath);


            //allObjectsInScene = UnityEngine.Object.FindObjectsOfType<GameObject>();
            //foreach (GameObject objects in allObjectsInScene)
            //    if (objects.activeInHierarchy && objects.tag != "Untagged" && objects.tag != "MainCamera")
            //    {
            //        print(objects.tag + " is an active object");
            //        objectsWords.Add(objects.tag);
            //        objectsWords.Add(objects.transform.position.x.ToString());
            //        objectsWords.Add(objects.transform.position.y.ToString());
            //        objectsWords.Add(objects.transform.position.z.ToString());
            //        objectsWords.Add(objects.transform.rotation.x.ToString());
            //        objectsWords.Add(objects.transform.rotation.y.ToString());
            //        objectsWords.Add(objects.transform.rotation.z.ToString());
            //
            //
            //
            //        string[] str = objectsWords.ToArray();
            //
            //
            //        using (StreamWriter outputFile = new StreamWriter(Path.Combine(filePath, "Saves" + currentSave + ".txt")))
            //        {
            //            foreach (string line in str)
            //                outputFile.WriteLine(line);
            //        }
            //    }
        }













        if (Input.GetKeyDown(KeyCode.L))
        {
            allObjectsInScene = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject objects in allObjectsInScene)
                if (objects.activeInHierarchy && objects.tag != "Untagged" && objects.tag != "MainCamera")
                {
                    Destroy(objects.gameObject);
                }


            int count = 0;
            Vector3 objectPosition = new Vector3(0, 0, 0);
            Vector3 objectAngles = new Vector3(0, 0, 0);
            string objectName = "";

            
            string objectsData;

            using (var line = new StreamReader(Path.Combine(filePath, "Saves" + currentSave + ".txt")))
            {
                while ((objectsData = line.ReadLine()) != null)
                {
                    switch (count)
                    {
                        case 0:
                            objectName = objectsData;
                            break;

                        case 1:
                            objectPosition.x = float.Parse(objectsData);
                            break;

                        case 2:
                            objectPosition.y = float.Parse(objectsData);
                            break;

                        case 3:
                            objectPosition.z = float.Parse(objectsData);
                            break;

                        case 4:
                            objectAngles.x = float.Parse(objectsData);
                            break;

                        case 5:
                            objectAngles.y = float.Parse(objectsData);
                            break;

                        case 6:
                            objectAngles.z = float.Parse(objectsData);
                            break;
                    }

                    count++;
                    if (count > 6)
                    {
                        count = 0;

                        switch (objectName)
                        {
                            case "NormalCube":
                                GameObject cloneOfObject = Instantiate(cube) as GameObject;
                                cube.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                                cube.transform.position = objectPosition;
                                cube.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                                break;

                        }

                        /*
                         case "":
                                GameObject cloneOfObject = Instantiate() as GameObject;
                                .name += Random.Range(Random.Range(-1000000.0f, 0.0f), Random.Range(0.0f, 1000000.0f));
                                .transform.position = objectPosition;
                                .transform.rotation.x = objectAngles.x;
                                .transform.rotation.y = objectAngles.y;
                                .transform.rotation.z = objectAngles.z;
                                break;

                         */
                    }
                }
            }
        }

    }


}
