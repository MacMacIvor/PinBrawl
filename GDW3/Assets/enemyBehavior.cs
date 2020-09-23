using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public Transform characterPos;
    Vector3 newPos = Vector3.zero;

    public const float MAX_HEALTH = 100;
    private float currentHealth;
    private bool beingKnockedBack = false;
    private Vector3 knockedDestination;


    [Range(0.0001f, 0.01f)]
    public float smoothfactor = 0.001f;

    [Range(0.0001f, 10f)]
    public float smoothknockedfactor = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MAX_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        newPos.x = characterPos.position.x;
        newPos.z = characterPos.position.z;
        float dist = Vector3.Magnitude(newPos - transform.position);
        switch (beingKnockedBack)
        {
            case true:
                transform.position = Vector3.Lerp(transform.position, knockedDestination, smoothknockedfactor);
                if (Vector3.Distance(transform.position, knockedDestination) < 1.0f)
                {
                    beingKnockedBack = false;
                }
                break;
            case false:
                if (!(dist < 5 && dist > -5))
                {
                    transform.position = Vector3.Slerp(transform.position, newPos, smoothfactor);
                }
                else
                {
                    //print("pew pew!");//Attack!
                }
                break;
        }
        

    }

    public void doKnockback(float heldPower, int orientation)
    {
        float angleToUse = (orientation == 1 ? 135 : (orientation == 2 ? 90 : (orientation == 3 ? 45 : (orientation == 4 ? 180 : (orientation == 5 ? 0 : (orientation == 6 ? 225 : (orientation == 7 ? 270 : 315)))))));
        beingKnockedBack = true;
        knockedDestination = new Vector3(transform.position.x + Mathf.Cos(angleToUse) * heldPower / 2, transform.position.y, transform.position.z + Mathf.Sin(angleToUse) * heldPower / 2);

        takeDmg(Mathf.Sqrt(heldPower));
    }

    public void takeDmg(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            dead();
        }
    }
    public void dead()
    {
        Destroy(gameObject, 1);
    }
}
