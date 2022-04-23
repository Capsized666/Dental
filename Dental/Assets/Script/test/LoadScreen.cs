using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    bool ishide;
    LoaderBar Bar;
    public Image Background;
    
    void Awake()
    {
        Bar = GetComponentInChildren<LoaderBar>(); 
    }


    void FixedUpdate()
    {
        if (ishide)
        {
            Background.color = new Color(Background.color.r, Background.color.g, Background.color.b,
                Mathf.Lerp(Background.color.a, 0, Time.deltaTime));
        }
    }

    public void setProgress(float count,float complet) {
        if (Bar!=null)
        {
            Bar.setPersent(count/complet);
        }

    }
    public void setHide(bool b) {
        ishide = b;
       // print($"{ishide} {Time.timeScale}");
    }

}
