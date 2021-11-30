using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstPlaneButtonBeh : MonoBehaviour, IPointerDownHandler
{
    public enum FPButton { 
    newgame=0,
    option=1,
    exit=2
    }
    public FPButton currentcase;
    public FirstPlaneBeh parent;
    
    void Awake()
    {
        parent = gameObject.GetComponentInParent<FirstPlaneBeh>();
    }

    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (currentcase)
        {
            case FPButton.newgame:
                //zagruz sceni
                //print("ng");
                break;
            case FPButton.option:
                //print("op");
                break;
            case FPButton.exit:
                //print("ex");
                Application.Quit();
                break;
            default:
                break;
        }
    }
}
