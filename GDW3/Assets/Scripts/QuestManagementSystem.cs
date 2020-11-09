using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManagementSystem : MonoBehaviour
{
    public static QuestManagementSystem singleton = null;

    public GameObject player;

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
        listOfQuests = new Queue<questStructure>();
        string hello = "Kill the enemy";
        string hello2 = "Use left mouse button to attack the enemy\nmove around with WASD";
        listOfQuests.Enqueue(new questStructure(hello, hello2, 0, 1));
        hello = "Go to destination";
        hello2 = "Move to the left";
        listOfQuests.Enqueue(new questStructure(hello, hello2, 1, 1));
        hello = "Kill the enemy";
        hello2 = "You can use rightMouse to use a\ncharged attack";
        listOfQuests.Enqueue(new questStructure(hello, hello2, 0, 2));
        hello2 = "Move to the right";
        listOfQuests.Enqueue(new questStructure(hello, hello2, 1, 1));
        hello = "Push the two buttons";
        hello2 = "Use q to push the two buttons at the top";
        listOfQuests.Enqueue(new questStructure(hello, hello2, 2, 2, 2));
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
                //Jump to next level/end of game
                //Hence, nothing yet
                break;
        }
    }

    public void createQuest(string Title, string description, int winCondition)
    {
        listOfQuests.Enqueue(new questStructure(Title, description, winCondition));
    }
    public void updateQuest(int number)
    {
        //TODO: FIGURE IT OUT
        //The number passed referes to a certain possible quest
        //ex. killing an enemy bring you closer to a maybe clearing a room quest and passes a 0, lowering the amount of enemies left by 1
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
                    //pressButton()
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
                break;
            case 4:
                break;
        }
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
            if (Vector3.Distance(player.transform.position, destination) < 10)
            {
                state = stateOfQuestManager.COMPLETED;
                destination = new Vector3(0, 00, 0);
            }
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
}
