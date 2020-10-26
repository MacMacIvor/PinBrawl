using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public LayerMask playerLayer;


    public GameObject smallBulletPrefab;
    public GameObject bigBulletPrefab;

    private float currentHealth;
    private float shootCooldown;
    private float speed;
    private float range;
    private float knockbackResist;
    private float knockbackSpeed;


    [Range(0, 10)]
    public int enemyType = 0;

    [Range(0.1f, 10.0f)]
    public float shootCooldownSmallShooterSaved = 1;

    [Range(0, 1.0f)]
    public float smallBurstCooldownbetweenShots = 0.1f;
    private float smallBurstCooldownbetweenShotsSaved;

    [Range(0, 100f)]
    public int numOfShots = 3;
    private int shotsFired;

    [Range(0.1f, 20.0f)]
    public float shootCooldownBigShooterSaved = 1;

    [Range(0.1f, 5.0f)]
    public float hitCoolDownSmallMeleeSaved = 1;

    public Transform characterPos;
    Vector3 newPos = Vector3.zero;

    public bullet smallBullets;
    public LargeBullets bigBullets;

    [Range(1, 1000)]
    public const float MAX_HEALTH_SMALL_SHOOTER = 100;

    [Range(1, 1000)]
    public const float MAX_HEALTH_BIG_SHOOTER = 100;

    [Range(1, 1000)]
    public const float MAX_HEALTH_SMALL_MELEE = 100;

    private bool beingKnockedBack = false;
    private Vector3 knockedDestination;

    [Range(0.0001f, 0.01f)]
    public float speedSmallShooter = 0.001f;

    [Range(0.0001f, 0.01f)]
    public float speedBigShooter = 0.001f;

    [Range(0.0001f, 0.01f)]
    public float speedSmallMelee = 0.001f;

    [Range(0.0001f, 10f)]
    public float knockedBackSpeedSmallShooter = 2.0f;

    [Range(0.0001f, 10f)]
    public float knockedBackSpeedBigShooter = 2.0f;

    [Range(0.0001f, 10f)]
    public float knockedBackSpeedSmallMelee = 2.0f;

    [Range(0.0001f, 10f)]
    public float knockBackPowerSmallShooter = 1.0f;

    [Range(0.0001f, 10f)]
    public float knockBackPowerBigShooter = 1.0f;

    [Range(0.0001f, 10f)]
    public float knockBackPowerSmallMelee = 1.0f;

    [Range(0, 100)]
    public float rangeOfSmallShooter = 5;

    [Range(0, 1000)]
    public float rangeOfLarge = 100;

    [Range(0, 1000)]
    public float rangeOfSmallMelee = 0.5f;

    [Range(0.001f, 1000)]
    public float knockbackResistSmallShooter = 1;

    [Range(0.001f, 1000)]
    public float knockbackResistBigShooter = 1;

    [Range(0.001f, 1000)]
    public float knockbackResistSmallMelee = 1;

    [Range(0, 100)]
    public int smallMeleeDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        switch (enemyType)
        {
            case 0:
                currentHealth = MAX_HEALTH_SMALL_SHOOTER;
                shootCooldown = shootCooldownSmallShooterSaved;
                speed = speedSmallShooter;
                range = rangeOfSmallShooter;
                knockbackResist = knockbackResistSmallShooter;
                knockbackSpeed = knockedBackSpeedSmallShooter;
                break;
            case 1:
                currentHealth = MAX_HEALTH_BIG_SHOOTER;
                shootCooldown = shootCooldownBigShooterSaved;
                speed = speedBigShooter;
                range = rangeOfLarge;
                knockbackResist = knockbackResistBigShooter;
                knockbackSpeed = knockedBackSpeedBigShooter;
                break;
            case 2:
                currentHealth = MAX_HEALTH_BIG_SHOOTER;
                shootCooldown = hitCoolDownSmallMeleeSaved;
                speed = speedSmallMelee;
                range = rangeOfLarge;
                knockbackResist = knockbackResistSmallMelee;
                knockbackSpeed = knockedBackSpeedSmallMelee;
                break;
        }
        shotsFired = 0;
        smallBurstCooldownbetweenShotsSaved = smallBurstCooldownbetweenShots;
    }

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
                newPos.x = characterPos.position.x;
                newPos.z = characterPos.position.z;
                float dist = Vector3.Magnitude(newPos - transform.position);
                

                switch (beingKnockedBack)
                {
                    case true:
                        transform.position = Vector3.Lerp(transform.position, knockedDestination, knockbackSpeed);
                        if (Vector3.Distance(transform.position, knockedDestination) < 1.0f)
                        {
                            beingKnockedBack = false;
                        }
                        break;
                    case false:
                        if (!(dist < range && dist > -range))
                        {
                            transform.position = Vector3.Slerp(transform.position, newPos, speed);
                        }
                        else
                        {
                            switch (enemyType)
                            {
                                case 0:
                                    if (shootCooldown <= 0 && smallBurstCooldownbetweenShots <= 0)
                                    {
                                        smallBurstCooldownbetweenShots = smallBurstCooldownbetweenShotsSaved;
                                        shotsFired++;
                                        if (shotsFired == numOfShots)
                                        {
                                            spawnBullet(true);
                                            shootCooldown = shootCooldownSmallShooterSaved;
                                            smallBurstCooldownbetweenShots = smallBurstCooldownbetweenShotsSaved;
                                            shotsFired = 0;
                                        }
                                        else
                                        {
                                            spawnBullet(false);
                                        }
                                    }
                                    else if (shootCooldown <= 0 && smallBurstCooldownbetweenShots > 0)
                                    {
                                        smallBurstCooldownbetweenShots -= Time.deltaTime;
                                    }
                                        break;
                                case 1:
                                case 2:
                                    if (shootCooldown <= 0)
                                    {
                                        spawnBullet(true);
                                    }
                                    break;
                            }
                            
                        }
                        break;
                }

                shootCooldown -= Time.deltaTime;
                break;
        }
    }

    public void doKnockback(float heldPower, int orientation)
    {
        float angleToUse = (orientation == 1 ? 135 : (orientation == 2 ? 90 : (orientation == 3 ? 45 : (orientation == 4 ? 180 : (orientation == 5 ? 0 : (orientation == 6 ? 225 : (orientation == 7 ? 270 : 315)))))));
        beingKnockedBack = true;
        //knockedDestination = new Vector3(transform.position.x + Mathf.Cos(angleToUse) * Mathf.Sqrt(heldPower) / knockbackResist, transform.position.y, transform.position.z + Mathf.Sin(angleToUse) * Mathf.Sqrt(heldPower) / knockbackResist);

        /*
         dirPos = target;

        point = dirPos;

        transform.LookAt(dirPos);

        dirPos = new Vector3(dirPos.x - transform.position.x, 0, dirPos.z - transform.position.z);

        dirPos = Vector3.Normalize(dirPos);
         */

        Vector3 temp = transform.position - characterPos.transform.position;
        temp = Vector3.Normalize(temp);

        temp = new Vector3(temp.x * heldPower / knockbackResist, 0, temp.z * heldPower / knockbackResist);

        knockedDestination = temp + transform.position;

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

    private void spawnBullet(bool timeReset)
    {
        switch (enemyType)
        {
            case 0:
                shootCooldown = timeReset == true ? shootCooldownSmallShooterSaved : shootCooldown;
                smallBullets.SetTarget(characterPos.position);
                GameObject theBullet = Instantiate(smallBulletPrefab) as GameObject;
                theBullet.transform.position = transform.position;
                break;
            case 1:
                shootCooldown = timeReset == true ? shootCooldownBigShooterSaved : shootCooldown;
                bigBullets.SetTarget(characterPos.position);
                GameObject theBullet2 = Instantiate(bigBulletPrefab) as GameObject;
                theBullet2.transform.position = transform.position;
                break;
            case 2:
                //not spawing anything but I put it in here because why not
                gameObject.GetComponentInChildren<meleeHit>().CheckHit(smallMeleeDamage, playerLayer);
                shootCooldown = hitCoolDownSmallMeleeSaved;
                break;
        }
    }

}
