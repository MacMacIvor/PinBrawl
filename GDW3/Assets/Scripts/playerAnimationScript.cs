using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimationScript : MonoBehaviour
{
    public static playerAnimationScript singleton = null;
    public void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            return;
        }
        Destroy(this);
    }

    bool receivedAttack = false;
    bool receivedIddle = false;
    bool receivedHurt = false;
    bool receivedRun = false;
    public Animator playerAnimation;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (receivedAttack == false)
        {
            playerAnimation.SetBool("isAttacking", false);
        }
        else
        {
            receivedAttack = false;
        }
        if (receivedHurt == false)
        {
            playerAnimation.SetBool("isHit", false);
        }
        else
        {
            receivedHurt = false;
        }
       
        if (receivedRun == false)
        {
            playerAnimation.SetBool("isMoving", false);
        }
        else
        {
            receivedRun = false;
        }
    }
    public void playAttack()
    {
        //playerAnimation.CrossFade("PlayerAttack", 0.05f);
        playerAnimation.SetBool("isAttacking", true);
            receivedAttack = true;
    }
    public void playIddle()
    {
        //playerAnimation.CrossFade("PlayerIddle", 0.2f);
            receivedIddle = true;
    }
    public void playHurt()
    {
        //playerAnimation.CrossFade("PlayerHit", 0.05f);
        playerAnimation.SetBool("isHit", true);
            receivedHurt = true;
    }
    public void playRun()
    {
        //playerAnimation.CrossFade("PlayerMove", 0.1f);
        playerAnimation.SetBool("isMoving", true);
            receivedRun = true;
    }
}
