using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toPlayerStats : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0)) && Input.mousePosition.x > transform.position.x - transform.localScale.x * 100.0f && Input.mousePosition.x < transform.position.x + transform.localScale.x * 100.0f && Input.mousePosition.y < transform.position.y + transform.localScale.y * 100.0f && Input.mousePosition.y > transform.position.y - transform.localScale.y * 100.0f)
        {
            SceneManager.LoadScene("playerStats");
        }
    }
}
