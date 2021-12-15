using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirstPlaneBeh : MonoBehaviour
{
    public RectTransform newgame;
    public RectTransform optinon;
    public RectTransform exit;

    public TextMeshProUGUI gn;
    public TextMeshProUGUI opt;
    public TextMeshProUGUI ext;

    // Start is called before the first frame update
    void Update()
    {
        sizeManager(CanvasBeh.Instance.getSize());
    }
                

    private void sizeManager(Vector2 mSize)
    {
        exit.sizeDelta = new Vector2 (mSize.x*0.22f, mSize.y*0.10f);
        optinon.sizeDelta = exit.sizeDelta;
        newgame.sizeDelta = exit.sizeDelta;

        exit.anchoredPosition = new Vector2(0, mSize.y * 0.1f);
        optinon.anchoredPosition = new Vector2(0, exit.anchoredPosition.y   +mSize.y * 0.1f);
        newgame.anchoredPosition = new Vector2(0, optinon.anchoredPosition.y+mSize.y * 0.1f);

        ext.fontSize = gn.fontSize;
        opt.fontSize = gn.fontSize;
    }
}
