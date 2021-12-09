using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public sealed class CameraBeh : MonoBehaviour
{
    public static CameraBeh Instance;

    Camera curentCam;

    void Awake()
    {
        Instance = this;
        curentCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Unparent() {
        transform.parent = null;
    }

    
}
