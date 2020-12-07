using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
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

    static int levelToLoad = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0)) && Input.mousePosition.x > transform.position.x - transform.localScale.x * 100.0f && Input.mousePosition.x < transform.position.x + transform.localScale.x * 100.0f && Input.mousePosition.y < transform.position.y + transform.localScale.y * 100.0f && Input.mousePosition.y > transform.position.y - transform.localScale.y * 100.0f)
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
