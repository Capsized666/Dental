using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class MovementBeh : MonoBehaviour
{
    Canvas canvas;
    
    
    void Awake()
    {
        canvas = GetComponent<Canvas>();
    }



    void Update()
    {
        
    }
}
