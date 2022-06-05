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
    public MedicalCard Master { get; private set; }
    public Dictionary<Lang, string> uiAnsw { get; set; } = new Dictionary<Lang, string>();
    public bool Writen = false;
    public bool Visible= false;

    Place current;
    float scaler = 1;

    public void Current(Place f,float s) {
        current = f;
        scaler = s;
        MakeUpdate("");
    }
    public Place GetCurrent() {
        return current;
    }
    public void Slavery(MedicalCard boss) {
        Master = boss;
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
        string wStr;
        if (!Writen&Master.QuestPosibility(current.name, out wStr))
        {
            MakeUpdate(wStr);
        }
        if (curentText.text != "")
        {
            Writen = true;
        }

    }
    public Vector2 getPlace() {
        return new Vector2(curentRect.anchoredPosition.x/scaler,
            -curentRect.anchoredPosition.y / scaler); 
    }
    public Vector2 getSize()
    {
        return curentRect.sizeDelta / scaler;
    }

    public void MakeUpdate(string wStr)
    {
        if (Writen)
        {
            gameObject.name = $"{current.name}";
        }
        curentText.text = $"{wStr}";

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
