using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLength : MonoBehaviour
{


    public void updateLength(float percent)
    {
        gameObject.transform.localScale = new Vector3(percent, 1, 1);
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
