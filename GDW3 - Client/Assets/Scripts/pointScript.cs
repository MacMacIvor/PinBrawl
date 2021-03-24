using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {
        
        switch (pauseGame.singleton.stateOfGame)
        {
            case pauseGame.generalState.PAUSED:
                break;
            case pauseGame.generalState.PLAYING:
                if (Input.GetMouseButton(1))
                {
                    Vector3 position = transform.position;

                    Ray aRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitStuff;

                    if (Physics.Raycast(aRay, out hitStuff) == true)
                    {
                        position = hitStuff.point;
                    }
                    transform.position = position;
                }
                break;
        }
    }
}
