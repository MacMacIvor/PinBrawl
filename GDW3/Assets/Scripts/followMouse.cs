using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMouse : MonoBehaviour
{

    public Transform characterPos;
    private Vector3 pointOffset;

    const int STARTING_DIRECTION = 2;
    private int currentDirrection;


    [Range(0, 1)]
    public int mouse = 1;

    // Start is called before the first frame update
    void Start()
    {
        pointOffset = transform.position - characterPos.position;
        pointOffset.x = pointOffset.z;
        currentDirrection = STARTING_DIRECTION;
    }


    public void updateDirection(int dir)
    {
        currentDirrection = dir;
    }

    // Update is called once per frame
    void Update()
    {


        switch (mouse) {
            case 0:
                Vector3 pointPos = Quaternion.AngleAxis((currentDirrection == 1 ? 135 : (currentDirrection == 2 ? 90 : (currentDirrection == 3 ? 45 : (currentDirrection == 4 ? 180 : (currentDirrection == 5 ? 0
                                                            : (currentDirrection == 6 ? 225 : (currentDirrection == 7 ? 270 : 315))))))), Vector3.down) * (Vector3.right * pointOffset.z);
                transform.position = characterPos.position + pointPos;
                transform.LookAt(characterPos.position);
                break;
            case 1:
                Vector3 pointPosM = Camera.main.WorldToScreenPoint(characterPos.position);
                pointPosM = Input.mousePosition - pointPosM;
                float angle = Mathf.Atan2(pointPosM.y, pointPosM.x) * Mathf.Rad2Deg;
                pointPosM = Quaternion.AngleAxis(angle, Vector3.down) * (Vector3.right * pointOffset.z);
                transform.position = characterPos.position + pointPosM;
                transform.LookAt(characterPos.position);
                break;
        }
       

    }

    

}
