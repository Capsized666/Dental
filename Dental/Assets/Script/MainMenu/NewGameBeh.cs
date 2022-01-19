using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameBeh : MonoBehaviour
{
    public RectTransform rtScroll;
    public RectTransform rtLabel;
    public RectTransform rtback;
    
    
    void Update()
    {
        sizeManager(CanvasBeh.Instance.getSize());
    }


    private void sizeManager(Vector2 mSize)
    {
        rtback.sizeDelta = new Vector2(mSize.x * 0.1f, mSize.y * 0.06f);
        rtLabel.sizeDelta = new Vector2(mSize.x * 0.2f, mSize.y * 0.06f);
        rtScroll.sizeDelta = new Vector2(mSize.x * 0.8f, mSize.y * 0.75f);


        /*
        print(mSize);
         */
        rtLabel.anchoredPosition    = Vector2.zero;
        rtback.anchoredPosition     = Vector2.zero;
        rtScroll.anchoredPosition   = Vector2.zero;



    }
}
