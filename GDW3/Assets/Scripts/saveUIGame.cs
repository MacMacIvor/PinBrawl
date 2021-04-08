using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveUIGame : MonoBehaviour
{
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
                gameObject.transform.position = originalPosition;
                if ((Input.GetMouseButtonDown(0)) && Input.mousePosition.x > transform.position.x - transform.localScale.x * 100.0f && Input.mousePosition.x < transform.position.x + transform.localScale.x * 100.0f && Input.mousePosition.y < transform.position.y + transform.localScale.y * 100.0f && Input.mousePosition.y > transform.position.y - transform.localScale.y * 100.0f)
                {
                    //saveLoadingManager.singleton.saveFile(Application.dataPath + "/SaveData/gameSaveData.txt");
                    //saveLoadingManager.singleton.savePlayer(Application.dataPath + "/SaveData/gameSaveDataPlayer.txt");
                }
                    break;
            case pauseGame.generalState.PLAYING:
                gameObject.transform.position = originalPosition + new Vector3(1500, 0, 0);
                break;
        }
    }
}
