using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateControl : MonoBehaviour
{
    Animator animator;
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5.5f && timer < 7.85f)
        {
            animator.SetBool("isWalking", true);
        }
        else if (timer >= 7.85f)
        {
            animator.SetBool("isWalking", false);
        }
        if (timer > 12.0f)
        {
            //Next scene
        }
    }
}
