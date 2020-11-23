using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeBullets : MonoBehaviour
{

    enum bulletState
    {

        ACTIVE,
        INACTIVE
    }

    bulletState state = bulletState.INACTIVE;

    [Range(0.0001f, 1.0f)]
    public float bulletSpeed = 1.0f;

    [Range(0.0001f, 100)]
    public float bulletYSpeedHeight = 0.001f;

    [Range(0, 1000)]
    public int damage = 30;

    private float distToTarget;
    private float distToTargetHalf;

    public Vector3 target;

    public Vector3 dirPos;

    public Vector3 point;

    Renderer m_Renderer;

    public LayerMask playerLayer;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        switch (pauseGame.singleton.stateOfGame)
        {
            case pauseGame.generalState.PAUSED:
                break;
            case pauseGame.generalState.PLAYING:
                distToTarget -= Vector3.Distance(transform.position, transform.position + dirPos * bulletSpeed);
                transform.position += dirPos * bulletSpeed;
                transform.position += new Vector3(0, (distToTarget > distToTargetHalf ? bulletYSpeedHeight : -bulletYSpeedHeight), 0);
                if (Vector3.Distance(transform.position, point) > 200 || transform.position.y <= -1)
                {
                    die();//Destroy(gameObject, 1);
                }
                else
                {
                    Collider[] playerHit = Physics.OverlapSphere(transform.position, 0.5f, playerLayer); //Change to just basicRange when we find the right numbers

                    foreach (Collider player in playerHit)
                    {
                        player.GetComponent<Behavior>().takeDmg(damage);
                        die();
                    }
                }
                break;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
    public void die()
    {
        BulletPoolManager.singleton.ResetLargeBullet(this.gameObject);
        //Destroy(gameObject);
    }
    public void SetTarget(Vector3 pos)
    {
        target = pos;
    }
    public void extraDmg(int extra)
    {
        damage *= extra;
    }
    public void changeActive()
    {
        switch (state)
        {
            case bulletState.ACTIVE:
                gameObject.SetActive(false);
                state = bulletState.INACTIVE;
                break;
            case bulletState.INACTIVE:
                gameObject.SetActive(true);
                state = bulletState.ACTIVE;

                target = BulletPoolManager.singleton.player.transform.position;

                distToTarget = Vector3.Distance(transform.position, target);
                distToTargetHalf = distToTarget / 2;

                dirPos = target;

                point = dirPos;

                transform.LookAt(dirPos);

                dirPos = new Vector3(dirPos.x - transform.position.x, 0, dirPos.z - transform.position.z);

                dirPos = Vector3.Normalize(dirPos);

                break;
        }
    }
}
