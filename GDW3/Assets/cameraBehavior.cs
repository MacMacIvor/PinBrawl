using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBehavior : MonoBehaviour
{

    public Transform characterPos;
    private Vector3 cameraOffset;
    [Range(0.001f, 1.0f)]
    public float smoothfactor = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - characterPos.position; //Remembers the offset you had put in unity
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = characterPos.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothfactor);
    }
}
