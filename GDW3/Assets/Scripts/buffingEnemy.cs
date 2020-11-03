using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buffingEnemy : MonoBehaviour
{
    public LayerMask enemies;
    [Range(0, 4)]
    public int hello = 0;
    Quaternion orientation = new Quaternion(0, 0, 0, 1);

    bool isAttacking = false;
    float theRange;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking == true)
        {
            Collider[] enemiesHit = Physics.OverlapBox(gameObject.transform.position, new Vector3(theRange, theRange, theRange), orientation, enemies); //Change to just basicRange when we find the right numbers

            foreach (Collider enemy in enemiesHit)
            {
                enemy.GetComponent<enemyBehavior>().buff();
            }
            isAttacking = false;
        }
    }

    public void CheckHit(float range)
    {
        isAttacking = true;
        theRange = range;

    }
}
