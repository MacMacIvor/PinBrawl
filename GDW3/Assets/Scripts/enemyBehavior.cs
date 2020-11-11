using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public GameObject smallBulletPrefab;
    public GameObject bigBulletPrefab;

    private float currentHealth;
    private float shootCooldown;
    private float speed;
    private float range;
    private float knockbackResist;
    private float knockbackSpeed;

    enum enemyState
    {

        CHASING,
        ATTACKING,
        FLYING,
        INACTIVE,
        TUTORIAL //Special for the first enemy because he will not attack
    }

    enemyState state = enemyState.INACTIVE;

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

    [Range(0.1f, 5.0f)]
    public float shootCoolDownBufferSaved = 1;

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

    [Range(1, 1000)]
    public const float MAX_HEALTH_BUFFER = 100;

    private bool beingKnockedBack = false;
    private Vector3 knockedDestination;

    [Range(0.0001f, 0.01f)]
    public float speedSmallShooter = 0.001f;

    [Range(0.0001f, 0.01f)]
    public float speedBigShooter = 0.001f;

    [Range(0.0001f, 0.01f)]
    public float speedSmallMelee = 0.001f;

    [Range(0.0001f, 0.01f)]
    public float speedBuffer = 0.001f;

    [Range(0.0001f, 10f)]
    public float knockedBackSpeedSmallShooter = 2.0f;

    [Range(0.0001f, 10f)]
    public float knockedBackSpeedBigShooter = 2.0f;

    [Range(0.0001f, 10f)]
    public float knockedBackSpeedSmallMelee = 2.0f;

    [Range(0.0001f, 10f)]
    public float knockedBackSpeedBuffer = 2.0f;

    [Range(0.0001f, 10f)]
    public float knockBackPowerSmallShooter = 1.0f;

    [Range(0.0001f, 10f)]
    public float knockBackPowerBigShooter = 1.0f;

    [Range(0.0001f, 10f)]
    public float knockBackPowerSmallMelee = 1.0f;

    [Range(0.0001f, 10f)]
    public float knockBackPowerBuffer = 1.0f;

    [Range(0, 100)]
    public float rangeOfSmallShooter = 5;

    [Range(0, 1000)]
    public float rangeOfLarge = 100;

    [Range(0, 1000)]
    public float rangeOfSmallMelee = 0.5f;

    [Range(0, 1000)]
    public float rangeOfBuffer = 0.5f;

    [Range(0.001f, 1000)]
    public float knockbackResistSmallShooter = 1;

    [Range(0.001f, 1000)]
    public float knockbackResistBigShooter = 1;

    [Range(0.001f, 1000)]
    public float knockbackResistSmallMelee = 1;

    [Range(0.001f, 1000)]
    public float knockbackResistBuffer = 1;

    [Range(0, 100)]
    public int smallMeleeDamage = 10;

    [Range(0, 100)]
    public int buffModifryer = 2;

    [Range(0, 100)]
    public float buffDurationSaved = 15;
    private float buffDuration = 0;

    // Start is called before the first frame update
    void Start()
    {
        

        gameObject.SetActive(false);

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

                characterPos = GameObject.FindGameObjectsWithTag("Player")[0].transform;
                newPos.x = GameObject.FindGameObjectsWithTag("Player")[0].transform.position.x;
                newPos.z = GameObject.FindGameObjectsWithTag("Player")[0].transform.position.z;
                float dist = Vector3.Distance(newPos, new Vector3(transform.position.x, 0, transform.position.z));

                switch (state)
                {
                    case enemyState.INACTIVE:

                        break;
                    case enemyState.TUTORIAL:
                        if (!(dist < range && dist > -range))
                        {
                            float saveYForBugggggs = transform.position.y;
                            transform.position = Vector3.Slerp(transform.position, newPos, speed);
                            transform.position = new Vector3(transform.position.x, saveYForBugggggs, transform.position.z);
                        }
                        break;
                    case enemyState.CHASING:
                        float saveYForBugggggs2 = transform.position.y;
                        transform.position = Vector3.Slerp(transform.position, newPos, speed);
                        transform.position = new Vector3(transform.position.x, saveYForBugggggs2, transform.position.z);
                        if ((dist < range && dist > -range))
                        {
                            state = enemyState.ATTACKING;
                        }
                        break;
                    case enemyState.ATTACKING:
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
                            case 3:
                                if (shootCooldown <= 0)
                                {
                                    spawnBullet(true);
                                }
                                break;
                        }
                        if (shootCooldown <= 0 && shotsFired == 0 && (Vector3.Distance(transform.position, BulletPoolManager.singleton.player.transform.position) > range))
                        {
                            state = enemyState.CHASING;

                        }
                        shootCooldown -= Time.deltaTime;
                        break;
                    case enemyState.FLYING:
                        transform.position = Vector3.Lerp(transform.position, knockedDestination, knockbackSpeed);
                        if (Vector3.Distance(transform.position, knockedDestination) < 1.0f)
                        {
                            state = enemyState.CHASING;
                        }
                        break;

                }

                buffDuration -= Time.deltaTime;
                break;
        }
    }

    public void doKnockback(float heldPower, int orientation)
    {
        float angleToUse = (orientation == 1 ? 135 : (orientation == 2 ? 90 : (orientation == 3 ? 45 : (orientation == 4 ? 180 : (orientation == 5 ? 0 : (orientation == 6 ? 225 : (orientation == 7 ? 270 : 315)))))));
        beingKnockedBack = true;
        

        Vector3 temp = new Vector3(0, 0, 0);
        temp = transform.position - characterPos.transform.position;
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
        QuestManagementSystem.singleton.updateQuest(0);
        switch (enemyType)
        {
            case 0:
                EnemyPoolManager.singleton.ResetSmallShooter(this.gameObject);
                break;
            case 1:
                EnemyPoolManager.singleton.ResetLargeRange(this.gameObject);
                break;
            case 2:
                EnemyPoolManager.singleton.ResetsmallMelee(this.gameObject);
                break;
            case 3:
                EnemyPoolManager.singleton.ResetBuffer(this.gameObject);
                break;
            case 4:
                break;
        }
        //Destroy(gameObject, 1);
    }

    private void spawnBullet(bool timeReset)
    {
        

        switch (enemyType)
        {
            case 0:
                shootCooldown = timeReset == true ? shootCooldownSmallShooterSaved : shootCooldown;
                //smallBullets.SetTarget(characterPos.position);
                
                //GameObject theBullet = Instantiate(smallBulletPrefab) as GameObject;
                //theBullet.transform.position = transform.position;
                if (buffDuration > 0)
                {
                    //theBullet.GetComponent<bullet>().extraDmg(buffModifryer);
                    BulletPoolManager.singleton.GetBulletSmall(transform.position).GetComponent<bullet>().extraDmg(buffModifryer);

                }
                else
                {
                    BulletPoolManager.singleton.GetBulletSmall(transform.position);
                }
                break;
            case 1:
                shootCooldown = timeReset == true ? shootCooldownBigShooterSaved : shootCooldown;
                if (buffDuration > 0)
                {
                    BulletPoolManager.singleton.GetBulletLarge(transform.position).GetComponent<LargeBullets>().extraDmg(buffModifryer); //Bullets should have been done using a factory instead of different files

                }
                else
                {
                    BulletPoolManager.singleton.GetBulletLarge(transform.position);
                }
                break;
            case 2:
                //not spawing anything but I put it in here because why not
                if (buffDuration > 0)
                {
                    gameObject.GetComponentInChildren<meleeHit>().CheckHit(smallMeleeDamage * buffModifryer, rangeOfSmallMelee);

                }
                else
                {
                    gameObject.GetComponentInChildren<meleeHit>().CheckHit(smallMeleeDamage, rangeOfSmallMelee);
                }
                shootCooldown = hitCoolDownSmallMeleeSaved;
                break;
            case 3:
                shootCooldown = timeReset == true ? shootCoolDownBufferSaved : shootCooldown;
                gameObject.GetComponentInChildren<buffingEnemy>().CheckHit(rangeOfBuffer);
                break;
        }
    }
    public void buff()
    {
        buffDuration = buffDurationSaved;

    }

    public void changeActive()
    {
        switch (state)
        {
            case enemyState.ATTACKING:
            case enemyState.CHASING:
            case enemyState.FLYING:
            case enemyState.TUTORIAL:
                gameObject.SetActive(false);
                state = enemyState.INACTIVE;
                break;
            case enemyState.INACTIVE:
                gameObject.SetActive(true);
                state = enemyState.CHASING;

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
                        currentHealth = MAX_HEALTH_SMALL_MELEE;
                        shootCooldown = hitCoolDownSmallMeleeSaved;
                        speed = speedSmallMelee;
                        range = rangeOfSmallMelee;
                        knockbackResist = knockbackResistSmallMelee;
                        knockbackSpeed = knockedBackSpeedSmallMelee;
                        break;
                    case 3:
                        currentHealth = MAX_HEALTH_BUFFER;
                        shootCooldown = shootCoolDownBufferSaved;
                        speed = speedBuffer;
                        range = rangeOfBuffer;
                        knockbackResist = knockbackResistBuffer;
                        knockbackSpeed = knockedBackSpeedBuffer;
                        break;
                }
                shotsFired = 0;
                smallBurstCooldownbetweenShotsSaved = smallBurstCooldownbetweenShots;
                buffDurationSaved = 15;

                break;
        }
    }
}
