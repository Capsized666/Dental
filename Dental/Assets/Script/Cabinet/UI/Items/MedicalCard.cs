using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicalCard : MonoBehaviour
{
    public List<Sprite> pages = new List<Sprite>();
    public List<Text> coloms = new List<Text>();

    public Image CurrentImage;
    private int pagenumber=0;
    bool visiable=false;

    public bool Visioble { get { return visiable; } set { visiable = value; } }
   private void Awake()
   {
        
       CurrentImage.sprite = pages[pagenumber];

   }
    private void Start()
    {
        UIEventSystem.Instance.onMedicalCardShow += Show;
        UIEventSystem.Instance.onMedicalCardHide += Hide;
    }
    public void Show() {
        visiable = true;
        ScenaManager.Instance.currentState = gamestate.notes;
    }
    public void Hide()
    {
        visiable = false;
        ScenaManager.Instance.currentState = gamestate.moving;
    }

    public void NextPage() {
        pagenumber++;
        pagenumber = pagenumber > 5 ? 5 : pagenumber;
        CurrentImage.sprite = pages[pagenumber];
    }
    public void PrevPage() {
        pagenumber--;
        pagenumber = pagenumber < 0 ? 0 : pagenumber;
        CurrentImage.sprite = pages[pagenumber];
    }

    // Update is called once per frame
    void Update()
    {
        if (visiable)
        {
            CurrentImage.rectTransform.anchoredPosition =new Vector2(Mathf.Lerp(CurrentImage.rectTransform.anchoredPosition.x,0,Time.deltaTime*3),0);
        }
        else {
            CurrentImage.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(CurrentImage.rectTransform.anchoredPosition.x, CanvasBeh.Instance.getSize().x, Time.deltaTime * 3), 0);
        }
    }
        
}

[Serializable]
public struct CardPlaces
{
    public Place[] DrawObgects;

}
[Serializable]
public struct Place
{
    public string name  ;//"MainList",
    public int[] size   ;//[1169 , 826],
    public int[] poin   ;//[0,0]
    public int page;

}