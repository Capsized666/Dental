using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    LoaderBar Bar;

    void Start()
    {
        Bar = GetComponentInChildren<LoaderBar>(); 
    }

    void LateUpdate()
    {
        
    }
}
