using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGame : MonoBehaviour
{

    public static pauseGame singleton = null;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            return;
        }
        Destroy(this);
    }

    bool isPressed = false;

    public enum generalState
    {
        PAUSED,
        PLAYING
    }

    public generalState stateOfGame = generalState.PLAYING;

    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (stateOfGame)
        {
            case generalState.PLAYING:
                gameObject.transform.position = originalPosition + new Vector3(5000, 0, 0);
                if (Input.GetKey(KeyCode.Escape))
                {
                    if(isPressed == false)
                        stateOfGame = generalState.PAUSED;
                    isPressed = true;
                }
                else
                {
                    isPressed = false;
                }
                break;
            case generalState.PAUSED:
                gameObject.transform.position = originalPosition;
                if (Input.GetKey(KeyCode.Escape))
                {
                    if (isPressed == false)
                        stateOfGame = generalState.PLAYING;
                    isPressed = true;
                }
                else
                {
                    isPressed = false;
                }
                break;
        }
    }
}
