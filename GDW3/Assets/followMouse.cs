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


        //Vector3 mousePos = Input.mousePosition;
        //mousePos.z = mousePos.y;

        //Vector3 exPoint = Camera.main.ScreenToWorldPoint(mousePos);
        //exPoint.z = exPoint.y;
        //exPoint.y = 0;
        //Vector3 charPos = characterPos.position;
        //charPos.y = 0;

        //exPoint.x = exPoint.x - charPos.x;
        //exPoint.z = exPoint.z - charPos.z;


        //float angle = Mathf.Atan2(exPoint.x, exPoint.z);



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
       

        
        //transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        //transform.RotateAround(characterPos.position, new Vector3(0, 1, 0), angle);
        //transform.position = new Vector3(characterPos.position.x + pointOffset.x, transform.position.y, characterPos.position.z + ((angle > -Mathf.PI && angle < -Mathf.PI/2) ? -Mathf.Cos(angle) : Mathf.Cos(angle)) * pointOffset.z);
        //((angle < 0 && angle > -Mathf.PI/2) ? Mathf.Sin(angle) : -Mathf.Sin(angle)) * 

        //Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //targetPos.x = targetPos.y;
        //targetPos.y = 0;
        //transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.3f * Time.deltaTime);

        //Debug.Log(targetPos.x);
        //Debug.Log(targetPos.z);

    }

    

}
