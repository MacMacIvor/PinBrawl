using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtPoint : MonoBehaviour
{
    public GameObject point;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInParent<Behavior>().locked == false)
            gameObject.transform.LookAt(new Vector3(point.transform.position.x, 7.6f, point.transform.position.z));
    }
}
