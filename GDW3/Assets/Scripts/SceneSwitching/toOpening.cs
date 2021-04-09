using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class toOpening : MonoBehaviour
{
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 14.0f || Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}