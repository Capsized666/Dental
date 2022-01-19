using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public sealed class CameraBeh : MonoBehaviour
{
    public static CameraBeh Instance;

    Camera curentCam;
    public float speed;
    void Awake()
    {
        Instance = this;
        curentCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setParrent(Transform p) {
        transform.parent = p;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
    public Transform parent() {
        return transform.parent;
    }

    public void setPosition(Vector3 v) {
        transform.position = Vector3.Lerp(transform.position,v,Time.deltaTime* speed);
    }

    public void lookAt(Transform t) {
        //transform.rotation = Quaternion.Lerp(transform.rotation, 
        //    Quaternion.LookRotation(t.position,Vector3.back), Time.time * speed);
        //;
        //transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(t.position - transform.position), speed * Time.deltaTime);
    }
}
