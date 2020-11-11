using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    enum bulletState
    {

        ACTIVE,
        INACTIVE
    }

    bulletState state = bulletState.INACTIVE;


    [Range(0.0001f, 1.0f)]
    public float bulletSpeed = 1.0f;

    public Vector3 target;

    public Vector3 dirPos;

    public Vector3 point;

    Renderer m_Renderer;

    public LayerMask playerLayer;

    private int damage = 30;

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
                transform.position += dirPos * bulletSpeed;
                if (Vector3.Distance(transform.position, point) > 20)
                {
                    die();// Destroy(gameObject, 1);
                }
                else
                {
                    Collider[] playerHit = Physics.OverlapSphere(transform.position, 0.15f, playerLayer); //Change to just basicRange when we find the right numbers

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
        Gizmos.DrawWireSphere(transform.position, 0.15f);
    }
    public void die()
    {
        BulletPoolManager.singleton.ResetSmallBullet(this.gameObject);
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

                dirPos = BulletPoolManager.singleton.player.transform.position;
                
                point = dirPos;
                
                transform.LookAt(dirPos);
                
                dirPos = new Vector3(dirPos.x - transform.position.x, 0, dirPos.z - transform.position.z);
                
                dirPos = Vector3.Normalize(dirPos);

                break;
        }
    }
}
