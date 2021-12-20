using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBar : MonoBehaviour
{
    RectTransform rect;
    public Vector2 size;
    public Vector2 offset;
    public InputSlider switcher;


    // Start is called before the first frame update
    void Start()
    {
        switcher = FindObjectOfType<InputSlider>();
        rect = GetComponent<RectTransform>();
    }

    
    void LateUpdate()
    {
        sizeManager();
    }
     
    private void sizeManager()
    {
        rect.sizeDelta = new Vector3(
            MCUI.Instance.getCanvasSize().x * size.x,
            MCUI.Instance.getCanvasSize().y * size.y);

        /*
            rect.anchoredPosition = new Vector2(0, 
                Mathf.Lerp(MCUI.Instance.getCanvasSize().y * offset.y*koef(switcher), 0, Time.deltaTime * 1.5f)
                );
        rect.anchoredPosition = new Vector2(
             MCUI.Instance.getCanvasSize().x * offset.x,
             MCUI.Instance.getCanvasSize().y * offset.y
             );
        */

        if (switcher.isHide)
        {
            rect.anchoredPosition = new Vector2(0,
             Mathf.Lerp(rect.anchoredPosition.y, (int)(MCUI.Instance.getCanvasSize().y * -offset.y + rect.sizeDelta.y), Time.deltaTime * 1.5f)
             );
        }
        else
        {
            rect.anchoredPosition = new Vector2(0,
             Mathf.Lerp(rect.anchoredPosition.y,(int)(MCUI.Instance.getCanvasSize().y * offset.y),  Time.deltaTime * 1.5f)
             );
        }



    }

    public int koef(InputSlider s) {
        int k = s.isHide ? 1 : -1;
        return k;
    }

        
}