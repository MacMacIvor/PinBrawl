using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            case false:
                break;
            case true:
                if (Input.GetMouseButton(1))
                {
                    Vector3 position = transform.position;

                    Ray aRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitStuff;

                    if (Physics.Raycast(aRay, out hitStuff) == true)
                    {
                        position = hitStuff.point;
                    }
                    transform.position = position;
                }
                break;
        }
    }
}
