using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class undoRedo : MonoBehaviour
{
    
    struct objData
    {

        public string name;
        public GameObject typeOfObject;
        //public int typeOfTransformID;
        public Vector3 positionOrTranslation;
        //Vec3 Rotation; Add this if it's added
    }

    static Stack<objData> theObjects = new Stack<objData>();
    static Stack<objData> theRedoObjects = new Stack<objData>();
    static Stack<string> actionList = new Stack<string>();
    static Stack<string> actionListForRedo = new Stack<string>();
    static private bool isEditing = false;
    static private bool isEDown = false;
    static Stack<int> limitOne = new Stack<int>();
    static Stack<int> buttonPressed = new Stack<int>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (false)
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
                    if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Z))
                    {
                        if (buttonPressed.Count == 0)
                        {
                            buttonPressed.Push(1);
                        }
                        if (theObjects.Count != 0 && limitOne.Count == 0)
                        {
                            string holder = actionList.Peek();

                            switch (holder)
                            {
                                case "createObject":
                                    //Debug.Log("There is stuff in here");
                                    if (theObjects.Peek().typeOfObject.GetComponent<highlightObjects>().deleteSelf(theObjects.Peek().typeOfObject))
                                    {
                                        actionListForRedo.Push(holder);
                                        actionList.Pop();
                                        theRedoObjects.Push(theObjects.Peek());
                                        theObjects.Pop();
                                        limitOne.Push(1);
                                    }

                                    //gameObject.GetComponent<highlightObjects>().deleteSelf(objName, obj, position);
                                    break;
                                case "delete":
                                    if (theObjects.Peek().typeOfObject.GetComponent<highlightObjects>().deleteCommandReverse(theObjects.Peek().typeOfObject))
                                    {
                                        actionListForRedo.Push(holder);
                                        actionList.Pop();
                                        theRedoObjects.Push(theObjects.Peek());
                                        theObjects.Pop();
                                        limitOne.Push(1);
                                    }
                                    break;
                                case "moved":
                                    if (theObjects.Peek().typeOfObject.GetComponent<highlightObjects>().undoRedoTransformation(theObjects.Peek().typeOfObject, theObjects.Peek().positionOrTranslation, -1))
                                    {
                                        actionListForRedo.Push(holder);
                                        actionList.Pop();
                                        theRedoObjects.Push(theObjects.Peek());
                                        theObjects.Pop();
                                        limitOne.Push(1);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            ; //Do nothing because there is nothing to go back to!
                        }
                    }
                    else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Y))
                    {
                        if (buttonPressed.Count == 0)
                        {
                            buttonPressed.Push(1);
                        }
                        if (theRedoObjects.Count != 0 && limitOne.Count == 0)
                        {
                            string holder = actionListForRedo.Peek();

                            switch (holder)
                            {
                                case "createObject":
                                    if (theRedoObjects.Peek().typeOfObject.GetComponent<highlightObjects>().createSelf(theRedoObjects.Peek().typeOfObject))
                                    {
                                        actionList.Push(holder);
                                        actionListForRedo.Pop();
                                        theObjects.Push(theRedoObjects.Peek());
                                        theRedoObjects.Pop();
                                        limitOne.Push(1);

                                    }
                                    break;
                                case "delete":
                                    if (theRedoObjects.Peek().typeOfObject.GetComponent<highlightObjects>().deleteCommandRedo(theRedoObjects.Peek().typeOfObject))
                                    {
                                        actionList.Push(holder);
                                        actionListForRedo.Pop();
                                        theObjects.Push(theRedoObjects.Peek());
                                        theRedoObjects.Pop();
                                        limitOne.Push(1);

                                    }
                                    break;
                                case "moved":
                                    if (theRedoObjects.Peek().typeOfObject.GetComponent<highlightObjects>().undoRedoTransformation(theRedoObjects.Peek().typeOfObject, theRedoObjects.Peek().positionOrTranslation, 1))
                                    {
                                        actionList.Push(holder);
                                        actionListForRedo.Pop();
                                        theObjects.Push(theRedoObjects.Peek());
                                        theRedoObjects.Pop();
                                        limitOne.Push(1);

                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (buttonPressed.Count > 0)
                        {
                            buttonPressed.Pop();
                        }
                    }

                    break;
            }
        }
    }


    public void doneCycle()
    {
        if (limitOne.Count > 0 && buttonPressed.Count < 1)
        {
            limitOne.Pop();
        }
    }



    public void callingSaveAddedObject(string commandType, string objName, GameObject objInfo, Vector3 position)
    {
        if (actionListForRedo.Count != 0)
        {
            Stack<objData> special = new Stack<objData>();
            Stack<objData> tempo = new Stack<objData>();
            bool isPresent = false;
            do
            {
                if (actionListForRedo.Peek() == "delete")
                {
                    special.Push(theRedoObjects.Peek());
                    do
                    {
                        string tempName = special.Peek().name;
                        if (theObjects.Peek().name == tempName || theObjects.Peek().name == tempName.Remove(tempName.Length - 8, 8))
                        {
                            isPresent = true;
                        }
                        else
                        {
                            tempo.Push(theObjects.Peek());
                            theObjects.Pop();
                        }
                    } while (theObjects.Count > 0 && isPresent != true);

                    do
                    {
                        theObjects.Push(tempo.Peek());
                        tempo.Pop();
                    } while (tempo.Count > 0);

                    if (isPresent == true)
                    {
                        theRedoObjects.Pop();
                        actionListForRedo.Pop();
                    }
                    else
                    {
                        theRedoObjects.Peek().typeOfObject.GetComponent<highlightObjects>().trueDelete(theRedoObjects.Peek().typeOfObject);
                        actionListForRedo.Pop();
                        theRedoObjects.Pop();
                    }
                }
                else if (actionListForRedo.Peek() == "moved")
                {
                    theRedoObjects.Pop();
                    actionListForRedo.Pop();
                }
                else
                {
                    theRedoObjects.Peek().typeOfObject.GetComponent<highlightObjects>().trueDelete(theRedoObjects.Peek().typeOfObject);
                    actionListForRedo.Pop();
                    theRedoObjects.Pop();
                }
            } while (actionListForRedo.Count != 0);
        }
        //Debug.Log("Was Called");
        //This took longer then I wish it did to finally work, no idea why
        objData temp = new objData();
        temp.name = objName;
        temp.typeOfObject = objInfo;
        temp.positionOrTranslation = position;
        //temp.realTranslation = 
        actionList.Push(commandType);
        theObjects.Push(temp);
        //Debug.Log("Was Called");
    }
}
