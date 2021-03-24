using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destinationObjects : MonoBehaviour
{
    public LayerMask playerLayer;
    bool wasUsed = false;
    // Start is called before the first frame update
    void Start()
    {
        wasUsed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (wasUsed == false)
        {
            Collider[] playerHit = Physics.OverlapSphere(transform.position, 3f, playerLayer); //Change to just basicRange when we find the right numbers

            foreach (Collider player in playerHit)
            {
                if (QuestManagementSystem.singleton.returnQuestType() == 1)
                {
                    QuestManagementSystem.singleton.updateQuest(1);
                    wasUsed = true;
                }
            }
        }
    }
}
