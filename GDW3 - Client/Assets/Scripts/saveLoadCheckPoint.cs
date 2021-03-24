using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class saveLoadCheckPoint : MonoBehaviour
{
    const string DLL_NAME = "saveAndLoadDLL2";




    [StructLayout(LayoutKind.Sequential)]
    struct GameForSendingToUnityObject
    {
        public string name;
        public float x;
        public float y;
        public float z;
    }


    [DllImport(DLL_NAME)]
    private static extern void saveObjects(string name, float posX, float posY, float posZ, int questAt);

    [DllImport(DLL_NAME)]
    private static extern void saveObjectsToFile(string filePath);
    [DllImport(DLL_NAME)]
    private static extern void loadObjectData(string filePath);
    [DllImport(DLL_NAME)]
    private static extern int popObject();

    [DllImport(DLL_NAME)]
    private static extern int getQuestAt();

    [DllImport(DLL_NAME)]
    private static extern GameForSendingToUnityObject getLoadedObjects();

    [StructLayout(LayoutKind.Sequential)]
    struct playerInformation
    {
        public float hitAccuracy, numberOfChargedAttacks, numberOfTimesHit, numberOfKills, healthHealed, numberOfDeaths;
    };

    [DllImport(DLL_NAME)]
    private static extern void savePlayerInfo(string filePath, playerInformation theInfo);

    [DllImport(DLL_NAME)]
    private static extern void saveNewPlayerInfo(string filePath, playerInformation theInfo);

    [DllImport(DLL_NAME)]
    private static extern void loadPlayerInfo(string filePath);

    [DllImport(DLL_NAME)]
    private static extern playerInformation getPlayerInfo();

    public void callSavePlayerInfo(string filePath, float hitAccuracy, float numberOfChargedAttacks, float numberOfTimesHit, float numberOfKills, float healthHealed, float numberOfDeaths)
    {
        playerInformation hello = new playerInformation();
        hello.hitAccuracy = hitAccuracy;
        hello.numberOfChargedAttacks = numberOfChargedAttacks;
        hello.numberOfTimesHit = numberOfTimesHit;
        hello.numberOfKills = numberOfKills;
        hello.healthHealed = healthHealed;
        hello.numberOfDeaths = numberOfDeaths;
        
        savePlayerInfo(filePath, hello);
    }

    public void callSaveNewPlayerInfo(string filePath, float hitAccuracy, float numberOfChargedAttacks, float numberOfTimesHit, float numberOfKills, float healthHealed, float numberOfDeaths)
    {
        playerInformation hello = new playerInformation();
        hello.hitAccuracy = hitAccuracy;
        hello.numberOfChargedAttacks = numberOfChargedAttacks;
        hello.numberOfTimesHit = numberOfTimesHit;
        hello.numberOfKills = numberOfKills;
        hello.healthHealed = healthHealed;
        hello.numberOfDeaths = numberOfDeaths;

        saveNewPlayerInfo(filePath, hello);
    }

    public void callLoadPlayerInfo(string filePath)
    {
        loadPlayerInfo(filePath);
    }

    public float[] callGetPlayerInfo()
    {
        playerInformation temp = getPlayerInfo();
        float[] hello = { temp.hitAccuracy, temp.numberOfChargedAttacks, temp.numberOfTimesHit, temp.numberOfKills, temp.healthHealed, temp.numberOfDeaths };
        return hello;
    }

    public void callSaveObject(string name, float posX, float posY, float posZ, int questAt)
    {
        saveObjects(name, posX, posY, posZ, questAt);
    }

    public void callSaveObjectsToFile(string filePath)
    {
        saveObjectsToFile(filePath);
    }

    public void callLoadObjectData(string filePath)
    {
        loadObjectData(filePath);
    }

    public int callPopObject()
    {
        return popObject();
    }

    public string callGetLoadedObjectsString()
    {
        return getLoadedObjects().name;
    }

    public float callGetLoadedObjectsX()
    {
        return getLoadedObjects().x;
    }
    public float callGetLoadedObjectsY()
    {
        return getLoadedObjects().y;
    }
    public float callGetLoadedObjectsZ()
    {
        return getLoadedObjects().z;
    }

    public int callGetQuestIsAt()
    {
        return getQuestAt();
    }

    // Start is called befGameForSendingToUnityObject getLoadedObjects();ore the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
