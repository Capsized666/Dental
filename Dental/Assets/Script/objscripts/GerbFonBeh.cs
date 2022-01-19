using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerbFonBeh : MonoBehaviour
{
    RectTransform rt;
    RectTransform parentRT;

    
    void Start()
    {
        rt = GetComponent<RectTransform>();
        parentRT = gameObject.transform.parent.GetComponentInParent<RectTransform>();

    }

    
    void Update()
    {
        if (parentRT.sizeDelta.y!=rt.sizeDelta.y)
        {
            rt.sizeDelta = new Vector3(parentRT.sizeDelta.y, parentRT.sizeDelta.y);
        }

    }
}


        
