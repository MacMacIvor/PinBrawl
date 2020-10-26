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
    private bool isEditing = false;
    private bool isEDown = false;
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (isEDown == false)
            {
                isEditing = !isEditing;

            }
            isEDown = true;
        }
        else
        {
            isEDown = false;
        }

        switch (isEditing)
        {
            case true:
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.position += Vector3.down;
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.position += Vector3.up;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.position += Vector3.left;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.position += Vector3.right;
                }
                break;
            case false:
                Vector3 newPos = characterPos.position + cameraOffset;
                transform.position = Vector3.Slerp(transform.position, newPos, smoothfactor);
                break;
        }
    }
}
