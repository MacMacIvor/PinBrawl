using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
public class ServerScipt : MonoBehaviour
{
    public GameObject playerDefault;
    public GameObject poolManager;
    public GameObject playersHolder;

    private static byte[] buffer = new byte[8192];
    private static Socket server;
    private static Socket newClientSocket;
    private static IPEndPoint client;
    private static EndPoint remoteClient;
    private static List<EndPoint> remoteClientList = new List<EndPoint>();
    private static List<EndPoint> remoteIPEPs = new List<EndPoint>();
    private static int rec = 0;
    bool isExit = false;

    public List<string> listOfPlayerNames;

    public static ServerScipt singleton = null;
    //private void Awake()
    //{
    //    if (singleton == null)
    //    {
    //        singleton = this;
    //        return;
    //    }
    //    Destroy(this);
    //}
    //
    //// Start is called before the first frame update
    //void Start()
    //{
    //    //StartCoroutine(sendToClients(0.3f));
    //    RunServer();
    //
    //    
    //}
    //
    //// Update is called once per frame
    //void Update()
    //{
    //    try
    //    {
    //        rec = server.ReceiveFrom(buffer, ref remoteClient);
    //
    //        if (remoteClient != null)
    //        {
    //            int[] values = saveNewClient(ref remoteClient);
    //            if (values[0] == 1)
    //            {
    //                createNewIPEndPoint();
    //            }
    //            float[] posTemp = new float[rec / 4];
    //            Buffer.BlockCopy(buffer, 0, posTemp, 0, rec);
    //
    //            float[] pos = { posTemp[0], posTemp[1], posTemp[2], posTemp[3], 
    //                values[1] };
    //
    //            int clientIndex = 0;
    //
    //            for (int i = 0; i < listOfPlayerNames.Count; i++) //Find the client in the list
    //            {
    //                if (listOfPlayerNames[i] == remoteClient.ToString())
    //                {
    //
    //                    clientIndex = i;
    //                    i = listOfPlayerNames.Count;
    //                }
    //            }
    //
    //            for (int y = 0; y < playersHolder.transform.childCount; y++) //Find the client's object in the list
    //            {
    //                if (playersHolder.transform.GetChild(y).name == listOfPlayerNames[clientIndex])
    //                {
    //                    if (posTemp[5] != 0)
    //                    {
    //
    //                        switch (pos[4])
    //                        {
    //                            case 0:
    //                                playersHolder.transform.GetChild(y).GetComponent<Behavior>().callBasicAttack();
    //                                break;
    //                            case 1:
    //                                playersHolder.transform.GetChild(y).GetComponent<Behavior>().callHeldPower(posTemp[5], posTemp[6]);
    //                                break;
    //                        }
    //                    }
    //                    playersHolder.transform.GetChild(y).transform.position = new Vector3(pos[0], pos[1], pos[2]);
    //                    playersHolder.transform.GetChild(y).gameObject.transform.GetChild(2).gameObject.transform.eulerAngles = new Vector3(playersHolder.transform.GetChild(y).gameObject.transform.GetChild(2).gameObject.transform.eulerAngles.x, pos[3], playersHolder.transform.GetChild(y).gameObject.transform.GetChild(2).gameObject.transform.eulerAngles.z);
    //
    //                    y = playersHolder.transform.childCount;
    //                }
    //            }
    //
    //
    //            Buffer.BlockCopy(buffer, 0, pos, 0, rec);
    //
    //            sendData(ref pos, clientIndex);
    //
    //        }
    //
    //
    //    }
    //    catch (SocketException e)
    //    {
    //
    //    }
    //
    //
    //}
    //
    //public static void RunServer()
    //{
    //    IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
    //    //IPAddress ip = host.AddressList[1];
    //    IPAddress ip = IPAddress.Parse("127.0.0.1");
    //    Console.WriteLine("UDP serverName: " + host.HostName + "IP: " + ip);
    //    IPEndPoint localEP = new IPEndPoint(ip, 11111);
    //
    //
    //    server = new Socket(ip.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
    //    client = new IPEndPoint(IPAddress.Any, 0);
    //    newClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    //
    //    remoteClient = (EndPoint)client;
    //    server.Bind(localEP);
    //    server.Blocking = false;
    //
    //    Console.WriteLine("UDP server Setup Complete");
    //}
    //
    //
    //public static int[] saveNewClient(ref EndPoint theEndPoint)
    //{
    //    int[] values = { 0, 0 };
    //    try
    //    {
    //        if (remoteClientList != null)
    //        {
    //            for (int i = 0; i < remoteClientList.Count; i++)
    //            {
    //                if (remoteClientList[i].ToString() == theEndPoint.ToString())
    //                {
    //                    values[1] = i;
    //                    Debug.Log("OldClientFound");
    //                    return values;
    //
    //                }
    //            }
    //        }
    //
    //        //Should create a new player later on
    //        remoteClientList.Add(theEndPoint);
    //        values[0] = 1;
    //        values[1] = remoteClientList.Count - 1;
    //
    //        GameObject newPlayer = Instantiate(ServerScipt.singleton.playerDefault, ServerScipt.singleton.playersHolder.transform);
    //        newPlayer.name = theEndPoint.ToString();
    //        ServerScipt.singleton.listOfPlayerNames.Add(theEndPoint.ToString());
    //        Debug.Log("NewClientFound");
    //        return values;
    //    }
    //    catch (Exception e)
    //    {
    //        return values;
    //    }
    //}
    //
    //public static void createNewIPEndPoint()
    //{
    //    Console.WriteLine("New UDP client connected");
    //    Console.WriteLine(remoteClientList[(remoteClientList.Count - 1)].GetHashCode());
    //    Console.WriteLine(remoteClientList[(remoteClientList.Count - 1)].GetType());
    //    Console.WriteLine(remoteClientList[(remoteClientList.Count - 1)]);
    //
    //    string[] ipString = remoteClientList[(remoteClientList.Count - 1)].ToString().Split(':');
    //    IPAddress ip = IPAddress.Parse(ipString[0]);
    //    IPEndPoint remoteEP = new IPEndPoint(ip, (int)Single.Parse(ipString[1]));
    //    remoteIPEPs.Add(remoteEP);
    //
    //
    //}
    //
    //public static void sendData(ref float[] pos, int intToNotInclude)
    //{
    //
    //    byte[] bytes = new byte[pos.Length * 4];
    //
    //    Buffer.BlockCopy(pos, 0, bytes, 0, bytes.Length);
    //    for (int i = 0; i < remoteIPEPs.Count; i++)
    //    {
    //        if (intToNotInclude != i)
    //        {
    //            newClientSocket.SendTo(bytes, remoteIPEPs[i]);
    //        }
    //    }
    //}

