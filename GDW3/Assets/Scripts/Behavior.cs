using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{
    public Vector3 playerDirection = Vector3.right;
    private float cooldown = 0;

    private float cooldownB = 0;


    [Range(1, 1000)]
    public int jumpModifyer = 250;

    [Range(0, 5)]
    public float cooldownDuration = 1;

    [Range(0, 5)]
    public float cooldownBDuration = 0.5f; //Basic slash

    private float heldPower = 0;

    [Range(0, 100)]
    public float chargePowerModifyer = 1;

    public Transform chargePoint;
    public Transform basicPoint;

    [Range(0,10)]
    public float chargeRangeHeight = 1;

    [Range(0, 10)]
    public float chargeRangeWidth = 1;

    [Range(0, 10)]
    public float basicRangeHeight = 1;

    [Range(0, 10)]
    public float basicRangeWidth = 1;

    Vector3 chargeDimensions = Vector3.up;
    Vector3 basicDimensions = Vector3.up;

    Quaternion orientationMaybe = new Quaternion(0, 0, 0, 1);

    public LayerMask enemyLayers;
    public LayerMask bulletLayers;

    public followMouse please;
    public followMouse please2;
    public followMouse please3;

    private int direction = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    int mouse1Buffer = 0;


    // Update is called once per frame
    private bool isEditing = false;
    private bool isEDown = false;
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (isEDown == false)
            {
                isEditing = !isEditing;

            }
            isEDown = true;
        }
        else
        {
            isEDown = false;
        }

        switch (isEditing)
        {
            case true:
                break;
            case false:
                transform.Translate(playerDirection * 5 * Time.deltaTime);
                if (Input.GetKey(KeyCode.W))
                {
                    playerDirection.z = 1;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    playerDirection.z = -1;
                }
                else
                {
                    playerDirection.z = 0;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    playerDirection.x = -1;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    playerDirection.x = 1;
                }
                else
                {
                    playerDirection.x = 0;
                }
                switch (playerDirection.x)
                {
                    case -1: //left
                        switch (playerDirection.z)
                        {
                            case -1: //down
                                callUpdateDirection(6);
                                break;
                            case 0:
                                callUpdateDirection(4);
                                break;
                            case 1:
                                callUpdateDirection(1);
                                break;
                        }
                        break;
                    case 0:
                        switch (playerDirection.z)
                        {
                            case -1:
                                callUpdateDirection(7);
                                break;
                            case 0:
                                break;
                            case 1:
                                callUpdateDirection(2);
                                break;
                        }
                        break;
                    case 1:
                        switch (playerDirection.z)
                        {
                            case -1:
                                callUpdateDirection(8);
                                break;
                            case 0:
                                callUpdateDirection(5);
                                break;
                            case 1:
                                callUpdateDirection(3);
                                break;
                        }
                        break;
                }


                if (Input.GetKey(KeyCode.LeftShift) && cooldown == 0)
                {
                    playerDirection *= jumpModifyer;
                    cooldown = cooldownDuration;
                }

                if (Input.GetKey(KeyCode.Mouse1))
                {
                    chargeAttack();
                    mouse1Buffer = 1;
                }
                else if (mouse1Buffer == 1)
                {
                    mouse1Buffer = 0;
                    Debug.Log(heldPower);
                    releaseChargeAttack();
                }


                if (Input.GetKey(KeyCode.Mouse0) && cooldownB == 0)
                {
                    basicAttack();
                    cooldownB = cooldownBDuration;
                }
                break;
        }
    }

    void callUpdateDirection(int num)
    {
        direction = num;
        please.updateDirection(num);
        please2.updateDirection(num);
        please3.updateDirection(num);
    }

    void basicAttack()
    {
        Collider[] enemiesHit = Physics.OverlapBox(basicPoint.position, new Vector3(basicDimensions.x + basicRangeHeight, 1, basicDimensions.z + basicRangeWidth), orientationMaybe, enemyLayers); //Change to just basicRange when we find the right numbers
        Collider[] bulletHit = Physics.OverlapBox(basicPoint.position, new Vector3(basicDimensions.x + basicRangeHeight, 1, basicDimensions.z + basicRangeWidth), orientationMaybe, bulletLayers); //Change to just basicRange when we find the right numbers

        foreach (Collider enemies in enemiesHit)
        {
            //enemies.GetComponent<enemyBehavior>().takeDmg(heldPower);
            enemies.GetComponent<enemyBehavior>().takeDmg(30);
        }

        foreach (Collider bullets in bulletHit)
        {
            //enemies.GetComponent<enemyBehavior>().takeDmg(heldPower);
            bullets.GetComponent<bullet>().die();
        }
    }

    void chargeAttack()
    {
        heldPower += 1.0f * chargePowerModifyer;
    }

    

    void releaseChargeAttack()
    {
        //do the attack
        Collider[] enemiesHit = Physics.OverlapBox(chargePoint.position, new Vector3(chargeDimensions.x + chargeRangeHeight, 1, chargeDimensions.z + chargeRangeWidth), orientationMaybe, enemyLayers); //Change to just chargeRange when we find the right numbers
        
        foreach(Collider enemies in enemiesHit)
        {
            //enemies.GetComponent<enemyBehavior>().takeDmg(heldPower);
            enemies.GetComponent<enemyBehavior>().doKnockback(heldPower, direction);
        }
        heldPower = 0;

    }

    private void OnDrawGizmosSelected()
    {


        Gizmos.DrawWireCube(chargePoint.position, new Vector3 (chargeDimensions.x + chargeRangeHeight, 1, chargeDimensions.z + chargeRangeWidth));
        Gizmos.DrawWireCube(basicPoint.position, new Vector3 (basicDimensions.x + basicRangeHeight, 1, basicDimensions.z + basicRangeWidth));
    }

    void LateUpdate() //Update that is called after all the other updates
    {
        cooldown = (cooldown <= 0 ? 0 : cooldown - 1 * Time.deltaTime);
        cooldownB = (cooldownB <= 0 ? 0 : cooldownB - 1 * Time.deltaTime);
    }

    public void takeDmg(int dmg)
    {
        //Nothing for now
        //transform.position = new Vector3(0, 0, 0);
    }
}
