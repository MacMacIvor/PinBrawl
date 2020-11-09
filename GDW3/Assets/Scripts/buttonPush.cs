using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPush : MonoBehaviour
{
    public LayerMask playerLayer;
    bool qPressed = false;
    bool wasUsed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
            qPressed = true;
        else
            qPressed = false;

        if (wasUsed == false)
        {
            Collider[] playerHit = Physics.OverlapSphere(transform.position, 5f, playerLayer); //Change to just basicRange when we find the right numbers

            foreach (Collider player in playerHit)
            {
                if (qPressed == true)
                {
                    QuestManagementSystem.singleton.updateQuest(2);
                    wasUsed = true;
                }
            }
        }
    }
}
