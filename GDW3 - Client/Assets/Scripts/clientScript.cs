using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
public class clientScript : MonoBehaviour
{
    public GameObject myCube;
    private string h;

    private static byte[] outBuffer;
    private static IPEndPoint remoteEP;
    private static Socket clientSocket;
    private float interval;
    [Range(0, 0.4f)]
    public float intervals = 0.2f;
    private Vector3 lastPosition;
    private Vector3 lastRotation;
    private float attackStrength = 0;
    private float isBasic = 0;
    private float directionServer = 0;
    byte[] bpos;

    private static byte[] inBuffer;
    private static EndPoint endpoint;

    public GameObject poolManager;


    public static clientScript singleton;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            return;
        }
        Destroy(this);
    }

    public static void RunClient(Vector3 pos)
    {
        
        try
        {
            IPAddress ip = IPAddress.Parse("10.0.0.23");
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
        RunClient(myCube.gameObject.transform.position);
        StartCoroutine(sendServer(interval));
    }

    // Update is called once per frame
    void Update()
    {
        //h = "Testing........INFR3830\n" +
        //        "Cube position: " + myCube.gameObject.transform.position.x.ToString() + ", "
        //        + myCube.gameObject.transform.position.y.ToString() + ", " + myCube.gameObject.transform.position.z.ToString() + "\n";
        //h = myCube.gameObject.transform.position.x.ToString() + "," + myCube.gameObject.transform.position.y.ToString() + "," + myCube.gameObject.transform.position.z.ToString();
        //  outBuffer = Encoding.ASCII.GetBytes(h);
        //clientSocket.SendTo(outBuffer, remoteEP);

        try
        {
            int rec = clientSocket.ReceiveFrom(inBuffer, ref endpoint);
            float[] pos = new float[rec / 4];
            Buffer.BlockCopy(inBuffer, 0, pos, 0, rec);

            for (int i = 0; i < 32; i++)
            {
                if (pos[4 * i] == 1)
                {
                    if (poolManager.transform.GetChild(i).transform.gameObject.GetComponent<enemyBehavior>().isActive() == 1)
                    {
                        poolManager.transform.GetChild(i).transform.position = new Vector3(pos[1 + i * 4], pos[2 + i * 4], pos[3 + i * 4]);

                    }
                    else
                    {
                        switch ((Mathf.Floor(i / 8)))
                        {
                            case 0:
                                EnemyPoolManager.singleton.GetsmallMelee(new Vector3(pos[1 + i * 4], pos[2 + i * 4], pos[3 + i * 4]));
                                break;
                            case 1:
                                EnemyPoolManager.singleton.GetSmallShooter(new Vector3(pos[1 + i * 4], pos[2 + i * 4], pos[3 + i * 4]));
                                break;
                            case 2:
                                EnemyPoolManager.singleton.GetLargeRange(new Vector3(pos[1 + i * 4], pos[2 + i * 4], pos[3 + i * 4]));
                                break;
                            case 3:
                                EnemyPoolManager.singleton.GetBuffer(new Vector3(pos[1 + i * 4], pos[2 + i * 4], pos[3 + i * 4]));
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


        }
        catch (Exception e)
        {
            ;


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
            if (lastPosition != myCube.transform.position || lastRotation != myCube.gameObject.transform.GetChild(2).gameObject.transform.eulerAngles)
            {
                float[] pos = { myCube.transform.position.x, myCube.transform.position.y, myCube.transform.position.z, myCube.gameObject.transform.GetChild(2).gameObject.transform.eulerAngles.y, isBasic, attackStrength, directionServer };
                bpos = new byte[pos.Length * 4];
                Buffer.BlockCopy(pos, 0, bpos, 0, bpos.Length);
                outBuffer = Encoding.ASCII.GetBytes(myCube.transform.position.x.ToString());
                clientSocket.SendTo(bpos, remoteEP);
                Debug.Log("DataSent");
                attackStrength = 0;
            }
            lastPosition = myCube.transform.position;
            lastRotation = myCube.gameObject.transform.GetChild(2).gameObject.transform.eulerAngles;
        }
    }

}
