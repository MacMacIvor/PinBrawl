using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUI : MonoBehaviour
{
    private int down = 0;
    private bool isEditing = false;
    private bool isEDown = false;
    private int areVisible = 1;
    public GameObject ui0;
    public GameObject ui1;
    public GameObject ui2;
    public GameObject ui3;
    public GameObject ui4;
    public GameObject ui5;
    public GameObject ui6;
    public GameObject ui7;
    public GameObject ui8;
    public GameObject ui9;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position += new Vector3(10000, -10000, 0);
        areVisible *= -1;
        ui0.transform.position -= new Vector3(10000, 0, 0) * areVisible;
        ui1.transform.position -= new Vector3(10000, 0, 0) * areVisible;
        ui2.transform.position -= new Vector3(10000, 0, 0) * areVisible;
        ui3.transform.position -= new Vector3(10000, 0, 0) * areVisible;
        ui4.transform.position -= new Vector3(10000, 0, 0) * areVisible;
        ui5.transform.position -= new Vector3(10000, 0, 0) * areVisible;
        ui6.transform.position -= new Vector3(10000, 0, 0) * areVisible;
        ui7.transform.position -= new Vector3(10000, 0, 0) * areVisible;
        ui8.transform.position -= new Vector3(10000, 0, 0) * areVisible;
        ui9.transform.position -= new Vector3(10000, 0, 0) * areVisible;
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
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        if (down == 0)
                        {
                            if (transform.position.x - 50 <= mousePos.x && transform.position.x + 50 >= mousePos.x && transform.position.y - 50 <= mousePos.y && transform.position.y + 50 >= mousePos.y)
                            {
                                areVisible *= -1;
                                ui0.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui1.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui2.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui3.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui4.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui5.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui6.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui7.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui8.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui9.transform.position -= new Vector3(10000, 0, 0) * areVisible;

                            }
                            else if ((ui0.transform.position.x - 50 <= mousePos.x && ui0.transform.position.x + 50 >= mousePos.x && ui0.transform.position.y - 50 <= mousePos.y && ui0.transform.position.y + 50 >= mousePos.y))
                            {
                                //Call the save object
                                string filePath = Application.dataPath + "/SaveData/SaveData0.txt";
                                gameObject.GetComponent<savingScript>().loadCommand(filePath);
                                areVisible *= -1;
                                ui0.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui1.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui2.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui3.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui4.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui5.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui6.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui7.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui8.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui9.transform.position -= new Vector3(10000, 0, 0) * areVisible;

                            }
                            else if ((ui1.transform.position.x - 50 <= mousePos.x && ui1.transform.position.x + 50 >= mousePos.x && ui1.transform.position.y - 50 <= mousePos.y && ui1.transform.position.y + 50 >= mousePos.y))
                            {
                                //Call the save object
                                string filePath = Application.dataPath + "/SaveData/SaveData1.txt";
                                gameObject.GetComponent<savingScript>().loadCommand(filePath);
                                areVisible *= -1;
                                ui0.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui1.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui2.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui3.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui4.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui5.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui6.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui7.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui8.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui9.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                            }
                            else if ((ui2.transform.position.x - 50 <= mousePos.x && ui2.transform.position.x + 50 >= mousePos.x && ui2.transform.position.y - 50 <= mousePos.y && ui2.transform.position.y + 50 >= mousePos.y))
                            {
                                //Call the save object
                                string filePath = Application.dataPath + "/SaveData/SaveData2.txt";
                                gameObject.GetComponent<savingScript>().loadCommand(filePath);
                                areVisible *= -1;
                                ui0.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui1.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui2.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui3.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui4.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui5.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui6.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui7.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui8.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui9.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                            }
                            else if ((ui3.transform.position.x - 50 <= mousePos.x && ui3.transform.position.x + 50 >= mousePos.x && ui3.transform.position.y - 50 <= mousePos.y && ui3.transform.position.y + 50 >= mousePos.y))
                            {
                                //Call the save object
                                string filePath = Application.dataPath + "/SaveData/SaveData3.txt";
                                gameObject.GetComponent<savingScript>().loadCommand(filePath);
                                areVisible *= -1;
                                ui0.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui1.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui2.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui3.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui4.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui5.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui6.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui7.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui8.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui9.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                            }
                            else if ((ui4.transform.position.x - 50 <= mousePos.x && ui4.transform.position.x + 50 >= mousePos.x && ui4.transform.position.y - 50 <= mousePos.y && ui4.transform.position.y + 50 >= mousePos.y))
                            {
                                //Call the save object
                                string filePath = Application.dataPath + "/SaveData/SaveData4.txt";
                                gameObject.GetComponent<savingScript>().loadCommand(filePath);
                                areVisible *= -1;
                                ui0.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui1.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui2.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui3.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui4.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui5.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui6.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui7.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui8.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui9.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                            }
                            else if ((ui5.transform.position.x - 50 <= mousePos.x && ui5.transform.position.x + 50 >= mousePos.x && ui5.transform.position.y - 50 <= mousePos.y && ui5.transform.position.y + 50 >= mousePos.y))
                            {
                                //Call the save object
                                string filePath = Application.dataPath + "/SaveData/SaveData5.txt";
                                gameObject.GetComponent<savingScript>().loadCommand(filePath);
                                areVisible *= -1;
                                ui0.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui1.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui2.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui3.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui4.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui5.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui6.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui7.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui8.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui9.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                            }
                            else if ((ui6.transform.position.x - 50 <= mousePos.x && ui6.transform.position.x + 50 >= mousePos.x && ui6.transform.position.y - 50 <= mousePos.y && ui6.transform.position.y + 50 >= mousePos.y))
                            {
                                //Call the save object
                                string filePath = Application.dataPath + "/SaveData/SaveData6.txt";
                                gameObject.GetComponent<savingScript>().loadCommand(filePath);
                                areVisible *= -1;
                                ui0.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui1.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui2.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui3.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui4.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui5.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui6.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui7.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui8.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui9.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                            }
                            else if ((ui7.transform.position.x - 50 <= mousePos.x && ui7.transform.position.x + 50 >= mousePos.x && ui7.transform.position.y - 50 <= mousePos.y && ui7.transform.position.y + 50 >= mousePos.y))
                            {
                                //Call the save object
                                string filePath = Application.dataPath + "/SaveData/SaveData7.txt";
                                gameObject.GetComponent<savingScript>().loadCommand(filePath);
                                areVisible *= -1;
                                ui0.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui1.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui2.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui3.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui4.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui5.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui6.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui7.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui8.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui9.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                            }
                            else if ((ui8.transform.position.x - 50 <= mousePos.x && ui8.transform.position.x + 50 >= mousePos.x && ui8.transform.position.y - 50 <= mousePos.y && ui8.transform.position.y + 50 >= mousePos.y))
                            {
                                //Call the save object
                                string filePath = Application.dataPath + "/SaveData/SaveData8.txt";
                                gameObject.GetComponent<savingScript>().loadCommand(filePath);
                                areVisible *= -1;
                                ui0.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui1.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui2.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui3.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui4.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui5.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui6.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui7.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui8.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui9.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                            }
                            else if ((ui9.transform.position.x - 50 <= mousePos.x && ui9.transform.position.x + 50 >= mousePos.x && ui9.transform.position.y - 50 <= mousePos.y && ui9.transform.position.y + 50 >= mousePos.y))
                            {
                                //Call the save object
                                string filePath = Application.dataPath + "/SaveData/SaveData9.txt";
                                gameObject.GetComponent<savingScript>().loadCommand(filePath);
                                areVisible *= -1;
                                ui0.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui1.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui2.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui3.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui4.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui5.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui6.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui7.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui8.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui9.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                            }
                            else if (areVisible == 1)
                            {
                                areVisible *= -1;
                                ui0.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui1.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui2.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui3.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui4.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui5.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui6.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui7.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui8.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                ui9.transform.position -= new Vector3(10000, 0, 0) * areVisible;
                                //The rest
                            }
                            down = 1;
                        }

                    }
                    else
                    {
                        down = 0;
                    }
                    break;
            }
        }
    }
}
