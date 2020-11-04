using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PositionUI : MonoBehaviour
{
    public GameObject mousePoint;
    GameObject selectedObject;

    public Text pointX;
    public Text pointY;
    public Text pointZ;

    public GameObject objectX;
    public GameObject objectY;
    public GameObject objectZ;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        pointX.text = Mathf.RoundToInt(mousePoint.transform.position.x).ToString();
        pointY.text = Mathf.RoundToInt(mousePoint.transform.position.y).ToString();
        pointZ.text = Mathf.RoundToInt(mousePoint.transform.position.z).ToString();
    }

    public void AddToUI(Transform objectTransform)
    {
        objectX.GetComponent<Text>().text = Mathf.RoundToInt(objectTransform.position.x).ToString();
        objectY.GetComponent<Text>().text = Mathf.RoundToInt(objectTransform.position.y).ToString();
        objectZ.GetComponent<Text>().text = Mathf.RoundToInt(objectTransform.position.z).ToString();
    }
}
