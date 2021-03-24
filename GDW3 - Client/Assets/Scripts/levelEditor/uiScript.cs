using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class uiScript : MonoBehaviour
{

    private int down = 0;
    public GameObject objectToSpawnPrefab;
    public Transform pointPos;
    private float mouseWheel = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position += new Vector3(10000, -10000, 0);

    }

    // Update is called once per frame
    private bool isEditing = false;
    private bool isEDown = false;
    void Update()
    {
        if (false)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (isEDown == false)
                {
                    isEditing = !isEditing;
                    gameObject.transform.position += (isEditing == false) ? new Vector3(10000, -10000, 0) : new Vector3(-10000, 10000, 0);
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
                    Vector3 mousePos = Input.mousePosition;
                    mouseWheel = Input.mouseScrollDelta.y;
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        if (down == 0)
                        {
                            if (transform.position.x - 50 <= mousePos.x && transform.position.x + 50 >= mousePos.x && transform.position.y - 50 <= mousePos.y && transform.position.y + 50 >= mousePos.y)
                            {
                                GameObject objects = Instantiate(objectToSpawnPrefab) as GameObject;
                                objects.name += mousePos.x * 0.1f * Random.Range(Random.Range(-1000000.0f, 0.0f), Random.Range(0.0f, 1000000.0f));
                                objects.transform.position = pointPos.transform.position;
                                objects.transform.parent = null;

                                gameObject.GetComponent<undoRedo>().callingSaveAddedObject("createObject", objects.name, objects, objects.transform.position);

                            }
                        }
                        down = 1;
                    }
                    else
                    {
                        down = 0;
                    }

                    if (mouseWheel != 0 && Input.mousePosition.x < 200)
                    {
                        transform.position += new Vector3(0, mouseWheel * 10, 0);
                    }
                    GetComponent<undoRedo>().doneCycle();

                    break;
            }
        }
    }
}
