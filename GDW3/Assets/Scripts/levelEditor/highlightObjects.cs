using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Rendering;

public class highlightObjects : MonoBehaviour
{
    Stack<string> actionList;
    Stack<string> actionListForRedo;
    struct objData 
    { 
    
      string name;
      GameObject typeOfObject;
      int typeOfTransformID;
      Vector3 positionOrTranslation;
      //Vec3 Rotation; Add this if it's added
    }

    static Stack<Vector3> distTransformed = new Stack<Vector3>();

    //Need to make a shader for the outline to work
    Color colour = new Color(0, 1, 1, 1);
    MeshRenderer render;

    public Material Highlighting;
    public Material originalMat;

    private bool isClicked = false;
    //private bool isOnMouse = false;

    private Vector3 startPosition;
    private int currentAxis = 0;
    private bool middleMouseClicked = false;
    private bool spaceClicked = false;
    private bool rClicked = false;
    private Vector3 mouseStartPosition;
    private int currentDisplacement = 0;

    private Vector3 mouseOffset;
    float mouseZoffPut;

    bool wasDeleted = false;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        actionList = new Stack<string>();
        actionListForRedo = new Stack<string>();
    }

    // Update is called once per frame
    private bool isEditing = true;
    private bool isEDown = false;

    void Update()
    {
        if (false)
        {
            if (isClicked) //Highlight when selected
            {
                GetComponent<Renderer>().material = Highlighting;
            }
            else //Un-highlight when not selected
            {
                GetComponent<Renderer>().material = originalMat;
            }

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

                    if (Input.GetKeyDown(KeyCode.Backspace))
                    {
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo = new System.Diagnostics.ProcessStartInfo("explorer.exe");
                        p.Start();
                    }

                    if (Input.GetMouseButton(0))
                    {

                        Ray aRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hitStuff;

                        if (Physics.Raycast(aRay, out hitStuff) == true)
                        {

                            if (hitStuff.transform.gameObject.name == gameObject.name)
                            {
                                isClicked = true;
                            }
                            else if (!Input.GetKey(KeyCode.LeftShift))
                            {
                                isClicked = false;
                            }
                        }

                    }

                    if (Input.GetKey(KeyCode.Delete) && isClicked)
                    {
                        //Destroy(gameObject);
                        gameObject.GetComponent<Renderer>().enabled = false;
                        string buff = gameObject.name.Substring(gameObject.name.Length - 8, 8);
                        if (buff != "_DELETED")
                        {
                            gameObject.name += "_DELETED";
                            //gameObject.SetActive(false); //When saving make you exclude any non-active gameobjects;
                            gameObject.GetComponent<undoRedo>().callingSaveAddedObject("delete", gameObject.name, gameObject, gameObject.transform.position);
                        }
                        wasDeleted = true;
                    }

                    if (Input.GetKey(KeyCode.Mouse2) && isClicked)
                    {

                        //This will be a drag option for quick placement
                        if (middleMouseClicked == false)
                        {
                            startPosition = transform.position;
                            middleMouseClicked = true;
                            mouseStartPosition = Input.mousePosition;
                        }
                        else
                        {
                            switch (currentAxis)
                            {
                                case 0: //z axis
                                    int theNewDisplacement = (int)((Input.mousePosition.y - mouseStartPosition.y) < 0 ? Mathf.Ceil((Input.mousePosition.y - mouseStartPosition.y) / 50) : Mathf.Floor((Input.mousePosition.y - mouseStartPosition.y) / 50));
                                    if (theNewDisplacement > currentDisplacement && currentDisplacement >= 0)
                                    {
                                        GameObject cloneOfObject = Instantiate(gameObject) as GameObject;
                                        cloneOfObject.transform.position = transform.position + new Vector3(0, 0, GetComponent<Renderer>().bounds.size.z + GetComponent<Renderer>().bounds.size.z * currentDisplacement);
                                        gameObject.name += Mathf.Pow(currentDisplacement * currentAxis * currentAxis * currentAxis, 3);
                                        currentDisplacement = theNewDisplacement;
                                        gameObject.GetComponent<undoRedo>().callingSaveAddedObject("createObject", cloneOfObject.name, cloneOfObject, cloneOfObject.transform.position);

                                    }
                                    else if (theNewDisplacement < currentDisplacement && currentDisplacement <= 0)
                                    {
                                        GameObject cloneOfObject = Instantiate(gameObject) as GameObject;
                                        cloneOfObject.transform.position = transform.position + new Vector3(0, 0, -GetComponent<Renderer>().bounds.size.z + GetComponent<Renderer>().bounds.size.z * currentDisplacement);
                                        gameObject.name += Mathf.Pow(currentDisplacement * currentAxis * currentAxis * currentAxis, 3);
                                        currentDisplacement = theNewDisplacement;
                                        gameObject.GetComponent<undoRedo>().callingSaveAddedObject("createObject", cloneOfObject.name, cloneOfObject, cloneOfObject.transform.position);

                                    }
                                    break;

                                case 1: //x axis
                                    int theNewDisplacement2 = (int)((Input.mousePosition.x - mouseStartPosition.x) < 0 ? Mathf.Ceil((Input.mousePosition.x - mouseStartPosition.x) / 100) : Mathf.Floor((Input.mousePosition.x - mouseStartPosition.x) / 100));
                                    if (theNewDisplacement2 > currentDisplacement)
                                    {
                                        currentDisplacement = theNewDisplacement2;
                                        GameObject cloneOfObject = Instantiate(gameObject) as GameObject;
                                        gameObject.name += Mathf.Pow(currentDisplacement * currentAxis * currentAxis * currentAxis, 3);
                                        cloneOfObject.transform.position = transform.position + new Vector3(GetComponent<Renderer>().bounds.size.x * currentDisplacement, 0, 0);
                                        gameObject.GetComponent<undoRedo>().callingSaveAddedObject("createObject", cloneOfObject.name, cloneOfObject, cloneOfObject.transform.position);
                                    }
                                    else if (theNewDisplacement2 < currentDisplacement && currentDisplacement <= 0)
                                    {
                                        GameObject cloneOfObject = Instantiate(gameObject) as GameObject;
                                        cloneOfObject.transform.position = transform.position + new Vector3(GetComponent<Renderer>().bounds.size.x * currentDisplacement, 0, 0);
                                        gameObject.name += Mathf.Pow(currentDisplacement * currentAxis * currentAxis * currentAxis, 3);
                                        currentDisplacement = theNewDisplacement2;
                                        gameObject.GetComponent<undoRedo>().callingSaveAddedObject("createObject", cloneOfObject.name, cloneOfObject, cloneOfObject.transform.position);
                                    }
                                    break;

                                case 2: //y axis
                                    int theNewDisplacement3 = (int)((Input.mousePosition.y - mouseStartPosition.y) < 0 ? Mathf.Ceil((Input.mousePosition.y - mouseStartPosition.y) / 100) : Mathf.Floor((Input.mousePosition.y - mouseStartPosition.y) / 100));
                                    if (theNewDisplacement3 > currentDisplacement)
                                    {
                                        currentDisplacement = theNewDisplacement3;
                                        GameObject cloneOfObject = Instantiate(gameObject) as GameObject;
                                        gameObject.name += Mathf.Pow(currentDisplacement * currentAxis * currentAxis * currentAxis, 3);
                                        cloneOfObject.transform.position = transform.position + new Vector3(0, GetComponent<Renderer>().bounds.size.y * currentDisplacement, 0);
                                        gameObject.GetComponent<undoRedo>().callingSaveAddedObject("createObject", cloneOfObject.name, cloneOfObject, cloneOfObject.transform.position);
                                    }
                                    else if (theNewDisplacement3 < currentDisplacement && currentDisplacement <= 0)
                                    {
                                        GameObject cloneOfObject = Instantiate(gameObject) as GameObject;
                                        cloneOfObject.transform.position = transform.position + new Vector3(0, GetComponent<Renderer>().bounds.size.y * currentDisplacement, 0);
                                        gameObject.name += Mathf.Pow(currentDisplacement * currentAxis * currentAxis * currentAxis, 3);
                                        currentDisplacement = theNewDisplacement3;
                                        gameObject.GetComponent<undoRedo>().callingSaveAddedObject("createObject", cloneOfObject.name, cloneOfObject, cloneOfObject.transform.position);
                                    }
                                    break;
                            }
                        }

                    }
                    else
                    {
                        currentDisplacement = 0;
                        middleMouseClicked = false;
                    }

                    if (Input.GetKey(KeyCode.Z) && !(Input.GetKey(KeyCode.LeftShift)))
                    {
                        currentAxis = 0;
                    }
                    else if (Input.GetKey(KeyCode.X) && !(Input.GetKey(KeyCode.LeftShift)))
                    {
                        currentAxis = 1;
                    }
                    else if (Input.GetKey(KeyCode.Y) && !(Input.GetKey(KeyCode.LeftShift)))
                    {
                        currentAxis = 2;
                    }


                    if (isClicked && Input.GetKey(KeyCode.Space))
                    {
                        if (spaceClicked == false)
                        {
                            if (distTransformed.Count == 0)
                            {
                                distTransformed.Push(gameObject.transform.position);
                            }
                            startPosition = transform.position;
                            mouseStartPosition = Input.mousePosition;
                            spaceClicked = true;
                        }
                        else
                        {
                            switch (currentAxis)
                            {
                                case 0: //z axis
                                    float theDisplacementDist = Input.mousePosition.y - mouseStartPosition.y;

                                    transform.position += new Vector3(0, 0, theDisplacementDist / 30.0f);

                                    startPosition = transform.position;
                                    mouseStartPosition = Input.mousePosition;
                                    break;

                                case 1: //x axis
                                    float theDisplacementDist2 = Input.mousePosition.x - mouseStartPosition.x;

                                    transform.position += new Vector3(theDisplacementDist2 / 30.0f, 0, 0);

                                    startPosition = transform.position;
                                    mouseStartPosition = Input.mousePosition;
                                    break;

                                case 2: //y axis
                                    float theDisplacementDist3 = Input.mousePosition.y - mouseStartPosition.y;

                                    transform.position += new Vector3(0, theDisplacementDist3 / 30.0f, 0);

                                    startPosition = transform.position;
                                    mouseStartPosition = Input.mousePosition;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (spaceClicked)
                        {
                            Vector3 temporary = gameObject.transform.position - distTransformed.Peek();
                            distTransformed.Pop();
                            gameObject.GetComponent<undoRedo>().callingSaveAddedObject("moved", gameObject.name, gameObject, temporary);
                        }
                        spaceClicked = false;
                    }


                    if (isClicked && Input.GetKey(KeyCode.R))
                    {
                        if (rClicked == false)
                        {
                            startPosition = transform.position;
                            mouseStartPosition = Input.mousePosition;
                            rClicked = true;
                        }
                        else
                        {
                            switch (currentAxis)
                            {
                                case 0: //z axis
                                    float theDisplacementDist = Input.mousePosition.x - mouseStartPosition.x;

                                    transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + theDisplacementDist / 30.0f, 1.0f);


                                    startPosition = transform.position;
                                    mouseStartPosition = Input.mousePosition;
                                    break;

                                case 1: //x axis
                                    float theDisplacementDist2 = Input.mousePosition.y - mouseStartPosition.y;

                                    transform.rotation = new Quaternion(transform.rotation.x + theDisplacementDist2 / 30.0f, transform.rotation.y, transform.rotation.z, 1.0f);

                                    startPosition = transform.position;
                                    mouseStartPosition = Input.mousePosition;
                                    break;

                                case 2: //y axis
                                    float theDisplacementDist3 = Input.mousePosition.x - mouseStartPosition.x;

                                    transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + theDisplacementDist3 / 30.0f, transform.rotation.z, 1.0f);

                                    startPosition = transform.position;
                                    mouseStartPosition = Input.mousePosition;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        rClicked = false;
                    }
                    break;
            }
        }
    }

    public bool deleteSelf(GameObject obj)
    {
        if (obj == gameObject)
        {

            //Destroy(gameObject);
            GetComponent<Renderer>().enabled = false;
            return true;
        }
        return false;
    }

    public bool createSelf(GameObject obj)
    {
        if (obj == gameObject)
        {

            //Destroy(gameObject);
            GetComponent<Renderer>().enabled = true;
            return true;
        }
        return false;
    }

    public bool deleteCommandReverse(GameObject obj)
    {
        if (obj == gameObject)
        {
            GetComponent<Renderer>().enabled = true;
            gameObject.name = gameObject.name.Remove(gameObject.name.Length - 8);
            return true;
        }
        return false;
    }
    public bool deleteCommandRedo(GameObject obj)
    {
        if (obj == gameObject)
        {
            GetComponent<Renderer>().enabled = false;
            gameObject.name += "_DELETED";

            return true;
        }
        return false;
    }

    public bool undoRedoTransformation(GameObject obj, Vector3 dist, int id)
    {
        if (obj == gameObject)
        {
            gameObject.transform.position += dist * id;
            return true;
        }
        return false;
    }

    public void trueDelete(GameObject obj)
    {
        if (obj == gameObject)
        {
            Destroy(gameObject);
        }
    }
}
