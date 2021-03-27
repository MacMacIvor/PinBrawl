using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unPause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0)) && Input.mousePosition.x > transform.position.x - transform.localScale.x * 100.0f && Input.mousePosition.x < transform.position.x + transform.localScale.x * 100.0f && Input.mousePosition.y < transform.position.y + transform.localScale.y * 50.0f && Input.mousePosition.y > transform.position.y - transform.localScale.y * 50.0f)
        {
            pauseGame.singleton.stateOfGame = pauseGame.generalState.PLAYING;
        }
    }
}
