using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    RectTransform rt;
    bool isClose;
    public guideInput gInput;
    



    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    void Start()
    {
        isClose = true;
        rt.sizeDelta = new Vector2(MCUI.Instance.getCanvasSize().x * 0.16f,
            MCUI.Instance.getCanvasSize().y
            ); 
    }

    void LateUpdate()
    {
        rt.sizeDelta = new Vector2(MCUI.Instance.getCanvasSize().x * 0.16f,
        MCUI.Instance.getCanvasSize().y);
        
        positionInSpace();
    }

    private void positionInSpace()
    {
        if (isClose)
        {
            rt.anchoredPosition = new Vector2(
                Mathf.Lerp(rt.anchoredPosition.x, 0, Time.deltaTime * 1.5f)
                , Mathf.Lerp(rt.anchoredPosition.y, 0, Time.deltaTime * 1.5f)
                );
        }
        else
        {
            rt.anchoredPosition = new Vector2(
                Mathf.Lerp(rt.anchoredPosition.x, -rt.sizeDelta.x, Time.deltaTime * 1.5f)
                , Mathf.Lerp(rt.anchoredPosition.y, 0, Time.deltaTime * 1.5f)
                );
        }
    }

    public void changeState() {
        isClose = !isClose;
    }
}