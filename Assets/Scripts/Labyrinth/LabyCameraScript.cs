using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LabyCameraScript : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraAnchor;
    [SerializeField]
    private GameObject cameraAnchor1;

    private float camEulerX;
    private float camEulerY;
    private float ancEulerX;
    private float ancEulerY;
    private Vector3 rod;
    
    void Start()
    {
        camEulerX = this.transform.eulerAngles.x;
        camEulerY = this.transform.eulerAngles.y;
        ancEulerX = 0;
        ancEulerY = 0;
        rod = this.transform.position - cameraAnchor.transform.position;
        LabyState.firstPersonView = false;
    }

    
    void Update()
    {
		if (LabyState.isPaused) return;

		float mh = Input.GetAxis("Mouse X");
        float mv = Input.GetAxis("Mouse Y");

        camEulerY += mh;
        ancEulerY += mh;
        float minVAngle = LabyState.firstPersonView ? 5 : 35;
        float maxVAngle = LabyState.firstPersonView ? 45 : 85;

        if (camEulerX - mv >= minVAngle && camEulerX - mv <= maxVAngle)
        {
            camEulerX -= mv;
            ancEulerX -= mv;
        }
        else
        {
            if (camEulerX - mv < minVAngle) 
            {
                ancEulerX += (minVAngle - camEulerX);
                camEulerX = minVAngle;
            }
            else
            {
				ancEulerX -= (camEulerX - maxVAngle);
				camEulerX = maxVAngle;
            }
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            LabyState.firstPersonView = !LabyState.firstPersonView;
        }
    }

	private void LateUpdate()
	{
        this.transform.eulerAngles = new Vector3(camEulerX, camEulerY, 0);
        if (LabyState.firstPersonView)
        {
            this.transform.position = cameraAnchor1.transform.position;
        }
        else
        {
            this.transform.position = cameraAnchor.transform.position + Quaternion.Euler(ancEulerX, ancEulerY, 0) * rod;
        }
    }
}
