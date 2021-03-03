using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reversePause : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (pauseGame.singleton.stateOfGame)
        {
            case pauseGame.generalState.PAUSED:
                gameObject.transform.position = originalPosition + new Vector3(5000, 0, 0);
                
                break;
            case pauseGame.generalState.PLAYING:
                gameObject.transform.position = originalPosition;
               
                break;
        }
    }
}
