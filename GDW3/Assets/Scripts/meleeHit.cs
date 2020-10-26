using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckHit(int smallMeleeDamage, LayerMask playerLayer)
    {
        Collider[] playerHit = Physics.OverlapBox(transform.position, new Vector3(1, 1, 1), new Quaternion(0, 0, 0, 1), playerLayer); //Change to just basicRange when we find the right numbers

        foreach (Collider player in playerHit)
        {
            player.GetComponent<Behavior>().takeDmg(smallMeleeDamage);
        }
    }
}
