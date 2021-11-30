using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderBar : MonoBehaviour
{
    Canvas pCanvas;
    bool isHide;
    public Image backLoad;
    public Image loadcolor;
    public Text LoadText;

    // Start is called before the first frame update
    void Start()
    {
        isHide = false;
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

    }
    
    void LateUpdate()
    {
        if (pCanvas.pixelRect.width*0.25f!= backLoad.rectTransform.sizeDelta.x)
        {
            reCalculate();
        }
        hideOperation();
        
    }

    private void hideOperation()
    {
        if (isHide)
        {

        }
        else
        {

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
