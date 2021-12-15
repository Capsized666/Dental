using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Awake()
    {
        currentState = menuState.firstPlane;
    }

    
    void Update()
    {
        chekstete();
    }

    private void chekstete()
    {
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
