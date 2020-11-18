using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeHit : MonoBehaviour
{
    public LayerMask playerLayer;
    [Range(0,4)]
    public int hello = 0;
    Quaternion orientation = new Quaternion(0, 0, 0, 1);

    bool isAttacking = false;
    int smallMeleeDamage = 0;
    float meleeRange;
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
                if (isAttacking == true)
                {
                    Collider[] playerHit = Physics.OverlapBox(gameObject.transform.position, new Vector3(meleeRange, meleeRange, meleeRange), orientation, playerLayer); //Change to just basicRange when we find the right numbers

                    foreach (Collider player in playerHit)
                    {
                        player.GetComponent<Behavior>().takeDmg(smallMeleeDamage);
                    }
                    isAttacking = false;
                }
                break;
        }
    }

    public void CheckHit(int smallMeleeDamages, float meleeRanges)
    {
        smallMeleeDamage = smallMeleeDamages;
        isAttacking = true;
        meleeRange = meleeRanges;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(4, 4, 4));

    }

}
