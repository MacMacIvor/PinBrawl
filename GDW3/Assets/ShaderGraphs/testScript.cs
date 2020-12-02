using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public Material mat1;
    public Material mat2;

    float activateCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab) && activateCooldown <= 0)
        {
            mat1.SetFloat("Vector1_F3FF3BC", 1);
            mat2.SetFloat("Vector1_2714CBC6", 1);
            activateCooldown = 3;
        }
        else if (activateCooldown <= 0)
        {
            mat1.SetFloat("Vector1_F3FF3BC", 0);
            mat2.SetFloat("Vector1_2714CBC6", 0);
        }
        activateCooldown -= Time.deltaTime;
    }
}
