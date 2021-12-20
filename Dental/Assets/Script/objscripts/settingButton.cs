using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class settingButton : MonoBehaviour, IPointerDownHandler
{
    RectTransform _rect;
    public SettingPanel sp;
    public float size;

    public void OnPointerDown(PointerEventData eventData)
    {
        sp.changeState();
    }

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }
    private void LateUpdate()
    {
        sizeManager();
    }

    private void sizeManager()
    {
        if (MCUI.Instance.getCanvasSize().y* size != _rect.sizeDelta.y
            )
        {
            _rect.sizeDelta = new Vector3(MCUI.Instance.getCanvasSize().y * size,
                MCUI.Instance.getCanvasSize().y * size);
        } 
            

    
    }
}
