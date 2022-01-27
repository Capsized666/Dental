using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum pauseState
{
    game = 0,
    Pause = 1,
    Options = 2,
    ExitQ = 3,
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
        pause = false;
        tmScl = 1;
        List<Transform> buf = new List<Transform>();
        currentState = pauseState.game;
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
        switch (currentState)
        {
            case pauseState.game:
                foreach (var panel in goList)
                {
                    panel.gameObject.SetActive(false);
                }
                break;
                /*
            case pauseState.Pause:
                break;
            case pauseState.Option:
                break;
            case pauseState.ExitQ:
                break;
                 */
            default:
                foreach (var panel in goList)
                {
                    //PausePanelsBeh ppb = new PausePanelsBeh();
                    if (panel.gameObject.name == currentState.ToString())
                    {
                        panel.gameObject.SetActive(true);
                    }
                    else {
                        panel.gameObject.SetActive(false);
                    }
                }
                break;
        }

    }
    public void setState(pauseState ps) {
        currentState = ps;
    }
    public void setTimeScale(float a)
    {
        tmScl = a;
        Time.timeScale = tmScl;
        //print($"{Time.timeScale}");
    }
    public void setPause(bool p) {
        pause = p;
    }
    private void inputManager()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseState.game  == currentState |
                pauseState.Pause == currentState)
            {
                pause = pause != true ? true : false;
            }
            else
            {
                currentState = pauseState.Pause != currentState ? pauseState.Pause :currentState ;
            }
        }
        if (pause)
        {
            tmScl = 0;
            Time.timeScale = tmScl;
            currentState = pauseState.game!=currentState?currentState:pauseState.Pause;
        }
        else
        {
            tmScl = 1;
            Time.timeScale = tmScl;
            currentState = pauseState.game;
        }
    }
}



            