    //IEnumerator sendToClients(float timeInterval) //I think the data should be sent to a different port?
    //{
    //    
    //
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(timeInterval);
    //        try
    //        {
    //            for (int i = 0; i < remoteIPEPs.Count; i++)
    //            {
    //
    //                Debug.Log(poolManager.transform.childCount);
    //                float[] enemyPosAndActive = {
    //                poolManager.transform.GetChild(0).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(0).transform.position.x, poolManager.transform.GetChild(0).transform.position.y, poolManager.transform.GetChild(0).transform.position.z,
    //                poolManager.transform.GetChild(1).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(1).transform.position.x, poolManager.transform.GetChild(1).transform.position.y, poolManager.transform.GetChild(1).transform.position.z,
    //                poolManager.transform.GetChild(2).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(2).transform.position.x, poolManager.transform.GetChild(2).transform.position.y, poolManager.transform.GetChild(2).transform.position.z,
    //                poolManager.transform.GetChild(3).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(3).transform.position.x, poolManager.transform.GetChild(3).transform.position.y, poolManager.transform.GetChild(3).transform.position.z,
    //                poolManager.transform.GetChild(4).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(4).transform.position.x, poolManager.transform.GetChild(4).transform.position.y, poolManager.transform.GetChild(4).transform.position.z,
    //                poolManager.transform.GetChild(5).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(5).transform.position.x, poolManager.transform.GetChild(5).transform.position.y, poolManager.transform.GetChild(5).transform.position.z,
    //                poolManager.transform.GetChild(6).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(6).transform.position.x, poolManager.transform.GetChild(6).transform.position.y, poolManager.transform.GetChild(6).transform.position.z,
    //                poolManager.transform.GetChild(7).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(7).transform.position.x, poolManager.transform.GetChild(7).transform.position.y, poolManager.transform.GetChild(7).transform.position.z,
    //                poolManager.transform.GetChild(8).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(8).transform.position.x, poolManager.transform.GetChild(8).transform.position.y, poolManager.transform.GetChild(8).transform.position.z,
    //                poolManager.transform.GetChild(9).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(9).transform.position.x, poolManager.transform.GetChild(9).transform.position.y, poolManager.transform.GetChild(9).transform.position.z,
    //                poolManager.transform.GetChild(10).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(10).transform.position.x, poolManager.transform.GetChild(10).transform.position.y, poolManager.transform.GetChild(10).transform.position.z,
    //                poolManager.transform.GetChild(11).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(11).transform.position.x, poolManager.transform.GetChild(11).transform.position.y, poolManager.transform.GetChild(11).transform.position.z,
    //                poolManager.transform.GetChild(12).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(12).transform.position.x, poolManager.transform.GetChild(12).transform.position.y, poolManager.transform.GetChild(12).transform.position.z,
    //                poolManager.transform.GetChild(13).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(13).transform.position.x, poolManager.transform.GetChild(13).transform.position.y, poolManager.transform.GetChild(13).transform.position.z,
    //                poolManager.transform.GetChild(14).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(14).transform.position.x, poolManager.transform.GetChild(14).transform.position.y, poolManager.transform.GetChild(14).transform.position.z,
    //                poolManager.transform.GetChild(15).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(15).transform.position.x, poolManager.transform.GetChild(15).transform.position.y, poolManager.transform.GetChild(15).transform.position.z,
    //                poolManager.transform.GetChild(16).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(16).transform.position.x, poolManager.transform.GetChild(16).transform.position.y, poolManager.transform.GetChild(16).transform.position.z,
    //                poolManager.transform.GetChild(17).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(17).transform.position.x, poolManager.transform.GetChild(17).transform.position.y, poolManager.transform.GetChild(17).transform.position.z,
    //                poolManager.transform.GetChild(18).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(18).transform.position.x, poolManager.transform.GetChild(18).transform.position.y, poolManager.transform.GetChild(18).transform.position.z,
    //                poolManager.transform.GetChild(19).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(19).transform.position.x, poolManager.transform.GetChild(19).transform.position.y, poolManager.transform.GetChild(19).transform.position.z,
    //                poolManager.transform.GetChild(20).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(20).transform.position.x, poolManager.transform.GetChild(20).transform.position.y, poolManager.transform.GetChild(20).transform.position.z,
    //                poolManager.transform.GetChild(21).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(21).transform.position.x, poolManager.transform.GetChild(21).transform.position.y, poolManager.transform.GetChild(21).transform.position.z,
    //                poolManager.transform.GetChild(22).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(22).transform.position.x, poolManager.transform.GetChild(22).transform.position.y, poolManager.transform.GetChild(22).transform.position.z,
    //                poolManager.transform.GetChild(23).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(23).transform.position.x, poolManager.transform.GetChild(23).transform.position.y, poolManager.transform.GetChild(23).transform.position.z,
    //                poolManager.transform.GetChild(24).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(24).transform.position.x, poolManager.transform.GetChild(24).transform.position.y, poolManager.transform.GetChild(24).transform.position.z,
    //                poolManager.transform.GetChild(25).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(25).transform.position.x, poolManager.transform.GetChild(25).transform.position.y, poolManager.transform.GetChild(25).transform.position.z,
    //                poolManager.transform.GetChild(26).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(26).transform.position.x, poolManager.transform.GetChild(26).transform.position.y, poolManager.transform.GetChild(26).transform.position.z,
    //                poolManager.transform.GetChild(27).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(27).transform.position.x, poolManager.transform.GetChild(27).transform.position.y, poolManager.transform.GetChild(27).transform.position.z,
    //                poolManager.transform.GetChild(28).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(28).transform.position.x, poolManager.transform.GetChild(28).transform.position.y, poolManager.transform.GetChild(28).transform.position.z,
    //                poolManager.transform.GetChild(29).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(29).transform.position.x, poolManager.transform.GetChild(29).transform.position.y, poolManager.transform.GetChild(29).transform.position.z,
    //                poolManager.transform.GetChild(30).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(30).transform.position.x, poolManager.transform.GetChild(30).transform.position.y, poolManager.transform.GetChild(30).transform.position.z,
    //                poolManager.transform.GetChild(31).transform.gameObject.GetComponent<enemyBehavior>().isActive(), poolManager.transform.GetChild(31).transform.position.x, poolManager.transform.GetChild(31).transform.position.y, poolManager.transform.GetChild(31).transform.position.z,
    //
    //
    //            };
    //
    //                Debug.Log(poolManager.transform.GetChild(0).transform.gameObject.GetComponent<enemyBehavior>().isActive());
    //
    //                byte[] bytes = new byte[enemyPosAndActive.Length * 4];
    //                Buffer.BlockCopy(enemyPosAndActive, 0, bytes, 0, bytes.Length);
    //
    //                newClientSocket.SendTo(bytes, remoteIPEPs[i]);
    //
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Debug.Log(e);
    //        }
    //    }
    //}

    public string getPlayerNames()
    {
        string returnS = "";
        if (listOfPlayerNames.Count != 0)
        {
            returnS = listOfPlayerNames[UnityEngine.Random.Range(0, listOfPlayerNames.Count - 1)];
        }
        return returnS;
    }
}
