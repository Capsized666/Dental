using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionPlaneBeh : MonoBehaviour
{
    public RectTransform rtLang;
    public RectTransform rtLanguage;
    public RectTransform rtLabel;
    public RectTransform rtback;
    public RectTransform rtmode;
    public RectTransform rtmodeName;
    //
    public TextMeshProUGUI lang;
    public TextMeshProUGUI language;
    public TextMeshProUGUI mode;
    public TextMeshProUGUI modeName;
    public TextMeshProUGUI back;
    public TextMeshProUGUI label;
    void Start()
    {
        
    }

    void Update()
    {
        sizeManager(CanvasBeh.Instance.getSize());
    }


    private void sizeManager(Vector2 mSize)
    {
        rtback.sizeDelta=  new Vector2(mSize.x * 0.22f, mSize.y * 0.10f);
        rtLang      .sizeDelta = rtback.sizeDelta;
        rtLanguage  .sizeDelta = rtback.sizeDelta;
        rtLabel     .sizeDelta = rtback.sizeDelta;
        rtmode      .sizeDelta = rtback.sizeDelta;
        rtmodeName  .sizeDelta = rtback.sizeDelta;
        /*
         */
        rtLabel.anchoredPosition    = Vector2.zero;
        rtback.anchoredPosition     = Vector2.zero;

        rtLang.anchoredPosition     = new Vector2(-rtback.sizeDelta.y, rtback.sizeDelta.y);
        rtmode.anchoredPosition     = new Vector2(-rtback.sizeDelta.y, -rtback.sizeDelta.y);

        rtLanguage.anchoredPosition = new Vector2(rtback.sizeDelta.y, rtback.sizeDelta.y);
        rtmodeName.anchoredPosition = new Vector2(rtback.sizeDelta.y, -rtback.sizeDelta.y);
    }
}