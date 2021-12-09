using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableEcvip : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        print("zastolom");
        CameraBeh.Instance.Unparent();

    }
}
