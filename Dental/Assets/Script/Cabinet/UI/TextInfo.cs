using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInfo : MonoBehaviour
{
    Text InfoText;
    Dictionary<Lang, string> nameDic = new Dictionary<Lang, string>();
    public bool setvisible { get; set; } = false;
    void Awake()
    {
        InfoText = GetComponent<Text>();
        UIEventSystem.Instance.onInfoTextShow += InfoShow;
        UIEventSystem.Instance.onInfoTextHide += InfoHide;
    }
    private void OnDisable()
    {
        UIEventSystem.Instance.onInfoTextShow -= InfoShow;
        UIEventSystem.Instance.onInfoTextHide -= InfoHide;

    }
    void InfoShow(string s)
    {
        if (ServiceStuff.Instance != null)
        {
            nameDic = ServiceStuff.Instance.getUIDict(s);
            InfoText.text = nameDic[ServiceStuff.Instance.getLang()];
        }
        else { InfoText.text = s; }

        InfoText.color = new Color(InfoText.color.r, InfoText.color.g, InfoText.color.b, Mathf.Lerp(InfoText.color.a, 255,1));
    }
    void InfoHide()
    {
        InfoText.color = new Color(InfoText.color.r, InfoText.color.g, InfoText.color.b, Mathf.Lerp(InfoText.color.a, 0, 1));
    }


    void Update()
    {
        //if (setvisible)
        //{
        //}
        //else
        //{
        //    InfoText.color = new Color(InfoText.color.r, InfoText.color.g, InfoText.color.b, Mathf.Lerp(InfoText.color.a, 0, 1));
        //}
        //sizeWork(CanvasBeh.Instance.getSize());
    }

    private void sizeWork(Vector2 size)
    {
        
    }

    private void LateUpdate()
    {
       // setvisible = false;
    }

    public void SetText(string s) {
        InfoText.text = s;
    }

        


}
