using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class savingScript : MonoBehaviour
{
    public GameObject cube;
    public GameObject BookShelf;
    public GameObject Door;
    public GameObject Door_Frame;
    public GameObject Five_Rail;
    public GameObject Four_Rail;
    public GameObject Rail_Post;
    public GameObject Rising_Rail;
    public GameObject Room_Plane;
    public GameObject Stair_Support;
    public GameObject Stairs;
    public GameObject Stool;
    public GameObject Table;
    public GameObject Top_Rail;
    public GameObject Upper_Floor;
    public GameObject EnemyShooter;
    public GameObject MainCharacter;
    public GameObject BookShelfWide;
    public GameObject Fire_Place;


    GameObject[] allObjectsInScene;
    List<string> objectsWords = new List<string>();
    //private int currentSave = -1;

    private bool isEditing = false;
    private bool isEDown = false;
    //list.Add("Hi");
    //String[] str = list.ToArray();
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (isEDown == false)
            {
                isEditing = !isEditing;
            }
            isEDown = true;
        }
        else
        {
            isEDown = false;
        }

        switch (isEditing)
        {
            case true:
                ;
                break;

        }
    }

    public void saveCommand(string theFilePath)
    {
        if (isEditing == true)
        {

            //currentSave++;
            string filePath = theFilePath;

            allObjectsInScene = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject objects in allObjectsInScene)
                if (objects.activeInHierarchy && objects.tag != "Untagged" && objects.tag != "MainCamera")
                {
                    //Call the saveObject function in dll to save the objects
                    gameObject.GetComponent<PluginTester>().callSaveObject(objects.tag, objects.transform.position.x, objects.transform.position.y, objects.transform.position.z);
                }
            //Call the save to file function in dll since all objects are saved
            gameObject.GetComponent<PluginTester>().callSaveObjectsToFile(filePath);
        }
    }

    public void loadCommand(string theFilePath)
    {
        if (isEditing == true)
        {
            for (int i = 0; i <= 1; i++) //Need to load it twice for some reason otherwise unity moves the objects to places they shouldn't be
            {
                allObjectsInScene = UnityEngine.Object.FindObjectsOfType<GameObject>();
                foreach (GameObject objects in allObjectsInScene)
                    if (objects.activeInHierarchy && objects.tag != "Untagged" && objects.tag != "MainCamera")
                    {
                        Destroy(objects.gameObject);
                    }

                string filePath = theFilePath;
                Vector3 objectPosition = new Vector3(0, 0, 0);
                string objectName = "";


                //Call loadFile from Dll
                gameObject.GetComponent<PluginTester>().callLoadObjectData(filePath);
                //Call the getObjects
                do
                {
                    objectName = gameObject.GetComponent<PluginTester>().callGetLoadedObjectsString();
                    objectPosition.x = gameObject.GetComponent<PluginTester>().callGetLoadedObjectsX();
                    objectPosition.y = gameObject.GetComponent<PluginTester>().callGetLoadedObjectsY();
                    objectPosition.z = gameObject.GetComponent<PluginTester>().callGetLoadedObjectsZ();

                    switch (objectName)
                    {
                        case "NormalCube":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, cube);

                            break;
                        case "BookShelf":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, BookShelf);
                            break;
                        case "Door":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, Door);
                            break;
                        case "Door_Frame":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, Door_Frame);
                            break;
                        case "Five_Rail":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, Five_Rail);
                            break;
                        case "Four_Rail":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, Four_Rail);
                            break;
                        case "Rail_Post":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, Rail_Post);
                            break;
                        case "Rising_Rail":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, Rising_Rail);
                            break;
                        case "Room_Plane":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, Room_Plane);
                            break;
                        case "Stair_Support":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, Stair_Support);
                            break;
                        case "Stairs":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, Stairs);
                            break;
                        case "Stool":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, Stool);
                            break;
                        case "Table":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, Table);
                            break;
                        case "Top_Rail":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, Top_Rail);
                            break;
                        case "Upper_Floor":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, Upper_Floor);
                            break;
                        case "EnemyShooter":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, EnemyShooter);
                            break;
                        case "Player":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, MainCharacter);
                            break;
                        case "BookShelfWide":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, BookShelfWide);
                            break;
                        case "Fire_Place":
                            gameObject.GetComponent<factory>().spawn(objectName, objectPosition, Fire_Place);
                            break;
                    }

                    //gameObject.GetComponent<factory>().spawn(objectName, objectPosition, );

                } while (gameObject.GetComponent<PluginTester>().callPopObject() > 0);




                //int count = 0;


                //if (count > 6)
                //{
                //    count = 0;
                //
                //    switch (objectName)
                //    {
                //        case "NormalCube":
                //            GameObject cloneOfObject = Instantiate(cube) as GameObject;
                //            cube.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            cube.transform.position = objectPosition;
                //            cube.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "BookShelf":
                //            GameObject cloneOfObject1 = Instantiate(BookShelf) as GameObject;
                //            BookShelf.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            BookShelf.transform.position = objectPosition;
                //            BookShelf.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //
                //        case "Door":
                //            GameObject cloneOfObject2 = Instantiate(Door) as GameObject;
                //            Door.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            Door.transform.position = objectPosition;
                //            Door.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "Door_Frame":
                //            GameObject cloneOfObject3 = Instantiate(Door_Frame) as GameObject;
                //            Door_Frame.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            Door_Frame.transform.position = objectPosition;
                //            Door_Frame.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "Five_Rail":
                //            GameObject cloneOfObjectFive_Rail = Instantiate(Five_Rail) as GameObject;
                //            Five_Rail.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            Five_Rail.transform.position = objectPosition;
                //            Five_Rail.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "Four_Rail":
                //            GameObject cloneOfObjectFour_Rail = Instantiate(Four_Rail) as GameObject;
                //            Four_Rail.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            Four_Rail.transform.position = objectPosition;
                //            Four_Rail.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "Rail_Post":
                //            GameObject cloneOfObjectRail_Post = Instantiate(Rail_Post) as GameObject;
                //            Rail_Post.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            Rail_Post.transform.position = objectPosition;
                //            Rail_Post.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "Rising_Rail":
                //            GameObject cloneOfObjectRising_Rail = Instantiate(Rising_Rail) as GameObject;
                //            Rising_Rail.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            Rising_Rail.transform.position = objectPosition;
                //            Rising_Rail.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "Room_Plane":
                //            GameObject cloneOfObjectRoom_Plane = Instantiate(Room_Plane) as GameObject;
                //            Room_Plane.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            Room_Plane.transform.position = objectPosition;
                //            Room_Plane.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "Stair_Support":
                //            GameObject cloneOfObjectStair_Support = Instantiate(Stair_Support) as GameObject;
                //            Stair_Support.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            Stair_Support.transform.position = objectPosition;
                //            Stair_Support.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "Stairs":
                //            GameObject cloneOfObjectStairs = Instantiate(Stairs) as GameObject;
                //            Stairs.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            Stairs.transform.position = objectPosition;
                //            Stairs.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "Stool":
                //            GameObject cloneOfObjectStool = Instantiate(Stool) as GameObject;
                //            Stool.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            Stool.transform.position = objectPosition;
                //            Stool.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "Table":
                //            GameObject cloneOfObjectTable = Instantiate(Table) as GameObject;
                //            Table.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            Table.transform.position = objectPosition;
                //            Table.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "Top_Rail":
                //            GameObject cloneOfObjectTop_Rail = Instantiate(Top_Rail) as GameObject;
                //            Top_Rail.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            Top_Rail.transform.position = objectPosition;
                //            Top_Rail.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "Upper_Floor":
                //            GameObject cloneOfObjectUpper_Floor = Instantiate(Upper_Floor) as GameObject;
                //            Upper_Floor.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            Upper_Floor.transform.position = objectPosition;
                //            Upper_Floor.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "EnemyShooter":
                //            GameObject cloneOfObjectEnemyShooter = Instantiate(EnemyShooter) as GameObject;
                //            EnemyShooter.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            EnemyShooter.transform.position = objectPosition;
                //            EnemyShooter.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "MainCharacter":
                //            GameObject cloneOfObjectMainCharacter = Instantiate(MainCharacter) as GameObject;
                //            MainCharacter.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            MainCharacter.transform.position = objectPosition;
                //            MainCharacter.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "BookShelfWide":
                //            GameObject cloneOfObjectBookShelfWide = Instantiate(BookShelfWide) as GameObject;
                //            BookShelfWide.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            BookShelfWide.transform.position = objectPosition;
                //            BookShelfWide.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //        case "Fire_Place":
                //            GameObject cloneOfObjectFire_Place = Instantiate(Fire_Place) as GameObject;
                //            Fire_Place.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            Fire_Place.transform.position = objectPosition;
                //            Fire_Place.transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //
                //    }
                //
                //    /*
                //     case "":
                //            GameObject cloneOfObject = Instantiate() as GameObject;
                //            .name += Random.Range(Random.Range(-1000000.0f, 0.0f), Random.Range(0.0f, 1000000.0f));
                //            .name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
                //            .transform.position = objectPosition;
                //            .transform.rotation = new Quaternion(objectAngles.x, objectAngles.y, objectAngles.z, 1.0f);
                //            break;
                //
                //     */
                //}
            }

        }
    }

}
