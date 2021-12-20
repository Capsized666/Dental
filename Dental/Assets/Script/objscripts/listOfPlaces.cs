using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listOfPlaces : MonoBehaviour
{

    RectTransform rt;
    bool isClose;
    
    public RectTransform GuidesMap;
    
    void Awake()
    {
        rt = GetComponent<RectTransform>();
        isClose = false;
    }

    private void Start()
    {
    
    }

    

    void LateUpdate()
    {
        if (isClose)
        {
            rt.anchoredPosition = new Vector2(
                rt.anchoredPosition.x
                , Mathf.Lerp(rt.anchoredPosition.y, -GuidesMap.sizeDelta.y, Time.deltaTime * 1.5f)
                );
        }
        else
        {
            rt.anchoredPosition = new Vector2(
                rt.anchoredPosition.x
                , Mathf.Lerp(rt.anchoredPosition.y, -GuidesMap.sizeDelta.y+rt.sizeDelta.y, Time.deltaTime * 1.5f)
                );
        }

        var y = rt.sizeDelta.y != MCUI.Instance.getCanvasSize().y - GuidesMap.sizeDelta.y ?
            MCUI.Instance.getCanvasSize().y - GuidesMap.sizeDelta.y:
            rt.sizeDelta.y;
        var x = GuidesMap.sizeDelta.x != rt.sizeDelta.x ?
            GuidesMap.sizeDelta.x:
            rt.sizeDelta.x;
        rt.sizeDelta = new Vector2(x, y);
        rt.anchoredPosition = new Vector2(GuidesMap.sizeDelta.x + GuidesMap.anchoredPosition.x - rt.sizeDelta.x,
            rt.anchoredPosition.y);
    }

    public void changeVisible()
    {
        isClose = isClose ? false : true;
    }

}