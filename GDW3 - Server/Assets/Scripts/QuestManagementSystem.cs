using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManagementSystem : MonoBehaviour
{
    public static QuestManagementSystem singleton = null;

    //public GameObject player;

    Queue<questStructure> listOfQuests;
    enum stateOfQuestManager
    {
        START,
        INPROGRESS,
        COMPLETED,
        WIN
    }
    stateOfQuestManager state;

    public Text Title;
    public Text description;
    public Text enemiesLeft;

    int amountOfEnemiesLeftToKill = 0;
    int buttonsLeftToPush = 0;
    Vector3 destination = new Vector3(0, 0, 0);
    static int amountOfDestinations = -1;
    int amountOfQuestsCompleted = 0;

    [Range(0, 10)]
    public int level = 0;

    public void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            return;
        }
        Destroy(this);
    }

    struct questStructure
    {
        public questStructure(string names = "", string descriptions = "", int winConditions = 0, int amountToKills = 0, int buttonsToPushs = 0)
        {
            name = names;
            description = descriptions;
            winCondition = winConditions;
            amountToKill = amountToKills;
            buttonsToPush = buttonsToPushs;
        }
        public string name;
        public string description;
        public int winCondition;
        public int amountToKill;
        public int buttonsToPush;
    }

    //Quest manager needs to have the quests
    //Needs to show current quest
    //Needs to be able to go to the next quest

    //Data needed
    //The Quest
    //Description of it


    //Quests will be loaded from a file later on



    // Start is called before the first frame update
    void Start()
    {
        state = stateOfQuestManager.START;
        switch (level)
        {
            case 0:

                listOfQuests = new Queue<questStructure>();
                string hello = "Kill the enemy";
                string hello2 = "Use left mouse button to\nattack the enemy\nmove around with WASD";
                listOfQuests.Enqueue(new questStructure(hello, hello2, 0, 1));
                hello = "Go to destination";
                hello2 = "Move to the left";
                listOfQuests.Enqueue(new questStructure(hello, hello2, 1, 1));
                hello = "Kill the enemies";
                hello2 = "You can use rightMouse to use a\ncharged attack";
                listOfQuests.Enqueue(new questStructure(hello, hello2, 0, 2));
                hello = "Go to destination";
                hello2 = "Move to the right";
                listOfQuests.Enqueue(new questStructure(hello, hello2, 1, 1));
                hello = "Push the two buttons";
                hello2 = "Use Q to push the two buttons at the top";
                listOfQuests.Enqueue(new questStructure(hello, hello2, 2, 2, 2));
                listOfQuests.Enqueue(new questStructure(hello, hello2, 3, 0));
                break;
            case 1:

                listOfQuests = new Queue<questStructure>();
                string hello3 = "Activate three generators\nSearch the level";
                string hello4 = "Press Q to activate\nwhen near";
                listOfQuests.Enqueue(new questStructure(hello3, hello4, 2, 0, 3));


                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case stateOfQuestManager.START:
                setQuest(listOfQuests.Peek().winCondition);
                updateUI();
                state = stateOfQuestManager.INPROGRESS;
                break;
            case stateOfQuestManager.INPROGRESS:
                ;// :(
                break;
            case stateOfQuestManager.COMPLETED:
                questComplete();
                break;
            case stateOfQuestManager.WIN:
                Debug.Log("YOU WON!!!!!!!!!!");
                toEndScene.singleton.callNext();
                break;
        }
    }

    public void createQuest(string Title, string description, int winCondition)
    {
        listOfQuests.Enqueue(new questStructure(Title, description, winCondition));
    }
    public void updateQuest(int number)
    {
        if (listOfQuests.Count > 0)
        {
            switch (number)
            {
                case 0:
                    killAllQuest(number);
                    break;
                case 1:
                    reachDestinationQuest();
                    break;
                case 2:
                    buttonPush(number);
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }


        updateUI();
        }
    }
    void questComplete()
    {
        listOfQuests.Dequeue();
        state = checkIfWon() ? stateOfQuestManager.WIN : stateOfQuestManager.START;
        amountOfQuestsCompleted++;
    }
    void updateUI()
    {

        //Title
        Title.text = listOfQuests.Peek().name;
        //Description
        description.text = listOfQuests.Peek().description;

        enemiesLeft.text = getSpecialNumber().ToString();

    }

    void setQuest(int number)
    {
        switch (number)
        {
            case 0:
                killAllQuest(listOfQuests.Peek().amountToKill);
                break;
            case 1:
                reachDestinationQuest();
                break;
            case 2:
                buttonPush(listOfQuests.Peek().buttonsToPush);
                break;
            case 3:
                nextLevel();
                break;
            case 4:
                break;
        }
    }

    public int returnQuestType()
    {
        return listOfQuests.Peek().winCondition;
    }

    void killAllQuest(int setOrUpdate)
    {
        if (setOrUpdate != 0)
        {
            amountOfEnemiesLeftToKill = setOrUpdate;
            return;
        }
        amountOfEnemiesLeftToKill--;
        if (amountOfEnemiesLeftToKill == 0)
        {
            state = stateOfQuestManager.COMPLETED;
        }
        //updateUI();
    }

    void reachDestinationQuest()
    {
        if (destination.y == 0)
        {
            destination = getNextDestination();
        }
        else
        {
            //if (Vector3.Distance(player.transform.position, destination) < 10)
            //{
            //    state = stateOfQuestManager.COMPLETED;
            //    destination = new Vector3(0, 00, 0);
            //}
        }
    }

    void buttonPush(int setOrUpdate)
    {
        if (buttonsLeftToPush == 0)
        {
            buttonsLeftToPush = setOrUpdate;
            return;
        }
        buttonsLeftToPush--;
        if (buttonsLeftToPush == 0)
        {
            state = stateOfQuestManager.COMPLETED;
        }
        //updateUI();
    }

    Vector3 getNextDestination()
    {
        amountOfDestinations++;
        return GameObject.FindGameObjectsWithTag("Destination")[amountOfDestinations].transform.position;
    }

    void nextLevel()
    {
        level++;
        //Have a the singleton here for the next level screen
        toNextLevel.singleton.callNext(level);
    }

    int getSpecialNumber()
    {
        switch (listOfQuests.Peek().winCondition)
        {
            case 0:
                return amountOfEnemiesLeftToKill;
            case 1:
                return 0; //none needed
            case 2:
                return buttonsLeftToPush;
            case 3:
                break;
            case 4:
                break;
        }
        return 9999; //If we see this it's an error
    }

    bool checkIfWon()
    {
        return listOfQuests.Count == 0;
    }

    public int getAmountOfQuestsCompleted()
    {
        return amountOfQuestsCompleted;
    }

    public void forceQuestUpdate() //This should never be used unless the game is being loaded
    {
        if (listOfQuests.Peek().winCondition == 3)
        {
            listOfQuests.Dequeue();
            nextLevel();
        }
        else
        {
            listOfQuests.Dequeue();
            state = checkIfWon() ? stateOfQuestManager.WIN : stateOfQuestManager.START;
            amountOfQuestsCompleted++;
            amountOfEnemiesLeftToKill = 0;
            destination = new Vector3(0, 00, 0);
            buttonsLeftToPush = 0;

        }
    }
}
