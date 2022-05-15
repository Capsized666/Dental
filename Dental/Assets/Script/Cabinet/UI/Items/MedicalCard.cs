using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicalCard : MonoBehaviour
{
    public List<Sprite> pages = new List<Sprite>();
    public List<AnswerMC> Field = new List<AnswerMC>();
    public TextAsset jsonFile;
    public CardPlaces topology;

    public RectTransform TextLoby;
    public GameObject prefab;
    public Image CurrentImage;
    private int pagenumber=0;
    bool visiable=false;
    public bool Visioble { get { return visiable; } set { visiable = value; } }
   private void Awake()
   {
        CurrentImage.sprite = pages[pagenumber];
        topology= JsonUtility.FromJson<CardPlaces>(jsonFile.text);
     
   }
    private void CleareContent(Transform t, Vector2 startSize)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            Destroy(t.transform.GetChild(i).gameObject);
        }
        t.GetComponent<RectTransform>().sizeDelta = startSize;

    }



    private void SetSize()
    {
        var canvas =  CanvasBeh.Instance.getSize();
        var MainList =topology.getByName("MainList");
        //print($"{MainList.x/MainList.y}|{canvas.x/canvas.y}");
        //print($"{canvas.y/MainList.y}|{canvas.x/ MainList.x}");
        var scaler = (canvas.y / MainList.y) * 0.93f;
        var cursize = MainList* scaler;
        var partsToDraw = topology.GetByPage(pagenumber);
        //CleareContent(TextLoby,Vector2.zero);
        if (Field.Count==0)
        {
            var list = topology.GetAll2Draw(pagenumber);
            for (int i = 0; i < list.Length; i++)
            {
                    var a = Instantiate(prefab,TextLoby);
                    var field = a.GetComponent<AnswerMC>();
                    Field.Add(field);
                    field.Current(list[i],scaler);
                    field.MakeUpdate();
            }
        }
        for (int i = 0; i < Field.Count; i++)
        {
            if (Field[i].GetCurrent().page == pagenumber)
            {
                Field[i].SetVisible(true);
            }
            else
            {
                Field[i].SetVisible(false);
            }
        }
        





        CurrentImage.rectTransform.sizeDelta =
            cursize;
    }

    private bool Existence(Place place)
    {
        if (Field.Count>0)
        {
            for (int i = 0; i < Field.Count; i++)
            {
                if (Field[i].GetCurrent().Equals(place))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void Start()
    {
        SetSize();          
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
        SetSize();
    }
    public void PrevPage() {
        pagenumber--;
        pagenumber = pagenumber < 0 ? 0 : pagenumber;
        CurrentImage.sprite = pages[pagenumber];
        SetSize();
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
    public Vector2 getByName(string s) {
        foreach (var item in DrawObgects)
        {
            if (item.name==s&
                item.size.Length==2
                )
            {
                return new Vector2(
                    item.size[0],
                    item.size[1]
                    );
            }
        }
        
        return Vector2.zero;
    }
    public Place[] GetByPage(int p) {
        var res = new List<Place>();
        foreach (var item in DrawObgects)
        {
            if (
                item.name!= "MainList" &
                item.page==p
                )
            {
                res.Add(item);
            }
        }
        return res.ToArray();
    }
    public Place[] GetAll2Draw(int p)
    {
        var res = new List<Place>();
        foreach (var item in DrawObgects)
        {
            if (
                item.name != "MainList" 
                )
            {
                res.Add(item);
            }
        }
        return res.ToArray();
    }

}
[Serializable]
public struct Place
{
    public string name  ;//"MainList",
    public int[] size   ;//[1169 , 826],
    public int[] point   ;//[0,0]
    public int page;
    public Vector2 getSizeVector() {
        return new Vector2(size[0], size[1]);
    }
    public Vector2 getPointVector() {
        return new Vector2(point[0], point[1]);
    }


}