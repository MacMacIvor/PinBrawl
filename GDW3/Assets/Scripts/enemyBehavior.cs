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

    private Vector3 knockedDestination;

    [Range(0.0001f, 10f)]
    public float speedSmallShooter = 0.001f;

    [Range(0.0001f, 10f)]
    public float speedBigShooter = 0.001f;

    [Range(0.0001f, 10f)]
    public float speedSmallMelee = 0.001f;

    [Range(0.0001f, 10f)]
    public float speedBuffer = 0.001f;

    [Range(0.0001f, 10f)]
    public float knockedBackSpeedSmallShooterSeconds = 2.0f;

    [Range(0.0001f, 10f)]
    public float knockedBackSpeedBigShooterSeconds = 2.0f;

    [Range(0.0001f, 10f)]
    public float knockedBackSpeedSmallMeleeSeconds = 2.0f;

    [Range(0.0001f, 10f)]
    public float knockedBackSpeedBufferSeconds = 2.0f;

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


    private int locked = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        gameObject.SetActive(false);

    }

    

    private Vector3 direction;
    //private bool isColliding = false;

    private float knockbackSpeedFraction;
    void Update()
    {


        switch (pauseGame.singleton.stateOfGame)
        {
            case pauseGame.generalState.PAUSED:
                break;
            case pauseGame.generalState.PLAYING:

                characterPos = GameObject.FindGameObjectsWithTag("Player")[0].transform;
                newPos.x = GameObject.FindGameObjectsWithTag("Player")[0].transform.position.x;
                newPos.z = GameObject.FindGameObjectsWithTag("Player")[0].transform.position.z;
                float dist = Vector3.Distance(newPos, new Vector3(transform.position.x, 0, transform.position.z));
                locked--;
                //Check to see if the player is looking at the enemy or not
                Vector3 cubeDirection = transform.position - QuestManagementSystem.singleton.player.gameObject.transform.position;
                float angle = Vector3.Angle(cubeDirection, QuestManagementSystem.singleton.player.gameObject.transform.GetChild(2).forward);
                gameObject.GetComponentInChildren<enemyHealth>().updateLength(currentHealth);
                gameObject.GetComponentInChildren<enemyHealth>().changeVisibility(angle < 40 ? true : false);
                

                switch (state)
                {
                    case enemyState.INACTIVE:

                        break;
                    case enemyState.TUTORIAL:
                        if (!(dist < range && dist > -range))
                        {
                            gameObject.GetComponent<Rigidbody>().velocity = direction * speed;
                        }
                        else
                        {
                            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                        }
                        break;
                    case enemyState.CHASING:
                        calculateDirection();

                        gameObject.GetComponent<Rigidbody>().velocity = direction * speed;
                        if ((dist < range && dist > -range))
                        {
                            state = enemyState.ATTACKING;
                            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                        }
                        break;
                    case enemyState.ATTACKING:
                        calculateDirection();

                        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

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
                                    if ((Vector3.Distance(transform.position, BulletPoolManager.singleton.player.transform.position) > range))
                                    {
                                        state = enemyState.CHASING;

                                    }
                                    else
                                    {
                                        spawnBullet(true);

                                    }
                                }
                                break;
                        }
                        if (shootCooldown <= 0 && shotsFired == 0 && (Vector3.Distance(transform.position, BulletPoolManager.singleton.player.transform.position) > range))
                        {
                            state = enemyState.CHASING;
                            gameObject.GetComponent<Rigidbody>().velocity = direction * speed;

                        }
                        shootCooldown -= Time.deltaTime;
                        break;
                    case enemyState.FLYING:
                        //gameObject.GetComponent<Rigidbody>().velocity = direction * knockbackSpeedFraction;

                        if ((Vector3.Distance(transform.position, knockedDestination) < 1.0f) || Vector3.Magnitude(gameObject.GetComponent<Rigidbody>().velocity) <= 0.5f)// || isColliding == true)
                        {
                            state = enemyState.CHASING;
                            gameObject.GetComponent<Rigidbody>().velocity = direction * speed;
                        }
                        break;

                }

                buffDuration -= Time.deltaTime;
                break;
        }
        //isColliding = false;

    }

    private Vector3 calculateDirection()
    {

        if (locked <= 0)
        {

            direction = BulletPoolManager.singleton.player.transform.position;

            transform.LookAt(direction);

            RaycastHit hit;
            Vector3 directionToExpand = new Vector3(1, 0, 0);



            if (Physics.Raycast(transform.position, transform.forward, out hit, range))
            {
                if (hit.transform.name != "player 1")
                {
                    if (Mathf.Abs(transform.forward.z) > Mathf.Abs(transform.forward.x))
                    {
                        directionToExpand = new Vector3(0, 0, 1);//Want perpendicularish to where the enemy is facing
                    }

                    //What I want to do
                    //Find corner of the hit object
                    //Find the angle from the enemy to the corner + enemy width
                    //Then check to see if the enemy can walk there
                    //Select the one that brings the enemy closer to the player    
                    //Increment angles in a zigzig fassion to find possible movement


                    //Find corner of the hit object
                    Vector3[] fourCorners = new[] { hit.transform.position + new Vector3(hit.collider.bounds.size.x + transform.localScale.x, 0, hit.collider.bounds.size.z   + transform.localScale.z),
                                                    hit.transform.position + new Vector3(hit.collider.bounds.size.x + transform.localScale.x, 0, -hit.collider.bounds.size.z  - transform.localScale.z),
                                                    hit.transform.position + new Vector3(-hit.collider.bounds.size.x - transform.localScale.x, 0, hit.collider.bounds.size.z  + transform.localScale.z),
                                                    hit.transform.position + new Vector3(-hit.collider.bounds.size.x - transform.localScale.x, 0, -hit.collider.bounds.size.z - transform.localScale.z)};

                    //Find what two points are closer to the enemy
                    Vector3 leftToSave = (Vector3.Distance(fourCorners[0], transform.position) < Vector3.Distance(fourCorners[1], transform.position) &&
                        Vector3.Distance(fourCorners[0], transform.position) < Vector3.Distance(fourCorners[2], transform.position) &&
                        Vector3.Distance(fourCorners[0], transform.position) < Vector3.Distance(fourCorners[3], transform.position) ? fourCorners[0] :

                        (Vector3.Distance(fourCorners[1], transform.position) < Vector3.Distance(fourCorners[0], transform.position) &&
                         Vector3.Distance(fourCorners[1], transform.position) < Vector3.Distance(fourCorners[2], transform.position) &&
                         Vector3.Distance(fourCorners[1], transform.position) < Vector3.Distance(fourCorners[3], transform.position)) ? fourCorners[1] :

                         (Vector3.Distance(fourCorners[2], transform.position) < Vector3.Distance(fourCorners[0], transform.position) &&
                          Vector3.Distance(fourCorners[2], transform.position) < Vector3.Distance(fourCorners[1], transform.position) &&
                          Vector3.Distance(fourCorners[2], transform.position) < Vector3.Distance(fourCorners[3], transform.position)) ? fourCorners[2] : fourCorners[3]);

                    for (int i = 0; i < 3; i++)
                    {
                        if (fourCorners[i] == leftToSave)
                        {
                            fourCorners[i] = new Vector3(9999.0f, 9999.0f, 9999.0f);
                        }
                    }

                    Vector3 rightToSave = (Vector3.Distance(fourCorners[0], transform.position) < Vector3.Distance(fourCorners[1], transform.position) &&
                        Vector3.Distance(fourCorners[0], transform.position) < Vector3.Distance(fourCorners[2], transform.position) &&
                        Vector3.Distance(fourCorners[0], transform.position) < Vector3.Distance(fourCorners[3], transform.position) ? fourCorners[0] :

                        (Vector3.Distance(fourCorners[1], transform.position) < Vector3.Distance(fourCorners[0], transform.position) &&
                         Vector3.Distance(fourCorners[1], transform.position) < Vector3.Distance(fourCorners[2], transform.position) &&
                         Vector3.Distance(fourCorners[1], transform.position) < Vector3.Distance(fourCorners[3], transform.position)) ? fourCorners[1] :

                         (Vector3.Distance(fourCorners[2], transform.position) < Vector3.Distance(fourCorners[0], transform.position) &&
                          Vector3.Distance(fourCorners[2], transform.position) < Vector3.Distance(fourCorners[1], transform.position) &&
                          Vector3.Distance(fourCorners[2], transform.position) < Vector3.Distance(fourCorners[3], transform.position)) ? fourCorners[2] : fourCorners[3]);
                    //We found the two corners of the object nearest to the object
                    //Now we need the angle for both

                    float leftAngle = Mathf.Sin(Vector3.Magnitude(leftToSave));
                    float rightAngle = Mathf.Sin(Vector3.Magnitude(rightToSave));


                    //Now we check the angles to see if there is another object in the way or not
                    RaycastHit leftHit;
                    RaycastHit rightHit;
                    bool useLeft = true;
                    bool useRight = true;
                    transform.LookAt(leftToSave);
                    if (Physics.Raycast(transform.position, transform.forward, out hit, range)) //We need to see if the up is actually clear to move in that direction
                    {
                        //It hit something
                        //TODO: make it check more shit, so now we would check to see if the new object hit is farther behind so we can path around it, if not, add more degrees to test a new spot if right also fails
                        //This would probably be done with a do loop
                        useLeft = false;
                    }

                    transform.LookAt(rightToSave);
                    if (Physics.Raycast(transform.position, transform.forward, out hit, range)) //We need to see if the up is actually clear to move in that direction
                    {
                        //It hit something
                        //TODO: make it check more shit, so now we would check to see if the new object hit is farther behind so we can path around it, if not, add more degrees to test a new spot if right also fails
                        //This would probably be done with a do loop
                        useRight = false;
                    }

                    if (useRight == true)
                    {
                        if (useLeft == true)
                        {
                            transform.LookAt(leftToSave);
                            direction = new Vector3(leftToSave.x - transform.position.x, 0, leftToSave.z - transform.position.z);

                            direction = Vector3.Normalize(direction);
                            locked = 100;

                        }
                        else
                        {
                            direction = new Vector3(rightToSave.x - transform.position.x, 0, rightToSave.z - transform.position.z);

                            direction = Vector3.Normalize(direction);
                            locked = 100;
                        }
                    }
                    else
                    {
                        transform.LookAt(leftToSave);
                        direction = new Vector3(leftToSave.x - transform.position.x, 0, leftToSave.z - transform.position.z);

                        direction = Vector3.Normalize(direction);
                        locked = 100;

                    }

                    int hello = 0;
                    hello++;
                }
                else
                {
                    direction = new Vector3(direction.x - transform.position.x, 0, direction.z - transform.position.z);

                    direction = Vector3.Normalize(direction);
                }

            }
            else
            {

                direction = new Vector3(direction.x - transform.position.x, 0, direction.z - transform.position.z);

                direction = Vector3.Normalize(direction);
            }
        }
        return direction;
    }

    public void OnCollisionEnter(Collision collision)
    {
        //isColliding = true;
        
        direction = Vector3.Reflect(direction, collision.contacts[0].normal);
        gameObject.GetComponent<Rigidbody>().velocity = direction * Vector3.Magnitude(gameObject.GetComponent<Rigidbody>().velocity);

        if (state == enemyState.FLYING)
        {
            soundsManager.soundsSingleton.playSoundEffect("dronecrash_by_metal_wall");
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("enemies"))
            {
                collision.collider.gameObject.GetComponent<enemyBehavior>().takeDmg(30);
                Debug.Log("damage Taken?");
            }
        }
    }

    public void doKnockback(float heldPower, int orientation)
    {
        float angleToUse = (orientation == 1 ? 135 : (orientation == 2 ? 90 : (orientation == 3 ? 45 : (orientation == 4 ? 180 : (orientation == 5 ? 0 : (orientation == 6 ? 225 : (orientation == 7 ? 270 : 315)))))));
        state = enemyState.FLYING;
        

        Vector3 temp = new Vector3(0, 0, 0);
        temp = transform.position - characterPos.transform.position;
        temp = Vector3.Normalize(temp);

        temp = new Vector3(temp.x * heldPower / knockbackResist, 0, temp.z * heldPower / knockbackResist);

        knockedDestination = temp + transform.position;

        direction = new Vector3(knockedDestination.x - transform.position.x, 0, knockedDestination.z - transform.position.z);
        direction = Vector3.Normalize(direction);

        knockbackSpeedFraction = Vector3.Distance(knockedDestination, transform.position) / knockbackSpeed;


        gameObject.GetComponent<Rigidbody>().velocity = direction * knockbackSpeedFraction;

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
        saveLoadingManager.singleton.numberOfKilledUpdate();
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
    }

    private void spawnBullet(bool timeReset)
    {

        switch (enemyType)
        {
            case 0:
                soundsManager.soundsSingleton.playSoundEffect("laser_pistol");
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
                soundsManager.soundsSingleton.playSoundEffect("laser_cannon");
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
                testScript.singleton.attack();
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
                        gameObject.GetComponent<Rigidbody>().drag = knockbackResistSmallShooter;
                        knockbackSpeed = knockedBackSpeedSmallShooterSeconds;
                        break;
                    case 1:
                        currentHealth = MAX_HEALTH_BIG_SHOOTER;
                        shootCooldown = shootCooldownBigShooterSaved;
                        speed = speedBigShooter;
                        range = rangeOfLarge;
                        knockbackResist = knockbackResistBigShooter;
                        gameObject.GetComponent<Rigidbody>().drag = knockbackResistBigShooter;
                        knockbackSpeed = knockedBackSpeedBigShooterSeconds;
                        break;
                    case 2:
                        currentHealth = MAX_HEALTH_SMALL_MELEE;
                        shootCooldown = hitCoolDownSmallMeleeSaved;
                        speed = speedSmallMelee;
                        range = rangeOfSmallMelee;
                        knockbackResist = knockbackResistSmallMelee;
                        gameObject.GetComponent<Rigidbody>().drag = knockbackResistSmallMelee;
                        knockbackSpeed = knockedBackSpeedSmallMeleeSeconds;
                        break;
                    case 3:
                        currentHealth = MAX_HEALTH_BUFFER;
                        shootCooldown = shootCoolDownBufferSaved;
                        speed = speedBuffer;
                        range = rangeOfBuffer;
                        knockbackResist = knockbackResistBuffer;
                        gameObject.GetComponent<Rigidbody>().drag = knockbackResistBuffer;
                        knockbackSpeed = knockedBackSpeedBufferSeconds;
                        break;
                }
                shotsFired = 0;
                smallBurstCooldownbetweenShotsSaved = smallBurstCooldownbetweenShots;
                buffDurationSaved = 15;

                direction = new Vector3(BulletPoolManager.singleton.transform.position.x - transform.position.x, 0, BulletPoolManager.singleton.transform.position.z - transform.position.z);
                direction = Vector3.Normalize(direction);
                direction = direction * speed;

                break;
        }
    }
}
