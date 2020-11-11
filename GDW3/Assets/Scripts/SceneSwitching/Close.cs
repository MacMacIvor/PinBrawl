using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Close : MonoBehaviour
{
    static float timer = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }
    }
}
