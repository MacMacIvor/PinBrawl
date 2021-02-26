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


    private string lastHit = "";

    // Start is called before the first frame update
    void Start()
    {
        
        gameObject.SetActive(false);

    }

    

    private Vector3 direction;
    private Vector3 lastDirection;
    private Vector3 lastPoint;
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
                        switch (enemyType)
                        {
                            case 0:
                                smallShooterAnimationControler.singleton.playWalk(0.2f);
                                break;

                            case 1:
                                largeShooterController.singleton.playWalk(0.2f);
                                break;

                            case 2:
                                break;

                            case 3:
                                break;
                        }
                        gameObject.GetComponent<Rigidbody>().velocity = direction * speed;
                        if ((dist < range && dist > -range))
                        {
                            state = enemyState.ATTACKING;
                            switch (enemyType)
                            {
                                case 0:
                                    smallShooterAnimationControler.singleton.playWalk(0);
                                    break;

                                case 1:
                                    largeShooterController.singleton.playWalk(0);
                                    break;

                                case 2:
                                    break;

                                case 3:
                                    break;
                            }
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
                                        switch (enemyType)
                                        {
                                            case 0:
                                                smallShooterAnimationControler.singleton.playAttack();
                                                break;

                                            case 1:
                                                largeShooterController.singleton.playAttack();
                                                break;

                                            case 2:
                                                break;

                                            case 3:
                                                break;
                                        }
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

        

        direction = BulletPoolManager.singleton.player.transform.position;
        
        transform.LookAt(direction);
        
        RaycastHit hit;
        Vector3 directionToExpand = new Vector3(1, 0, 0);



        if (Physics.SphereCast(transform.position, GetComponent<Collider>().bounds.size.x, transform.forward, out hit, range))
        {
            if (hit.transform.name != "player 1")
            {
                if (!(hit.transform.name == lastHit))
                {
                    lastHit = hit.transform.name;

                    Vector3 savedRotation = gameObject.transform.eulerAngles;

                    RaycastHit boom;

                    for (int i = 1; i < 19; i++) {
                        gameObject.transform.eulerAngles = savedRotation;
                        gameObject.transform.eulerAngles += new Vector3(0, 10 * i, 0);
                        
                        Vector3 cubeDirections = transform.position - QuestManagementSystem.singleton.player.gameObject.transform.position;
                        float angles = Vector3.Angle(cubeDirections, transform.forward); //instead of square forward we want enemy forward);
                        
                        if (angles < 100)
                        {
                            ;
                        }
                        else
                        {


                            Physics.SphereCast(transform.position, GetComponent<Collider>().bounds.size.x, transform.forward, out boom, range);
                            if (boom.collider != null && boom.collider.name != "player 1") //We need to see if the up is actually clear to move in that direction
                            {
                                //It hit something


                                if (Mathf.Abs(Mathf.Abs(boom.distance) - Mathf.Abs(hit.distance)) > GetComponent<Collider>().bounds.size.x)
                                {
                                    //It can fit through the gap
                                    i = 20; //Exit the loop
                                    direction = transform.gameObject.transform.forward;
                                    lastDirection = direction;
                                }

                            }
                            else if (boom.collider == null)
                            {
                                i = 20;
                                direction = transform.gameObject.transform.forward;
                                lastDirection = direction;
                            }
                            else
                            {
                                gameObject.transform.eulerAngles = savedRotation;
                                gameObject.transform.eulerAngles += new Vector3(0, 10 * -i, 0);
                                Physics.SphereCast(transform.position, GetComponent<Collider>().bounds.size.x, transform.forward, out boom, range);
                                if (boom.collider != null && boom.collider.name != "player 1") //We need to see if the up is actually clear to move in that direction
                                {
                                    //It hit something


                                    if (Mathf.Abs(Mathf.Abs(boom.distance) - Mathf.Abs(hit.distance)) > GetComponent<Collider>().bounds.size.x)
                                    {
                                        //It can fit through the gap
                                        i = 20; //Exit the loop
                                        direction = transform.gameObject.transform.forward;
                                        lastDirection = direction;
                                    }

                                }
                                else if (boom.collider == null)
                                {
                                    i = 20;
                                    direction = transform.gameObject.transform.forward;
                                    lastDirection = direction;
                                }
                            }
                        }

                    }


                }
                else
                {
                    //The same object in the way so we want to continue the same path we were doing
                    //direction = new Vector3(lastPoint.x - transform.position.x, 0, lastPoint.z - transform.position.z);
                    
                    direction = lastDirection;
                    transform.LookAt(transform.position + direction);

                }
            }
            else
            {
                //Just follow the player as it's a straight line to them

                direction = new Vector3(direction.x - transform.position.x, 0, direction.z - transform.position.z);

                direction = Vector3.Normalize(direction);
                lastHit = hit.transform.name;

            }

        }
        else
        {

            direction = new Vector3(direction.x - transform.position.x, 0, direction.z - transform.position.z);

            direction = Vector3.Normalize(direction);

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
            soundsManager.soundsSingleton.playSoundEffect(0);
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
        switch (enemyType)
            {
            case 0:
                soundsManager.soundsSingleton.playSoundEffect(Random.Range(7, 10));
                smallShooterAnimationControler.singleton.playHit();
                break;

            case 1:
                soundsManager.soundsSingleton.playSoundEffect(Random.Range(3, 6));
                largeShooterController.singleton.playHit();
                break;

            case 2:
                soundsManager.soundsSingleton.playSoundEffect(Random.Range(7, 10));
                break;

            case 3:
                soundsManager.soundsSingleton.playSoundEffect(Random.Range(3, 6));
                break;
        }
        if (currentHealth <= 0)
        {
            soundsManager.soundsSingleton.playSoundEffect(0);
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
                soundsManager.soundsSingleton.playSoundEffect(2);
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
                soundsManager.soundsSingleton.playSoundEffect(1);
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
                   meleeHit.singleton.CheckHit(smallMeleeDamage * buffModifryer, range, gameObject.transform.position);

                }
                else
                {
                   meleeHit.singleton.CheckHit(smallMeleeDamage, range, gameObject.transform.position);
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
