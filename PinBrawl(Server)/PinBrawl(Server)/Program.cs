using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace PinBrawl_Server_
{
    //Server needs to 
    //Detect where a player is (search, lobby or in game)
    //Hold a list of all players
    //Have a list of all lobbies
    //Send the list of current lobbies to the players
    //Allow the player to join a lobby or not
    //Play the game (receive and send player position and enemy)
    //Receive score and save it
    //Chat
    class Servers
    {
        //A lobby should be a int number that holds up to 4 other int numbers
        private static byte[] buffer = new byte[1024];
        private static Socket server;
        private static Socket newClientSocket;
        private static IPEndPoint client;
        private static EndPoint remoteClient;
        private static List<EndPoint> remoteClientList = new List<EndPoint>();
        private static List<EndPoint> remoteIPEPs = new List<EndPoint>();
        private static List<string> listOfPlayerNames = new List<string>();
        private static int rec = 0;

        private static List<List<int>> Lobbies = new List<List<int>>();

        private static float[] scoreList = new float[10];

        public static void startUDPServer()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ip = IPAddress.Parse("127.0.0.1");//host.AddressList[1];

            Console.WriteLine("UDP serverName: " + host.HostName + "IP: " + ip);
            IPEndPoint localEP = new IPEndPoint(ip, 11111);


            server = new Socket(ip.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            client = new IPEndPoint(IPAddress.Any, 0);
            newClientSocket = new Socket(ip.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

            remoteClient = (EndPoint)client;
            server.Bind(localEP);
            server.Blocking = false;
                       
            Console.WriteLine("UDP server Setup Complete");
        }

        public static void runServers()
        {
            //The start ups
            startUDPServer();
            loadScores(ref scoreList);

            do
            {
                //Udp server
                udpLoop();
                

            } while (true == true);

        }

        public static void udpLoop()
        {
            

            try
            {
                rec = server.ReceiveFrom(buffer, ref remoteClient); //Receive data from a client

                Console.WriteLine("GotInformationfrom: " + remoteClient.ToString());

                //Client should send (so server should receive)
                //Where it currently is (position)
                //Attack, this is two things basic or charged and the amount of damage
                //It's orientation
                //Lobby it's in

                //Server should send back all of these
                //Plus the name at the last position


                if (remoteClient != null)
                {
                    //Values is an array of size of 2, first index being either 0 for no new client or a 1 for a new client, second index is the size of the array for the amount of saved players
                    int[] values = saveNewClient(ref remoteClient); //Saves a new client if need be


                    if (values[0] == 1)
                    {
                        createNewIPEndPoint(); //Saves the endpoint of the new client if it is new
                    }
                    float[] posTemp = new float[rec / 4];
                    Buffer.BlockCopy(buffer, 0, posTemp, 0, rec);

                    string[] ip = (remoteClient.ToString().Split(':'));
                    string final = ((ip[0].ToString().Split('.')[0]) + ip[0].ToString().Split('.')[1]) + ip[0].ToString().Split('.')[2] + ip[0].ToString().Split('.')[3] + ip[1]; //This is the final pure number of the client 

                    int clientIndex = 0;

                    for (int i = 0; i < listOfPlayerNames.Count; i++) //Find the client that sent the into in the list
                    {
                        if (listOfPlayerNames[i] == remoteClient.ToString())
                        {

                            clientIndex = i;
                            i = listOfPlayerNames.Count;
                        }
                    }

                    if (posTemp.Length == 10) // Means it's player data rather then enemy data
                    {

                        
                        float[] pos = {
                                    posTemp[0], //X position 
                                    posTemp[1], //Y position
                                    posTemp[2], //Z position
                                    posTemp[3], //Orientation of player
                                    posTemp[4], //Is Basic or Charged
                                    posTemp[5], //Amount of damage
                                    posTemp[6], //Orientation of attack
                                    posTemp[7], //Is Basic or Charged
                                    posTemp[8], //Current lobby          // 0 - in the searching scene
                                    posTemp[9], //Is in game             // 0 - in the character scene
                                    Single.Parse(final) / 100000 //The ip or name of user
                                    };

                        
                        if (posTemp[8] == 0) //Since the player is in the lobby searching screen then we send back available lobbies
                        {
                            float[] listOfLobbies = new float[Lobbies.Count * 2 + 2]; //if there are 4, 0,1  2,3,  4,5  6,7
                            for (int i = 0; i < Lobbies.Count; i++)
                            {
                                listOfLobbies[i * 2 + 2] = Lobbies[i][0]; //The lobby id
                                listOfLobbies[i * 2 + 1 + 2] = Lobbies[i].Count - 1; //Amount of people in the lobby
                            }
                            Buffer.BlockCopy(buffer, 0, listOfLobbies, 0, listOfLobbies.Length);
                            sendLobbyList(ref listOfLobbies, clientIndex);

                        }
                        else
                        {
                            for (int i = 0; i < Lobbies.Count; i++)
                            {
                                for (int y = 1; y < Lobbies[i].Count; y++)
                                {
                                    if (remoteIPEPs[Lobbies[i][y]].ToString() == remoteClient.ToString())
                                    {
                                        Buffer.BlockCopy(buffer, 0, pos, 0, pos.Length);
                                        sendToLobbyWithIndex(ref pos, clientIndex, 0, i);
                                    }
                                }
                            }

                        }
                    }
                    else if (posTemp.Length == 2){ //The client is asking to join a lobby // 0 is the lobby code, 1 is if it's a new lobby
                        switch (posTemp[1])
                        {
                            case 1: //Create new lobby
                                List<int> newLobby = new List<int>();
                                newLobby.Add((int)posTemp[0]);
                                int why = clientIndex;                                
                                newLobby.Add(why);
                                Lobbies.Add(newLobby);

                                float[] toSend2 = { newLobby[0] };
                                Buffer.BlockCopy(buffer, 0, toSend2, 0, toSend2.Length);
                                sendLobbyList(ref toSend2, clientIndex);
                                break;
                            case 0: //Join a lobby
                                for (int i = 0; i < Lobbies.Count; i++)
                                {
                                    if (Lobbies[i][0] == posTemp[0])
                                    {
                                        if(Lobbies[i].Count != 5)
                                        {
                                            //They can join the lobby since it's not full
                                            //Add the client to the list
                                            Lobbies[i].Add(clientIndex);

                                            //Send back to the client that they joined
                                            float[] toSend = { Lobbies[i][0] };
                                            Buffer.BlockCopy(buffer, 0, toSend, 0, toSend.Length);
                                            sendLobbyList(ref toSend, clientIndex);
                                            
                                            //We can exit the loop
                                            i = Lobbies.Count;
                                        }
                                    }
                                }
                                break;
                        }
                        
                    }
                    else if (posTemp.Length == 3 || posTemp.Length == 4)
                    {

                        sendToLobby(ref posTemp, clientIndex, 1);

                    }
                    else if (posTemp.Length == 160)
                    {
                        for (int i = 0; i < Lobbies.Count; i++)
                        {
                            if (Lobbies[i][1] == clientIndex && remoteIPEPs[Lobbies[i][1]].ToString() == remoteClient.ToString())
                            {
                                try
                                {
                                    int z = 0;
                                    string theIP = remoteClient.ToString().Split(':')[0];
                                    string nameToReturn = theIP.Split('.')[0] + theIP.Split('.')[1] + theIP.Split('.')[2] + theIP.Split('.')[3] + remoteClient.ToString().Split(':')[1];

                                    do
                                    {
                                        if (posTemp[4 + z * 5] == 0)
                                        {
                                            posTemp[4 + z * 5] = Single.Parse(nameToReturn) / 100000;
                                        }
                                        z++;
                                    } while (true);
                                }
                                catch (Exception e)
                                {

                                }
                                sendToLobbyWithIndex(ref posTemp, clientIndex, 0, i);
                            }
                        }
                    }
                    else if (posTemp.Length == 5)
                    {
                        for (int i = 0; i < Lobbies.Count; i++)
                        {
                            if (Lobbies[i][1] == clientIndex && remoteIPEPs[Lobbies[i][1]].ToString() == remoteClient.ToString())
                            {
                                saveScores(posTemp);
                                sendToLobbyWithIndex(ref posTemp, clientIndex, 1, i);
                                Lobbies.RemoveAt(i); //Lobby is now done
                            }
                        }
                    }
                    else if (posTemp.Length == 6)
                    {
                        byte[] bytes = new byte[scoreList.Length * 4];

                        Buffer.BlockCopy(scoreList, 0, bytes, 0, bytes.Length);
                        newClientSocket.SendTo(bytes, remoteIPEPs[clientIndex]);
                    }
                }


            }
            catch (SocketException e)
            {

            }
        }

        public static int[] saveNewClient(ref EndPoint theEndPoint)
        {
            int[] values = { 0, 0 };
            try
            {
                if (remoteClientList != null)
                {
                    for (int i = 0; i < remoteClientList.Count; i++)
                    {
                        if (remoteClientList[i].ToString() == theEndPoint.ToString())
                        {
                            values[1] = i;
                            return values;

                        }
                    }
                }

                //Should create a new player later on
                remoteClientList.Add(theEndPoint);
                listOfPlayerNames.Add(theEndPoint.ToString());
                values[0] = 1;
                values[1] = remoteClientList.Count - 1;

                return values;
            }
            catch (Exception e)
            {
                return values;
            }
        }

        public static void createNewIPEndPoint()
        {
            Console.WriteLine("New UDP client connected");
            Console.WriteLine(remoteClientList[(remoteClientList.Count - 1)].GetHashCode());
            Console.WriteLine(remoteClientList[(remoteClientList.Count - 1)].GetType());
            Console.WriteLine(remoteClientList[(remoteClientList.Count - 1)]);

            string[] ipString = remoteClientList[(remoteClientList.Count - 1)].ToString().Split(':');
            IPAddress ip = IPAddress.Parse(ipString[0]);
            IPEndPoint remoteEP = new IPEndPoint(ip, (int)Single.Parse(ipString[1]));
            remoteIPEPs.Add(remoteEP);


        }

        public static void sendData(ref float[] pos, int intToNotInclude)
        {

            byte[] bytes = new byte[pos.Length * 4];

            Buffer.BlockCopy(pos, 0, bytes, 0, bytes.Length);
            for (int i = 0; i < remoteIPEPs.Count; i++)
            {
                if (intToNotInclude != i)
                {
                    newClientSocket.SendTo(bytes, remoteIPEPs[i]);
                }
            }
        }

        public static void sendLobbyList(ref float[] pos, int intToSendTo)
        {
            byte[] bytes = new byte[pos.Length * 4];

            Buffer.BlockCopy(pos, 0, bytes, 0, bytes.Length);
            for (int i = 0; i < remoteIPEPs.Count; i++)
            {
                if (intToSendTo == i)
                {
                    newClientSocket.SendTo(bytes, remoteIPEPs[i]);
                    i = remoteIPEPs.Count;
                }
            }
        }

        public static void sendToLobby(ref float[] pos, int intToNotSendTo, int everyoneOrNo)
        {

            byte[] bytes = new byte[pos.Length * 4];

            Buffer.BlockCopy(pos, 0, bytes, 0, bytes.Length);
            for (int y = 0; y < Lobbies.Count; y++)
            {
                if(Lobbies[y][0] == pos[0])
                {
                    for (int i = 1; i < Lobbies[y].Count; i++)
                    {
                        switch (everyoneOrNo)
                        {
                            case 0:
                                if (intToNotSendTo != Lobbies[y][i])
                                {
                                    newClientSocket.SendTo(bytes, remoteIPEPs[Lobbies[y][i]]);
                                }
                                break;
                            case 1:
                                newClientSocket.SendTo(bytes, remoteIPEPs[Lobbies[y][i]]);
                                break;
                        }
                    }
                }
            }
        }

        public static void sendToLobbyWithIndex(ref float[] pos, int intToNotSendTo, int everyoneOrNo, int lobbyIndex)
        {

            byte[] bytes = new byte[pos.Length * 4];

            Buffer.BlockCopy(pos, 0, bytes, 0, bytes.Length);
            for (int i = 1; i < Lobbies[lobbyIndex].Count; i++)
            {
                switch (everyoneOrNo)
                {
                    case 0:
                        if (intToNotSendTo != Lobbies[lobbyIndex][i])
                        {
                            newClientSocket.SendTo(bytes, remoteIPEPs[Lobbies[lobbyIndex][i]]);
                        }
                        break;
                    case 1:
                        newClientSocket.SendTo(bytes, remoteIPEPs[Lobbies[lobbyIndex][i]]);
                        break;
                }
            }
        }

        public static void loadScores(ref float[] scoreList)
        {
            //string filePath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "PinBrawl(Server)\\Highscores.txt");
            string filePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            int indexing = filePath.IndexOf("PinBrawl(Server).dll");
            filePath = filePath.Remove(indexing) + "Highscores.txt";
            string theLine;
            int i = 0;
            using (var line = new StreamReader(filePath))
            {
                while ((theLine = line.ReadLine()) != null) //Set up this way in case in the future we want to add times for each individual level
                {
                    if (theLine != "")
                    {
                        scoreList[i] = float.Parse(theLine);
                        i++;
                    }
                }

            }
            string startupPatih = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "PinBrawl(Server)\\Highscores.txt");

        }

        public static void saveScores(float[] newValues)
        {
            string filePath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "PinBrawl(Server)\\Highscores.txt");

            for (int i = 1; i < 10; i += 2)
            {
                if (newValues[1] > scoreList[i])
                {
                    for (int y = 9; y > i; y--)
                    {
                        scoreList[y] = scoreList[y - 2];
                    }
                    scoreList[i] = newValues[1];
                    scoreList[i - 1] = newValues[0];
                    i = 10;
                    using (StreamWriter outputFile = new StreamWriter(filePath))
                    {
                        //outputFile.Flush();
                        for (int z = 0; z < 10; z++)
                        {
                            if (z != -1)
                            {
                                outputFile.Write(scoreList[z] + "\n");
                            }
                            else
                            {
                                outputFile.WriteLine(scoreList[z]);

                            }
                        }
                    }
                }
                
            }
            
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            runServers();
            Console.WriteLine("Hello World!");
            
        }
    }
}
