using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerMC : MonoBehaviour
{
    public RectTransform curentRect;
    public Button   curentButton;
    public Text     curentText;

    public Dictionary<Lang, string> uiAnsw { get; set; } = new Dictionary<Lang, string>();
    public bool Writen = false;
    public bool Visible= false;

    Place current;
    float scaler = 1;

    public void Current(Place f,float s) {
        current = f;
        scaler = s;
        MakeUpdate();
    }
    public Place GetCurrent() {
        return current;
    }

    void Awake()
    {
        curentRect = GetComponent<RectTransform>();
        curentButton =GetComponent<Button>() ;
        curentText  =GetComponent<Text>();
        curentText.text = "";
        curentButton.onClick.AddListener(Clic);

    }

    void Clic() {
        Writen = true;
        MakeUpdate();
    }

    public void MakeUpdate()
    {
        if (Writen)
        {
            gameObject.name = current.name;
            curentText.text = current.name;

        }
        curentRect.sizeDelta = current.getSizeVector()* scaler;
        curentRect.anchoredPosition = new Vector2(current.getPointVector().x,
               -current.getPointVector().y) * scaler;
    }

    private void Update()
    {
    }
    public void SetVisible(bool t)
    {
        gameObject.SetActive(t);
    }
}
