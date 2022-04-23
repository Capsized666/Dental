using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public gamestate currentState { get; set; } = gamestate.moving;

    private void Awake()
    {
        if (Instance!=this)
        {
            Instance = this;

        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Managing();
    }

    private void Managing()
    {
        switch (currentState)
        {
            case gamestate.moving:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case gamestate.asking:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                break;
            case gamestate.notes:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                break;
            default:
                break;
        }

    }
}
