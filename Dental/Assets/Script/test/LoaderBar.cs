using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderBar : MonoBehaviour
{
    Canvas pCanvas;
    bool isHide;
    bool complet;
    public Image    backLoad;
    public Image    loadcolor;
    public Text     LoadText;

    // Start is called before the first frame update
    void Start()
    {
        isHide = false;
        complet = false;
        pCanvas = GetComponentInParent<Canvas>();
        //print(
        //pCanvas.pixelRect.width);
    }


    public void setPersent(float a) {
        loadcolor.fillAmount = a;
        int s = (int)a * 100;
        switch (s % 4)
        {
            case 0 :
                LoadText.text = "Loading";
                break;
            case 1:
                LoadText.text = "loAding";
                break;
            case 2:
                LoadText.text = "loadIng";
                break;
            case 3:
                LoadText.text = "loadinG";
                break;
            default:
                break;
        }
        if (a==1)
        {
            complet = true;
        }
    }
    
    void FixedUpdate()
    {
        if (pCanvas.pixelRect.width*0.25f!= backLoad.rectTransform.sizeDelta.x)
        {
            reCalculate();
        }
        hideOperation();
    }
        

    private void hideOperation()
    {
        if (complet)
        {
            backLoad.color = new Color(backLoad.color.r, backLoad.color.g, backLoad.color.b,
                Mathf.Lerp(backLoad.color.a, 0, Time.deltaTime*5));
            loadcolor.color = new Color(loadcolor.color.r, loadcolor.color.g, loadcolor.color.b,
                Mathf.Lerp(backLoad.color.a, 0, Time.deltaTime * 7));
            LoadText.color = new Color(LoadText.color.r, LoadText.color.g, LoadText.color.b,
                Mathf.Lerp(backLoad.color.a, 0, Time.deltaTime * 9));
        }

        else
        {

            backLoad.color = new Color(backLoad.color.r, backLoad.color.g, backLoad.color.b,
                Mathf.Lerp(backLoad.color.a, 255, Time.deltaTime));
            loadcolor.color = new Color(loadcolor.color.r, loadcolor.color.g, loadcolor.color.b,
                Mathf.Lerp(backLoad.color.a, 255, Time.deltaTime));
            LoadText.color = new Color(LoadText.color.r, LoadText.color.g, LoadText.color.b,
                Mathf.Lerp(backLoad.color.a, 255, Time.deltaTime));
        }
    }

    private void reCalculate()
    {
        backLoad.rectTransform.sizeDelta = new Vector2(pCanvas.pixelRect.width * 0.25f
                                                    ,pCanvas.pixelRect.height * 0.05f);
        loadcolor.rectTransform.sizeDelta = backLoad.rectTransform.sizeDelta;
        LoadText.rectTransform.sizeDelta = backLoad.rectTransform.sizeDelta;
        
        backLoad.rectTransform.anchoredPosition = new Vector2(0,0);
        loadcolor.rectTransform.anchoredPosition= new Vector2(0,0);
        LoadText.rectTransform.anchoredPosition = new Vector2(0,0);
    }
}
