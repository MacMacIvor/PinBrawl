using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{

    public void updateLength(float health)
    {
        if (health <= 0)
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0.2f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(health / 100, 0.2f, 0.2f);
        }
    }

    public void changeVisibility(bool isVisible)
    {
        switch (isVisible)
        {
            case true:
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f, 1.0f));
                break;
            case false:
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f, 0.0f));

                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
