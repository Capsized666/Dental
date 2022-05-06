using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AskinBar : MonoBehaviour
{

    public Image CurrentImage;
    public RectTransform currentScroll;
    public RectTransform rectContent;
    public GameObject TextPrfab;

    bool visiable = false;
    void Awake()
    {
        CurrentImage = GetComponent<Image>();
        //currentScroll = GetComponent<ScrollRect>();
        setSize();
    }

    private void setSize()
    {
        CurrentImage.rectTransform.sizeDelta = new Vector2(CanvasBeh.Instance.getSize().x*0.93f, CanvasBeh.Instance.getSize().y*0.39f);
        CurrentImage.rectTransform.anchoredPosition = new Vector2(CanvasBeh.Instance.getSize().x, -CanvasBeh.Instance.getSize().y*0.25f);

        currentScroll.sizeDelta = new Vector2(CurrentImage.rectTransform.sizeDelta.x, CurrentImage.rectTransform.sizeDelta.y * 0.75f);
        currentScroll.anchoredPosition = new Vector2(0, 0);
    }

    void Start()
    {
        UIEventSystem.Instance.onAskingBarShow += Show;
        UIEventSystem.Instance.onAskingBarHide += Hide;
    }
    private void OnDisable()
    {
        UIEventSystem.Instance.onAskingBarShow -= Show;
        UIEventSystem.Instance.onAskingBarHide -= Hide;
    }

    public void Show()
    {
        visiable = true;
        ScenaManager.Instance.currentState = gamestate.asking;
    }
    public void Hide()
    {
        visiable = false;
        ScenaManager.Instance.currentState = gamestate.moving;
    }

    public void CreateQuestion()
    {
        var quest = ServiceStuff.Instance.currLangPack.DQuestion;
        var answ = ServiceStuff.Instance.currLangPack.GetPatientAnswers(ServiceStuff.Instance.Chose);



    }

    // Update is called once per frame
    void Update()
    {
        if (visiable)
        {
            CurrentImage.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(CurrentImage.rectTransform.anchoredPosition.x, 0, Time.deltaTime * 6), CurrentImage.rectTransform.anchoredPosition.y);
        }
        else
        {
            CurrentImage.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(CurrentImage.rectTransform.anchoredPosition.x, CanvasBeh.Instance.getSize().x, Time.deltaTime * 6), CurrentImage.rectTransform.anchoredPosition.y);
        }
    }
}
