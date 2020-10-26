using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    [Range(0.0001f, 1.0f)]
    public float bulletSpeed = 1.0f;

    public Vector3 target;

    public Vector3 dirPos;

    public Vector3 point;

    Renderer m_Renderer;

    public LayerMask playerLayer;


    // Start is called before the first frame update
    void Start()
    {

        dirPos = target;

        point = dirPos;

        transform.LookAt(dirPos);

        dirPos = new Vector3(dirPos.x - transform.position.x, 0, dirPos.z - transform.position.z);

        dirPos = Vector3.Normalize(dirPos);
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
                    Destroy(gameObject, 1);
                }
                else
                {
                    Collider[] playerHit = Physics.OverlapSphere(transform.position, 0.15f, playerLayer); //Change to just basicRange when we find the right numbers

                    foreach (Collider player in playerHit)
                    {
                        player.GetComponent<Behavior>().takeDmg(30);
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
        Destroy(gameObject);
    }
    public void SetTarget(Vector3 pos)
    {
        target = pos;
    }

}
