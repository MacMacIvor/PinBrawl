using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class pointsManager : MonoBehaviour
{
    public static pointsManager pointsSingleton = null;

    Text pointsString;
    string filePath;

    static float points = 0;
    private int onOff = 0;
    [Range(0, 2)]
    public float pointsModifyer = 0.3f;
    public void Awake()
    {
        if (pointsSingleton == null)
        {
            pointsSingleton = this;
            return;
        }
        Destroy(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.dataPath + "SomethingWeShallFillInLater.txt";

    }

    // Update is called once per frame
    void Update()
    {
        switch (onOff)
        {
            case 0:
                break;
            case 1:
                points += Time.deltaTime * pointsModifyer;
                break;
        }
        pointsString.text = points.ToString();
    }

    public void addPoints(float pointsToAdd)
    {
        points += pointsToAdd;
    }
    public void resetPoints()
    {
        points = 0;
    }
    public float getPoints()
    {
        return points;
    }
    public void switchOnOff(int OnOff)
    {
        onOff = OnOff;
    }
    public void saveToFile()
    {
        using (StreamWriter writter = new StreamWriter(filePath))
        {
            writter.WriteLine(points.ToString());
        }
    }
}
