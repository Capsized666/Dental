using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StateOfLoadScreen { 
    Loading = 0,
    Moving = 1,
    Look = 2,
    }

public enum StateOfMoving
{
    Image = 0,
    Movi = 1,
}

public class UniversalScreenBeh : MonoBehaviour
{
    public StateOfLoadScreen currState;
    public StateOfMoving currMoviState;

    public Image curentImage;

    public Image    Loadimg;
    public Text     LoadText;
    public CloseImage    Close;

    public VideoPlayerBeh curentPlayer;

    // [SerializeField]
    public bool ImageVisible;



    private void Awake()
    {
        currState = StateOfLoadScreen.Loading;
        currMoviState = StateOfMoving.Image;
    }
    void Start()
    {
        Close = GetComponentInChildren<CloseImage>();
        Close.SetManager(this);
    }
    void Update()
    {
        setLastChild();
        
        switch (currState)
        {
            case StateOfLoadScreen.Loading:
                LoadingState();
                break;
            case StateOfLoadScreen.Moving:
                MovingState();
                break;
            case StateOfLoadScreen.Look:
                LookState();
            break;
        }
    }

    private void LookState()
    {
        var alpha = curentImage.color.a >= 0 ? curentImage.color.a - Time.deltaTime : 0;
        curentImage.color = new Color
            (curentImage.color.r, curentImage.color.g, curentImage.color.b, alpha/255);
        Loadimg.color = new Color
            (Loadimg.color.r, Loadimg.color.g, Loadimg.color.b, alpha);
        LoadText.color = new Color
            (LoadText.color.r, LoadText.color.g, LoadText.color.b, alpha);
        //print(" fgrg ");
    }
    
    private void MovingState()
    {
        switch (currMoviState)
        {
            case StateOfMoving.Image:

                break;
            case StateOfMoving.Movi:
                
                break;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            closeWindow();
        }



        var alpha = curentImage.color.a >= 255 ? 255 : curentImage.color.a + Time.deltaTime;

        curentImage.color = new Color
            (curentImage.color.r, curentImage.color.g, curentImage.color.b, alpha);
    }
    private void LoadingState()
    {
        var alpha = curentImage.color.a >= 255 ? 255 : curentImage.color.a + Time.deltaTime;
        curentImage.color = new Color
            (curentImage.color.r, curentImage.color.g, curentImage.color.b, alpha);
        Loadimg.color = new Color
            (Loadimg.color.r, Loadimg.color.g, Loadimg.color.b, alpha);
        LoadText.color = new Color
            (LoadText.color.r, LoadText.color.g, LoadText.color.b, alpha);
    }
     
    private void setLastChild()
    {
        if (currState != StateOfLoadScreen.Look)
        {
            transform.SetSiblingIndex(MCUI.Instance.transform.childCount - 1);
        }
        else {
            transform.SetSiblingIndex(0);
        }
        

    }

    public void SetColor(Color c) {
        var alpha = ImageVisible ? 255 : 0;
        curentImage.color = new Color (c.r,c.g,c.b,alpha);
    }

    public void SetSprate(Sprite s)
    {
        curentImage.sprite = s;
    }

    public void SetState(StateOfLoadScreen s) {

        currState = s;
        switch (currState)
        {
            case StateOfLoadScreen.Loading:
                curentImage.color = new Color
                    (curentImage.color.r, curentImage.color.g, curentImage.color.b, 255);
                Loadimg.color = new Color
                    (Loadimg.color.r, Loadimg.color.g, Loadimg.color.b, 255);
                LoadText.color = new Color
                    (LoadText.color.r, LoadText.color.g, LoadText.color.b, 255);
                break;
        }


    }

    public void SetMovingState(StateOfMoving s) {
        currMoviState = s;
    }

    public void closeWindow() {
        curentImage.sprite = null;
        currState = StateOfLoadScreen.Look;
        curentPlayer.StopVideo();
    }


}
  