using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum menuState { 
    firstPlane=1,
    Option=2,
    newGame=3,
}

public class menuManager : MonoBehaviour
{

    menuState currentState;

    public GameObject firstplane;
    public GameObject optionplane;
    public GameObject newGameplane;

    public RectTransform BackImg;

    RectTransform fp;
    RectTransform op;
    RectTransform ntp;

    void Awake()
    {
        currentState = menuState.firstPlane;
        fp=  firstplane  .GetComponent<RectTransform>();
        op=  optionplane .GetComponent<RectTransform>();
        ntp= newGameplane.GetComponent<RectTransform>();
    }

    
    void Update()
    {
        chekstete();
    }

    private void chekstete()
    {
        BackImg.sizeDelta = CanvasBeh.Instance.getSize();
        fp.sizeDelta  = BackImg.sizeDelta;
        op.sizeDelta  = BackImg.sizeDelta;
        ntp.sizeDelta = BackImg.sizeDelta;

        BackImg.anchoredPosition = Vector2.zero;
        fp. anchoredPosition = Vector2.zero;
        op. anchoredPosition = Vector2.zero;
        ntp.anchoredPosition = Vector2.zero;


        switch (currentState)
        {
            case menuState.firstPlane:
                firstplane.SetActive(true);
                optionplane.SetActive(false);
                newGameplane.SetActive(false);
                break;
            case menuState.Option:
                firstplane.SetActive(false);
                optionplane.SetActive(true);
                newGameplane.SetActive(false);
                break;
            case menuState.newGame:
                firstplane.SetActive(false);
                optionplane.SetActive(false);
                newGameplane.SetActive(true);
                break;
        }
    }

    public void setState(menuState state) {
        currentState = state;
    }
}
