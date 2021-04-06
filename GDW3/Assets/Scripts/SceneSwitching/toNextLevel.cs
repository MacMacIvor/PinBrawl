using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
public class toNextLevel : MonoBehaviour
{
    public static toNextLevel singleton = null;
    public void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            return;
        }
        Destroy(this);
    }

    static int levelToLoad = 1;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> results = rayResult();
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.name == this.gameObject.name)
                {
                    switch (levelToLoad)
                    {
                        case 0:
                            SceneManager.LoadScene("prototype_tutorial");
                            break;
                        case 1:
                            SceneManager.LoadScene("First_Level");
                            break;
                    }
                }
            }
        }
    }
    static List<RaycastResult> rayResult()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> rayRes = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, rayRes);
        return rayRes;
    }


    public void callNext(int level)
    {
        switch (level) {
            case 1:
                levelToLoad = level;
                SceneManager.LoadScene("NextLevel");
                break;
        }
    }

}
