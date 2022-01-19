using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class uiInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum buttons2input { 
        Left=0,
        Up=1,
        Down=2,
        Right,
        ZIn,
        ZOut
    }

    public buttons2input currBeh;

    guideInput giScript;
    RectTransform rect;
    RectTransform parent;
    bool isPresed;
    int index;
    int childCount;
    // Start is called before the first frame update
    void Start()
    {
        giScript = FindObjectOfType<guideInput>();
        childCount = gameObject.transform.parent.childCount;
        parent = gameObject.transform.parent.gameObject.GetComponent<RectTransform>();// GetComponentInParent<RectTransform>();
        rect = GetComponent<RectTransform>();
        index = transform.GetSiblingIndex()+1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPresed)
        {
            clicWork();
        }
        rect.sizeDelta = new Vector2(parent.sizeDelta.y*0.95f, parent.sizeDelta.y * 0.95f);
        var posX = parent.sizeDelta.x / childCount;
        rect.anchoredPosition = new Vector2(posX*index - rect.sizeDelta.x / 2,0);



    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPresed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isPresed = false;
    }

    void clicWork() { 
        switch (currBeh)
        {
            case buttons2input.Left:
                LeftOperation();
                break;
            case buttons2input.Up:
                UpOperation();
                break;
            case buttons2input.Down:
                DownOperation();
                break;
            case buttons2input.Right:
                RightOperation();
                break;
            case buttons2input.ZIn:
                ZInOperation();
                break;
            case buttons2input.ZOut:
                ZOutOperation();
                break;
        }
    
    }

    private void DownOperation()
    {
        giScript.changeView(new Vector2(1,0));
    }

    private void ZInOperation()
    {
        giScript.changeFOV(-1);
    }

    private void ZOutOperation()
    {
        giScript.changeFOV(1);
    }

    private void RightOperation()
    {
        giScript.changeView(new Vector2(0, 1));
    }

    private void UpOperation()
    {
        giScript.changeView(new Vector2(-1, 0));
    }

    private void LeftOperation()
    {
        giScript.changeView(new Vector2(0, -1));


    }

}
