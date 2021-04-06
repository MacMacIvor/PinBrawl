using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPush : MonoBehaviour
{
    public LayerMask playerLayer;
    bool qPressed = false;
    bool wasUsed = false;
    [Range(0, 50)]
    public float theDist = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (pauseGame.singleton.stateOfGame)
        {
            case pauseGame.generalState.PAUSED:
                break;
            case pauseGame.generalState.PLAYING:
                if (Input.GetKey(KeyCode.Q))
                    qPressed = true;
                else
                    qPressed = false;

                if (wasUsed == false)
                {
                    Collider[] playerHit = Physics.OverlapSphere(transform.position, theDist, playerLayer); //Change to just basicRange when we find the right numbers

                    foreach (Collider player in playerHit)
                    {
                        if (qPressed == true)
                        {
                            QuestManagementSystem.singleton.updateQuest(2);
                            wasUsed = true;
                        }
                    }
                }
                break;
        }
    }
}
