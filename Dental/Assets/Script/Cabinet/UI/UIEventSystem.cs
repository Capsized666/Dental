using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIEventSystem : MonoBehaviour
{
    public static UIEventSystem Instance;

    public event Action<string> onInfoTextShow;
    public event Action onInfoTextHide;

    public event Action onMedicalCardShow;
    public event Action onMedicalCardHide;




    public void InfoTextShowT(string name) {
        if (onInfoTextShow!=null)
        {
            onInfoTextShow(name);
            //print($"tres"+onInfoTextShow.ToString());
        }
    }
    public void InfoTextHideT()
    {
        if (onInfoTextHide != null)
        {
            onInfoTextHide();
        }
    }
    public void MedicalCardShowT()
    {
        if (onMedicalCardShow != null)
        {
            onMedicalCardShow();
        }
    }
    public void MedicalCardHideT()
    {
        if (onMedicalCardHide != null)
        {
            onMedicalCardHide();
        }
    }

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
