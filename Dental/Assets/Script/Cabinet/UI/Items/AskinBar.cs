using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AskinBar : MonoBehaviour
{

    public Image CurrentImage;
    public RectTransform currentScroll;
    public RectTransform rectContent;
    public GameObject textPrfab;
    
    [SerializeField]
    public List<Dictionary<Lang, string>> PrintAnsvers 
        = new List<Dictionary<Lang, string>>();
    public List<int> OrderAnswers =
        new List<int>();

    bool visiable = false;
    void Awake()
    {
        CurrentImage = GetComponent<Image>();
        //currentScroll = GetComponent<ScrollRect>();
        setSize();
        
    }

    public void Ansvering(int oa, Dictionary<Lang, string>q, Dictionary<Lang, string>a) {
        OrderAnswers.Add(oa);
        PrintAnsvers.Add(q);
        PrintAnsvers.Add(a);
    }
    private void setSize()
    {
        CurrentImage.rectTransform.sizeDelta = new Vector2(CanvasBeh.Instance.getSize().x*0.93f, CanvasBeh.Instance.getSize().y*0.39f);
        CurrentImage.rectTransform.anchoredPosition = new Vector2(CanvasBeh.Instance.getSize().x, -CanvasBeh.Instance.getSize().y*0.25f);

        currentScroll.sizeDelta = new Vector2(CurrentImage.rectTransform.sizeDelta.x, CurrentImage.rectTransform.sizeDelta.y * 0.75f);
        currentScroll.anchoredPosition = new Vector2(0, 0);
    }

    void Start()
    {
        UIEventSystem.Instance.onAskingBarShow += Show;
        UIEventSystem.Instance.onAskingBarHide += Hide;
        FillField();
    }

    public void FillField()
    {
        var quest = QuestManager.Instance.quest.getCurent();
        if (ServiceStuff.Instance != null)
        {
            CleareContent();
            var answ = ServiceStuff.
                    Instance.
                    currLangPack.
                    GetPatientAnswers(ServiceStuff.Instance.Chose);
            questionText qves = ServiceStuff.
                    Instance.
                    currLangPack.
                    GetQuestionBlock(quest.name);
            var answB = answ.GetAnsverBloc(quest.name);
            for (int i = 0,c=0; i < qves.ServiceText.Length; c++, i++)
            {
                if (IsEnable2Print(i))
                {
                    var repl = Instantiate(textPrfab, rectContent);
                    var script = repl.GetComponent<QuestionPref>();
                    script.SetCurator(this);
                    script.SetOrderNumber(i);
                    script.uiQuest = qves.ServiceText[i].uiTextD;
                    script.uiAnsw = answB[i].uiTextD;//.ServiceText[i].uiTextD;
                    rectContent.sizeDelta += new Vector2(
                        0,
                        textPrfab.GetComponent<RectTransform>().sizeDelta.y);
                    script.RefreshData(ServiceStuff.Instance.getLang());
                }
            }
        }
    }

    private void CleareContent()
    {
        for (int i = 0; i < rectContent.childCount; i++)
        {
            Destroy(rectContent.transform.GetChild(i).gameObject); 
        }
        rectContent.sizeDelta = Vector2.zero;
    }

    private bool IsEnable2Print(int chosen) {
        if (OrderAnswers.Count>1)
        {
            for (int i = 0; i < OrderAnswers.Count; i++)
            {
                if (OrderAnswers[i]==chosen)
                {
                    return false;
                }
            }
        }
        return true;
    }


    private void OnDisable()
    {
        UIEventSystem.Instance.onAskingBarShow -= Show;
        UIEventSystem.Instance.onAskingBarHide -= Hide;
    }

    public void Show()
    {
        visiable = true;
        ScenaManager.Instance.currentState = gamestate.asking;
    }
    public void Hide()
    {
        visiable = false;
        ScenaManager.Instance.currentState = gamestate.moving;
    }


    
    public void CreateQuestion()
    {
        var quest = ServiceStuff.Instance.currLangPack.DQuestion;
        var answ = ServiceStuff.Instance.currLangPack.GetPatientAnswers(ServiceStuff.Instance.Chose);
    }
    void Update()
    {
        if (visiable)
        {
            CurrentImage.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(CurrentImage.rectTransform.anchoredPosition.x, 0, Time.deltaTime * 6), CurrentImage.rectTransform.anchoredPosition.y);
        }
        else
        {
            CurrentImage.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(CurrentImage.rectTransform.anchoredPosition.x, CanvasBeh.Instance.getSize().x, Time.deltaTime * 6), CurrentImage.rectTransform.anchoredPosition.y);
        }
    }
    public void chek() {
        print("dds");
    }
    public void chek(Vector2 v)
    {
        print($"dds {v} {v.x}:{v.y}");
    }
}
