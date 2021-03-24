using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class largeShooterController : MonoBehaviour
{

    public static largeShooterController singleton = null;
    public void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            return;
        }
        Destroy(this);
    }



    public Animator animation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playAttack()
    {
        animation.SetTrigger("shoot");
    }
    public void playWalk(float num)
    {
        animation.SetFloat("walking", num);
    }
    public void playDeath()
    {
        animation.SetTrigger("dead");
    }
    public void playHit()
    {
        animation.SetTrigger("hit");
    }
}
