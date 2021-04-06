using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class clientScript : MonoBehaviour
{
    public GameObject myCube;
    public GameObject myPlayerClone;
    public GameObject playerHolder;

    private string h;

    private static byte[] outBuffer;
    private static IPEndPoint remoteEP;
    private static Socket clientSocket;
    private float interval;
    [Range(0, 0.004f)]
    public float intervals = 0.05f;
    private Vector3 lastPosition;
    private Vector3 lastRotation;
    private float attackStrength = 0;
    private float isBasic = 0;
    private float directionServer = 0;
    byte[] bpos;

    private static byte[] inBuffer;
    private static EndPoint endpoint;


    private static int lobby = 0;

    public GameObject poolManager;


    public static clientScript singleton;

    List<int> availableLobbies = new List<int>();

    Scene currentScene;
    List<RaycastResult> results;

    static bool foundOnce = false;
    GameObject lobbyListParent;

    int connectedLobby = 0;
    bool isAlive = true;


    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this);
            return;
        }
        Destroy(this);
    }

    public static void RunClient()
    {
        
        try
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            remoteEP = new IPEndPoint(ip, 11111);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            clientSocket.Blocking = false;

            endpoint = (EndPoint)remoteEP;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        interval = intervals;
        outBuffer = new byte[1024];
        inBuffer = new byte[1024];
        RunClient();
        StartCoroutine(sendServer(interval));
        StartCoroutine(sendEnemies(interval));
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        switch (currentScene.name)
        {
            case "prototype_tutorial":
            case "EndlessMode":
            case "First_Level":
                try
                {
                    myCube = GameObject.Find("player 1 (1)");
                    myPlayerClone = GameObject.Find("player 1");
                    playerHolder = GameObject.Find("PlayerHolder");
                    poolManager = GameObject.Find("enemyPoolManager");


                    int rec = clientSocket.ReceiveFrom(inBuffer, ref endpoint);
                    float[] pos = new float[rec / 4];
                    Buffer.BlockCopy(inBuffer, 0, pos, 0, rec);
                    
                    bool isEnemy = false;

                    try
                    {
                        float i = pos[100];
                        isEnemy = true;
                    }
                    catch (Exception e)
                    {

                    }

                    if (pos.Length == 5)
                    {
                        SceneManager.LoadScene("playerStats");
                        pointsManager.pointsSingleton.resetPoints();
                        float CauseACrash = pos[9];
                    }

                    switch (isEnemy)
                    {
                        case false: //The other player code
                            bool exists = false;

                            for (int i = 0; i < playerHolder.transform.childCount; i++)
                            {
                                if (pos[pos.Length - 1].ToString() == playerHolder.transform.GetChild(i).name)
                                {
                                    playerHolder.transform.GetChild(i).transform.position = new Vector3(pos[0], pos[1], pos[2]);
                                    playerHolder.transform.GetChild(i).transform.eulerAngles = new Vector3(playerHolder.transform.GetChild(i).transform.eulerAngles.x, pos[3], playerHolder.transform.GetChild(i).transform.eulerAngles.z);
                                    exists = true;
                                    i = playerHolder.transform.childCount;
                                }
                            }
                            if (!exists)
                            {
                                GameObject newPlayer = Instantiate(myPlayerClone, playerHolder.transform);
                                newPlayer.name = pos[pos.Length - 1].ToString();
                            }

                            break;

                        case true: //The enemies

                            for (int i = 0; i < 32; i++)
                            {
                                if (pos[(5 * i)] == 1)
                                {
                                    poolManager.transform.GetChild(i).transform.gameObject.GetComponent<enemyBehavior>().setTarget(pos[5 * i + 4].ToString());
                                    if (poolManager.transform.GetChild(i).transform.gameObject.GetComponent<enemyBehavior>().isActive() == 1)
                                    {
                                        poolManager.transform.GetChild(i).transform.position = new Vector3(pos[1 + i * 5], pos[2 + i * 5], pos[3 + i * 5]);

                                    }
                                    else
                                    {
                                        switch ((Mathf.Floor(i / 8)))
                                        {
                                            case 0:
                                                EnemyPoolManager.singleton.GetsmallMelee(new Vector3(pos[1 + i * 5], pos[2 + i * 5], pos[3 + i * 5]));
                                                break;
                                            case 1:
                                                EnemyPoolManager.singleton.GetSmallShooter(new Vector3(pos[1 + i * 5], pos[2 + i * 5], pos[3 + i * 5]));
                                                break;
                                            case 2:
                                                EnemyPoolManager.singleton.GetLargeRange(new Vector3(pos[1 + i * 5], pos[2 + i * 5], pos[3 + i * 5]));
                                                break;
                                            case 3:
                                                EnemyPoolManager.singleton.GetBuffer(new Vector3(pos[1 + i * 5], pos[2 + i * 5], pos[3 + i * 5]));
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (poolManager.transform.GetChild(i).transform.gameObject.GetComponent<enemyBehavior>().isActive() == 1)
                                    {
                                        //Deactivate the thing
                                        switch ((Mathf.Floor(i / 8)))
                                        {
                                            case 0:
                                                EnemyPoolManager.singleton.ResetsmallMelee(poolManager.transform.GetChild(i).transform.gameObject);
                                                break;
                                            case 1:
                                                EnemyPoolManager.singleton.ResetSmallShooter(poolManager.transform.GetChild(i).transform.gameObject);
                                                break;
                                            case 2:
                                                EnemyPoolManager.singleton.ResetLargeRange(poolManager.transform.GetChild(i).transform.gameObject);
                                                break;
                                            case 3:
                                                EnemyPoolManager.singleton.ResetBuffer(poolManager.transform.GetChild(i).transform.gameObject);
                                                break;
                                        }
                                    }
                                }

                            }

                            break;
                    }


                }
                catch (Exception e)
                {

                }
                break;
            case "Lobby":
                
                if (Input.GetMouseButtonDown(0))
                {
                    results = rayResult();
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i].gameObject.name == "Text(Clone)")
                        {
                            float[] intentionToJoin = { availableLobbies[results[i].gameObject.transform.GetSiblingIndex()], 0 };
                            bpos = new byte[intentionToJoin.Length * 4];
                            Buffer.BlockCopy(intentionToJoin, 0, bpos, 0, bpos.Length);
                            clientSocket.SendTo(bpos, remoteEP);

                            i = results.Count;
                        }
                        else if (results[i].gameObject.name == "New")
                        {
                            int random = UnityEngine.Random.Range(1000000, 80000000);
                            float[] intentionToJoin = { random, 1 };
                            bpos = new byte[intentionToJoin.Length * 4];
                            Buffer.BlockCopy(intentionToJoin, 0, bpos, 0, bpos.Length);
                            clientSocket.SendTo(bpos, remoteEP);

                            i = results.Count;
                        }
                    }
                }


                switch (foundOnce)
                {
                    case false:
                        lobbyListParent = GameObject.Find("Content");
                        foundOnce = true;
                        break;
                }
                try
                {
                    int rec = clientSocket.ReceiveFrom(inBuffer, ref endpoint);
                    float[] pos = new float[rec / 4];
                    Buffer.BlockCopy(inBuffer, 0, pos, 0, rec);

                    if (pos.Length == 1)
                    {

                        connectedLobby = (int)pos[0];
                        SceneManager.LoadScene("CharacterSelect");
                    }
                    else
                    {
                        for (int i = lobbyListParent.gameObject.transform.childCount - 1; i > 0; i--)
                        {
                            Destroy(lobbyListParent.gameObject.transform.GetChild(i).gameObject);
                        }
                        availableLobbies.Clear();

                        for (int i = 0; i < pos.Length; i += 2) // 
                        {
                            availableLobbies.Add((int)pos[i]);
                            Debug.Log(availableLobbies[i / 2]);
                            if (pos[i] != 0)
                            {
                                GameObject newLobby = Instantiate(lobbyListParent.gameObject.transform.GetChild(0).gameObject, lobbyListParent.gameObject.transform);
                                newLobby.GetComponent<Text>().text = "Lobby: " + availableLobbies[i / 2].ToString() + "                  Players: " + pos[i + 1].ToString();
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                }
                break;


            case "CharacterSelect":
                if (Input.GetMouseButtonDown(0))
                {
                    results = rayResult();
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i].gameObject.name == "Ready")
                        {
                            float[] next = { connectedLobby, 0, 0 };
                            bpos = new byte[next.Length * 4];
                            Buffer.BlockCopy(next, 0, bpos, 0, bpos.Length);
                            clientSocket.SendTo(bpos, remoteEP);
                            i = results.Count;
                        }
                    }
                }
                try
                {
                    int rec = clientSocket.ReceiveFrom(inBuffer, ref endpoint);
                    SceneManager.LoadScene("GamemodeSelect");
                }
                catch (Exception e)
                {

                }

                    break;
            case "GamemodeSelect":
                if (Input.GetMouseButtonDown(0))
                {
                    results = rayResult();
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i].gameObject.name == "Tut" || results[i].gameObject.name == "Lvl1" || results[i].gameObject.name == "End" || results[i].gameObject.name == "Back")
                        {
                            float[] next = { connectedLobby, 0, 0, (results[i].gameObject.name == "Tut" ? 0 : (results[i].gameObject.name == "Lvl1" ? 1 : (results[i].gameObject.name == "End" ? 2 : 3))) };
                            bpos = new byte[next.Length * 4];
                            Buffer.BlockCopy(next, 0, bpos, 0, bpos.Length);
                            clientSocket.SendTo(bpos, remoteEP);
                            i = results.Count;
                        }
                    }
                }
                try
                {
                    int rec = clientSocket.ReceiveFrom(inBuffer, ref endpoint);
                    float[] pos = new float[rec / 4];
                    Buffer.BlockCopy(inBuffer, 0, pos, 0, rec);
                    switch (pos[3])
                    {
                        case 0:
                            SceneManager.LoadScene("prototype_tutorial");
                            break;
                        case 1:
                            SceneManager.LoadScene("First_Level");
                            break;
                        case 2:
                            SceneManager.LoadScene("EndlessMode");
                            break;
                        case 3:
                            break;
                    }
                }
                catch (Exception e)
                {

                }
                break;
            case "playerStats":
                try
                {
                    int rec = clientSocket.ReceiveFrom(inBuffer, ref endpoint);
                    float[] pos = new float[rec / 4];
                    Buffer.BlockCopy(inBuffer, 0, pos, 0, rec);
                    GameObject text = GameObject.Find("Scores");
                    text.GetComponent<Text>().text =
                        "1: " + pos[0].ToString() + " Score: " + pos[1].ToString() + "\n" +
                        "2: " + pos[2].ToString() + " Score: " + pos[3].ToString() + "\n" +
                        "3: " + pos[4].ToString() + " Score: " + pos[5].ToString() + "\n" +
                        "4: " + pos[6].ToString() + " Score: " + pos[7].ToString() + "\n" +
                        "5: " + pos[8].ToString() + " Score: " + pos[9].ToString() + "\n";
                }
                catch (Exception e)
                {

                }
                break;
        }
        
        
    }

    public void doAttack(float basicOrCharged, float dmgAmount, int direction) //1 for charged and 0 for basic
    {
        isBasic = basicOrCharged;
        attackStrength = dmgAmount;
        directionServer = (float)direction;
    }

    IEnumerator sendServer(float timer)
    {

        Debug.Log("DataSent");
        while (true)
        {
            yield return new WaitForSeconds(timer);
            switch (currentScene.name)
            {
                case "prototype_tutorial":
                case "EndlessMode":
                case "First_Level":
                    if (lastPosition != myCube.transform.position || lastRotation != myCube.gameObject.transform.GetChild(2).gameObject.transform.eulerAngles)
                    {
                        float[] pos = { myCube.transform.position.x, myCube.transform.position.y, myCube.transform.position.z, myCube.gameObject.transform.GetChild(2).gameObject.transform.eulerAngles.y, isBasic, attackStrength, directionServer, isBasic, connectedLobby, 1 };
                        bpos = new byte[pos.Length * 4];
                        Buffer.BlockCopy(pos, 0, bpos, 0, bpos.Length);
                        outBuffer = Encoding.ASCII.GetBytes(myCube.transform.position.x.ToString());
                        clientSocket.SendTo(bpos, remoteEP);
                        Debug.Log("DataSent");
                        attackStrength = 0;
                    }
                    lastPosition = myCube.transform.position;
                    lastRotation = myCube.gameObject.transform.GetChild(2).gameObject.transform.eulerAngles;
                    break;
                case "Lobby":

                    float[] data = { 0, 0, 0, 0, 0, 0, 0, 0, lobby, 0 };
                    bpos = new byte[data.Length * 4];
                    Buffer.BlockCopy(data, 0, bpos, 0, bpos.Length);
                    clientSocket.SendTo(bpos, remoteEP);
                    Debug.Log("DataSent");

                    break;

                case "CharacterSelect":
                case "GamemodeSelect":
                    break;
                case "playerStats":
                    float[] data2 = { 0, 0, 0, 0, 0, 0 };
                    bpos = new byte[data2.Length * 4];
                    Buffer.BlockCopy(data2, 0, bpos, 0, bpos.Length);
                    clientSocket.SendTo(bpos, remoteEP);
                    Debug.Log("DataSent");
                    break;
            }


        }
    }


    IEnumerator sendEnemies(float timeInterval) //I think the data should be sent to a different port?
    {


        while (true)
        {
            yield return new WaitForSeconds(timeInterval);
            if (currentScene.name == "prototype_tutorial" || currentScene.name == "EndlessMode" || currentScene.name == "First_Level")
            try
            {
                
                    float[] enemyPosAndActive = {
                    poolManager.transform.GetChild(0).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(0).transform.position.x, poolManager.transform.GetChild(0).transform.position.y,      poolManager.transform.GetChild(0).transform.position.z, Single.Parse(poolManager.transform.GetChild(0).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(1).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(1).transform.position.x, poolManager.transform.GetChild(1).transform.position.y,      poolManager.transform.GetChild(1).transform.position.z, Single.Parse(poolManager.transform.GetChild(1).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(2).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(2).transform.position.x, poolManager.transform.GetChild(2).transform.position.y,      poolManager.transform.GetChild(2).transform.position.z, Single.Parse(poolManager.transform.GetChild(2).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(3).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(3).transform.position.x, poolManager.transform.GetChild(3).transform.position.y,      poolManager.transform.GetChild(3).transform.position.z, Single.Parse(poolManager.transform.GetChild(3).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(4).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(4).transform.position.x, poolManager.transform.GetChild(4).transform.position.y,      poolManager.transform.GetChild(4).transform.position.z, Single.Parse(poolManager.transform.GetChild(4).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(5).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(5).transform.position.x, poolManager.transform.GetChild(5).transform.position.y,      poolManager.transform.GetChild(5).transform.position.z, Single.Parse(poolManager.transform.GetChild(5).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(6).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(6).transform.position.x, poolManager.transform.GetChild(6).transform.position.y,      poolManager.transform.GetChild(6).transform.position.z, Single.Parse(poolManager.transform.GetChild(6).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(7).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(7).transform.position.x, poolManager.transform.GetChild(7).transform.position.y,      poolManager.transform.GetChild(7).transform.position.z, Single.Parse(poolManager.transform.GetChild(7).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(8).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(8).transform.position.x, poolManager.transform.GetChild(8).transform.position.y,      poolManager.transform.GetChild(8).transform.position.z, Single.Parse(poolManager.transform.GetChild(8).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(9).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(9).transform.position.x, poolManager.transform.GetChild(9).transform.position.y,      poolManager.transform.GetChild(9).transform.position.z, Single.Parse(poolManager.transform.GetChild(9).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(10).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(10).transform.position.x, poolManager.transform.GetChild(10).transform.position.y, poolManager.transform.GetChild(10).transform.position.z,  Single.Parse(poolManager.transform.GetChild(10).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(11).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(11).transform.position.x, poolManager.transform.GetChild(11).transform.position.y, poolManager.transform.GetChild(11).transform.position.z,  Single.Parse(poolManager.transform.GetChild(11).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(12).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(12).transform.position.x, poolManager.transform.GetChild(12).transform.position.y, poolManager.transform.GetChild(12).transform.position.z,  Single.Parse(poolManager.transform.GetChild(12).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(13).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(13).transform.position.x, poolManager.transform.GetChild(13).transform.position.y, poolManager.transform.GetChild(13).transform.position.z,  Single.Parse(poolManager.transform.GetChild(13).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(14).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(14).transform.position.x, poolManager.transform.GetChild(14).transform.position.y, poolManager.transform.GetChild(14).transform.position.z,  Single.Parse(poolManager.transform.GetChild(14).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(15).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(15).transform.position.x, poolManager.transform.GetChild(15).transform.position.y, poolManager.transform.GetChild(15).transform.position.z,  Single.Parse(poolManager.transform.GetChild(15).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(16).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(16).transform.position.x, poolManager.transform.GetChild(16).transform.position.y, poolManager.transform.GetChild(16).transform.position.z,  Single.Parse(poolManager.transform.GetChild(16).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(17).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(17).transform.position.x, poolManager.transform.GetChild(17).transform.position.y, poolManager.transform.GetChild(17).transform.position.z,  Single.Parse(poolManager.transform.GetChild(17).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(18).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(18).transform.position.x, poolManager.transform.GetChild(18).transform.position.y, poolManager.transform.GetChild(18).transform.position.z,  Single.Parse(poolManager.transform.GetChild(18).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(19).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(19).transform.position.x, poolManager.transform.GetChild(19).transform.position.y, poolManager.transform.GetChild(19).transform.position.z,  Single.Parse(poolManager.transform.GetChild(19).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(20).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(20).transform.position.x, poolManager.transform.GetChild(20).transform.position.y, poolManager.transform.GetChild(20).transform.position.z,  Single.Parse(poolManager.transform.GetChild(20).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(21).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(21).transform.position.x, poolManager.transform.GetChild(21).transform.position.y, poolManager.transform.GetChild(21).transform.position.z,  Single.Parse(poolManager.transform.GetChild(21).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(22).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(22).transform.position.x, poolManager.transform.GetChild(22).transform.position.y, poolManager.transform.GetChild(22).transform.position.z,  Single.Parse(poolManager.transform.GetChild(22).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(23).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(23).transform.position.x, poolManager.transform.GetChild(23).transform.position.y, poolManager.transform.GetChild(23).transform.position.z,  Single.Parse(poolManager.transform.GetChild(23).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(24).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(24).transform.position.x, poolManager.transform.GetChild(24).transform.position.y, poolManager.transform.GetChild(24).transform.position.z,  Single.Parse(poolManager.transform.GetChild(24).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(25).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(25).transform.position.x, poolManager.transform.GetChild(25).transform.position.y, poolManager.transform.GetChild(25).transform.position.z,  Single.Parse(poolManager.transform.GetChild(25).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(26).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(26).transform.position.x, poolManager.transform.GetChild(26).transform.position.y, poolManager.transform.GetChild(26).transform.position.z,  Single.Parse(poolManager.transform.GetChild(26).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(27).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(27).transform.position.x, poolManager.transform.GetChild(27).transform.position.y, poolManager.transform.GetChild(27).transform.position.z,  Single.Parse(poolManager.transform.GetChild(27).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(28).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(28).transform.position.x, poolManager.transform.GetChild(28).transform.position.y, poolManager.transform.GetChild(28).transform.position.z,  Single.Parse(poolManager.transform.GetChild(28).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(29).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(29).transform.position.x, poolManager.transform.GetChild(29).transform.position.y, poolManager.transform.GetChild(29).transform.position.z,  Single.Parse(poolManager.transform.GetChild(29).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(30).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(30).transform.position.x, poolManager.transform.GetChild(30).transform.position.y, poolManager.transform.GetChild(30).transform.position.z,  Single.Parse(poolManager.transform.GetChild(30).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,
                    poolManager.transform.GetChild(31).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(31).transform.position.x, poolManager.transform.GetChild(31).transform.position.y, poolManager.transform.GetChild(31).transform.position.z,  Single.Parse(poolManager.transform.GetChild(31).transform.gameObject.GetComponent<enemyBehavior>().getTargetPlayer())/ 100000,


                };


                    byte[] bpos = new byte[enemyPosAndActive.Length * 4];
                    Buffer.BlockCopy(enemyPosAndActive, 0, bpos, 0, bpos.Length);
                    clientSocket.SendTo(bpos, remoteEP);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    static public void playersDied()
    {
        float[] points = { Single.Parse(Application.dataPath.Split('/')[4]), pointsManager.pointsSingleton.getPoints(), 0, 0, 0 };
        byte[] bpos = new byte[points.Length * 4];
        Buffer.BlockCopy(points, 0, bpos, 0, bpos.Length);
        clientSocket.SendTo(bpos, remoteEP);
    }

    static List<RaycastResult> rayResult()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> rayRes = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, rayRes);
        return rayRes;
    }
}
