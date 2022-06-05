using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MedicalCard : MonoBehaviour
{
    public List<Sprite> pages = new List<Sprite>();
    public List<AnswerMC> Field = new List<AnswerMC>();
    public TextAsset jsonFile;
    public CardPlaces topology;
    QuestPref item;

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
    private void OnDisable()
    {


    }

    private void ChangeTopology()
    {

        var canvas = CanvasBeh.Instance.getSize();
        var MainList = topology.getByName("MainList");
        var scaler = (canvas.y / MainList.y) * 0.93f;
        //var cursize = MainList * scaler;
        List<Place> top4ik = new List<Place>();

        for (int i = 0; i < TextLoby.childCount; i++)
        {
            var ans = TextLoby.GetChild(i).GetComponent<AnswerMC>();

            Place p = new Place();
            p.name  = TextLoby.GetChild(i).name;
            p.SetSize(ans.getSize());
            p.SetPoint(ans.getPlace()); 
            p.page = pagenumber;
            topology.SetModif(p);
            //top4ik.Add(p);
        }
        //for (int i = 0; i < top4ik.Count; i++)
        //{
        //    var item = topology.GetByName(top4ik[i].name);
        //    if (topology.GetByName(top4ik[i].name).name!="")
        //    {
        //        item.SetSize(top4ik[i].getSizeVector());
        //        item.SetPoint(top4ik[i].getPointVector());
        //    }
        //}
        //print($"{JsonUtility.ToJson(topology)}");
        var s = JsonUtility.ToJson(topology,true);
        var path = Path.Combine(Application.dataPath + "/Resources", $"{jsonFile.name}.json");
        //print( jsonFile.name);
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileStream fileStream = new FileStream(path,
                                        FileMode.OpenOrCreate,
                                        FileAccess.ReadWrite,
                                        FileShare.None);
            if (fileStream.CanWrite)
            {
                byte[] arr = System.Text.Encoding.Default.GetBytes(s);
                fileStream.Write(arr, 0, arr.Length);
            }
            fileStream.Close();
        }
        catch (Exception e)
        {
            print($"Pizda togo sho {e.ToString()}");
        }

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
        var scaler = (canvas.y / MainList.y) * 0.93f;
        var cursize = MainList* scaler;
        //print($"{MainList.x/MainList.y}|{canvas.x/canvas.y}");
        //print($"{canvas.y/MainList.y}|{canvas.x/ MainList.x}");
        //var partsToDraw = topology.GetByPage(pagenumber);
        //CleareContent(TextLoby,Vector2.zero);
        
        if (Field.Count!= topology.DrawObgects.Length)
        {
            var list = topology.GetAll2Draw(pagenumber);
            for (int i = 0; i < list.Length; i++)
            {
                if (i>= Field.Count)
                {
                    var a = Instantiate(prefab,TextLoby);
                    a.name = list[i].name;
                    var field = a.GetComponent<AnswerMC>();
                    Field.Add(field);
                    field.Slavery(this);
                    field.Current(list[i],scaler);
                    field.MakeUpdate("");
                }
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
        item.GetAnsvers(OrderAnswers());
        ScenaManager.Instance.currentState = gamestate.moving;
    #if UNITY_EDITOR
        //Debug.Log("Unity Editor");
        ChangeTopology();
    #endif
    }

    private string[] OrderAnswers()
    {
        List<string> answer = new List<string>();

        foreach (var item in Field)
        {
             //item.gameObject.GetComponent<>
            if (item.Writen)
            {
                //print(item.gameObject.name);
                answer.Add(item.gameObject.name); 
            }
        }
        return answer.ToArray();
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
    public void SetEvent(QuestPref i)
    {
        item = i;
    }
    public bool QuestPosibility(string name,out string wStr) {
        wStr = ExtraAdds(name);
        if (item.currEvent.status ==QuestEvent.EventStatus.CURRENT|
            item.currEvent.status == QuestEvent.EventStatus.DONE
            )
        {
            List<string> cheker = new List<string>();
            cheker.AddRange(item.currEvent.curentquest.MustOrder);
            cheker.AddRange(item.currEvent.curentquest.RightOrder);

            foreach (var item in cheker
            )
            {
                if (name==item)
                {
                    return true;
                }
            }
        }
        return false;

    }

    private string ExtraAdds(string name)
    {

        var ansv = ServiceStuff.Instance.currLangPack.GetPatientAnswers(ServiceStuff.Instance.Chose);
        var an = ansv.GetAnsverBloc(item.QuestName());
        switch (name)
        {
            case "Gender_2":
                return ansv.Sex.ToString();
                break;
            case "DateOfBirth_3":
                //var ansv = ServiceStuff.Instance.currLangPack.GetPatientAnswers(ServiceStuff.Instance.Chose);
                DateTime dateTime = DateTime.Today;
                dateTime.Subtract(new DateTime());
                 an = ansv.GetAnsverBloc(item.QuestName());
                //    item.QuestName()
                int age = 18;
                for (int i = 0; i < an.Length; i++)
                {
                    if (an[i].uiname == "DateOfBirth_3")
                    {
                        //   an[i].uiTextD[Lang.en].Split(new char[] { ' ' })[0];
                        age = int.Parse(an[i].uiTextD[Lang.en].Split(new char[] { ' ' })[0]);
                    }
                }
                dateTime = dateTime.AddYears(-age);

                dateTime = dateTime.AddMonths(-(UnityEngine.Random.Range(0, dateTime.Month)));
                dateTime = dateTime.AddDays(-(UnityEngine.Random.Range(0, dateTime.Day)));

                return dateTime.ToShortDateString().ToString();
                break;
            case "Address_4":
                string phone = " +38";//
                string[] code = new string[] { "067", "096", "097", "098", "050", "066", "095", "099 ", "063", "073", "093" };
                phone+=code[UnityEngine.Random.Range(0, code.Length)];
                phone += string.Format("{0:d7}", UnityEngine.Random.Range(0, 9999999));
                an = ansv.GetAnsverBloc(item.QuestName());
                for (int i = 0; i < an.Length; i++)
                {
                    if (an[i].uiname == name)
                    {
                        return an[i].uiTextD[ServiceStuff.Instance.getLang()] + phone;
                        //   an[i].uiTextD[Lang.en].Split(new char[] { ' ' })[0];
                        //age = int.Parse(.Split(new char[] { ' ' })[0]);
                    }
                }
                return name + phone;
                break;

            default:
                an = ansv.GetAnsverBloc(item.QuestName());
                for (int i = 0; i < an.Length; i++)
                {
                    if (an[i].uiname == name)
                    {
                        return an[i].uiTextD[ServiceStuff.Instance.getLang()];
                        //   an[i].uiTextD[Lang.en].Split(new char[] { ' ' })[0];
                        //age = int.Parse(.Split(new char[] { ' ' })[0]);
                    }
                }
                return name;
                break;
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
                item.size.Length==2&
                item.point.Length == 2
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
    public void SetModif(Place p) {
        for (int i = 0; i < DrawObgects.Length; i++)
        {
            if (p.name == DrawObgects[i].name)
            {
                DrawObgects[i].size = p.size;
                DrawObgects[i].point = p.point;
            }
        }
    }

    
    public Place GetByName(string s)
    {
        foreach (var item in DrawObgects)
        {
            if (item.name == s &
                item.size.Length == 2
                )
            {
                return item;
            }
        }

        return new Place {
            name = "",
            size = new int[0] ,
            point = new int[0],
            page = 0
        };
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

    public string   name  ;//"MainList",
    public int[]    size   ;//[1169 , 826],
    public int[]    point   ;//[0,0]
    public int      page;
    public Vector2 getSizeVector() {
        return new Vector2(size[0], size[1]);
    }
    public Vector2 getPointVector() {
        return new Vector2(point[0], point[1]);
    }

    internal void SetSize(Vector2 sizeDelta)
    {
       
        size = new int[2] {(int)(sizeDelta.x), (int)(sizeDelta.y)};
    }

    internal void SetPoint(Vector2 anchoredPosition)
    {
        point = new int[2] { (int)(anchoredPosition.x), (int)(anchoredPosition.y) };
    }
}