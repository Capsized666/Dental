using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class guideInput : MonoBehaviour
{
    private Camera cam;
    public MapObjBeh mob;

    //[SerializeField]
    float minView = 2;
    float minZoom = 2;

    float speedView = 2;
    float speedZoom = 2;
    float FOV;

    [Space]
    public ControlPanelBeh viev;
    public ControlPanelBeh zoom;
    /**
     

    public delegate float ViewChange();
    
    public delegate float ViewZoom();
     */



    private Vector2 posit = Vector2.zero;

    private void Awake()
    {
        if (cam== null) {
            cam = GetComponent<Camera>();
        }
        transform.eulerAngles = posit;
        FOV = cam.fieldOfView;
    }

    public void setViewSpeed(float t) {
        speedView = t* minView;
    }                 
    public void setZoomSpeed(float t)
    {
        speedZoom = t* minZoom;
    }


    void FixedUpdate()
    {
        if ( Input.touchCount ==1)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 pos = touch.position;
            if (touch.phase == TouchPhase.Moved)
            {
                pos.x = (pos.x - (float)Screen.width / 2.0f) / (float)Screen.width / 2.0f; 
                pos.y = (pos.y - (float)Screen.height / 2.0f) / (float)Screen.height / 2.0f;
            }
            posit += new Vector2(-pos.x, pos.y);
        }


        if (Input.GetKey(KeyCode.Mouse1))
        {
            posit += new Vector2(-speedView * Input.GetAxis("Mouse Y"), 
                                speedView * Input.GetAxis("Mouse X"));
        }
        //Mouse ScrollWheel

        if (Input.GetAxis("Mouse ScrollWheel") !=0 & !mob.isOnPoint())
        {
            FOV += Input.GetAxis("Mouse ScrollWheel") * speedZoom;
        }
        
        FOV = FOV > 60 ? 60 : FOV;
        FOV = FOV < 25 ? 25 : FOV;
        transform.eulerAngles = posit;
        cam.fieldOfView = FOV;
    }

    public void changeView(Vector2 v) {
        posit += v * speedView;
    }
    public void changeFOV(float v) {
        FOV += v * speedZoom;
    }

    private void LateUpdate()
    {
        setViewSpeed(viev.getSliderValue());
        setZoomSpeed(zoom.getSliderValue());
    }
}