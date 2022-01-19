using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UInfo : MonoBehaviour
{
    Text info;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddText(string s) {
        info.text += $"\n{s}";
        
    }
}
