using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum pauseState
{
    game = 0,
    pause = 1,
    option = 2,
    exitQ = 3,
}
public class PauseBeh : MonoBehaviour
{
    pauseState currentState;

    bool pause;
    float tmScl;

    Transform[] goList;
    //list of bihaviors

    private void Awake()
    {
        pause = true;
        tmScl = 1;
        List<Transform> buf = new List<Transform>();

        var a = GetComponent<Transform>();
        
        for (int i = 0; i < a.childCount; i++)
        {
            buf.Add(a.GetChild(i));
        }
        goList = buf.ToArray();
        //gorct.gameObject.SetActive(!pause);

    }
    void Start()
    {
        
    }

    private void OnGUI()
    {
    }

    // Update is called once per frame
    void Update()
    {
        inputManager();
        sizeManager(CanvasBeh.Instance.getSize());
    }

    private void sizeManager(Vector2 vector2)
    {
        /*
        if (pause)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
         */
    }

    public void setTimeScale(float a)
    {
        tmScl = a;
    }
    private void inputManager()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = pause != true ? true : false;
        }

        if (pause)
        {
            tmScl = 0;
            Time.timeScale = tmScl;

        }
        else
        {
            tmScl = 1;
            Time.timeScale = tmScl;

        }
            

    }
}
