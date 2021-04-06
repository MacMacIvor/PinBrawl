using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
public class toStartSceen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> results = rayResult();
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.name == this.gameObject.name)
                {
                    SceneManager.LoadScene("StartScene");
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

}
