using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vocal : MonoBehaviour
{
    public Image CurrentImage;
    public RectTransform currentScroll;
    public RectTransform AnsverScroll;
    public Scrollbar AnsverScrollBar;

    public RectTransform RectContent;
    public RectTransform DialogContent;

    public GameObject textPrfab;
    public GameObject answerTextPrfab;
    public Text AnsverField;
    
    [SerializeField]
    public List<Dictionary<Lang, string>> PrintAnsvers 
        = new List<Dictionary<Lang, string>>();
    public  List<int> OrderNumber =
    new     List<int>();
    public List <string> OrderAnswers =
        new List<string>();
    bool ansverVisible= false;
    bool visiable = false;
    void Awake()
    {
        CurrentImage = GetComponent<Image>();
        //currentScroll = GetComponent<ScrollRect>();
        CleareContent(RectContent,Vector2.zero);
        CleareContent(DialogContent, Vector2.zero);
        AnsverScroll.gameObject.SetActive(ansverVisible);
    }
    private void OnDisable()
    {
        UIEventSystem.Instance.onAskingBarShow -= Show;
        UIEventSystem.Instance.onAskingBarHide -= Hide;
    }
    void Start()
    {
        UIEventSystem.Instance.onAskingBarShow += Show;
        UIEventSystem.Instance.onAskingBarHide += Hide;
        setSize();
        UpdateText();
        FillField();
    }
    public void Show()
    {
        visiable = true;
        ScenaManager.Instance.currentState = gamestate.asking;
        CleareContent(RectContent, Vector2.zero);
        CleareContent(DialogContent, Vector2.zero);
        setSize(); 
        UpdateText();
        FillField();
        UpdateText();
        UpdateAnswer();
    }
    public void Hide()
    {

        visiable = false;
        item.GetAnsvers(OrderAnswers.ToArray());
        ScenaManager.Instance.currentState = gamestate.moving;

    }
    public void FillField()
    {
        var quest = QuestManager.Instance.getAsking();
        if (ServiceStuff.Instance != null&
            quest!=null)
        {
            CleareContent(RectContent, Vector2.zero);
            var answ = ServiceStuff.
                    Instance.
                    currLangPack.
                    GetPatientAnswers(ServiceStuff.Instance.Chose);
            questionText qves = ServiceStuff.
                    Instance.
                    currLangPack.
                    GetQuestionBlock(quest.QuestsName);
            var answB = answ.GetAnsverBloc(quest.QuestsName);
            for (int i = 0; i < qves.ServiceText.Length; i++)
            {
                if (IsEnable2Print(answB[i].uiname))
                {
                    var repl = Instantiate(textPrfab, RectContent);
                    var script = repl.GetComponent<QuestionPref>();
                    
                    script.SetCurator(this);
                    script.SetOrderNumber(i);
                    script.SetAnswer(answB[i].uiname);
                    script.uiQuest = qves.ServiceText[i].uiTextD;
                    script.uiAnsw = answB[i].uiTextD;//.ServiceText[i].uiTextD;
                    RectContent.sizeDelta += new Vector2(
                        0,
                        textPrfab.GetComponent<RectTransform>().sizeDelta.y);
                    script.RefreshData(ServiceStuff.Instance.getLang());
                }
            }
        }
    }
    private void setSize()
    {
        CurrentImage.rectTransform.sizeDelta = new Vector2(CanvasBeh.Instance.getSize().x*0.93f, CanvasBeh.Instance.getSize().y*0.39f);
        CurrentImage.rectTransform.anchoredPosition = new Vector2(CanvasBeh.Instance.getSize().x, -CanvasBeh.Instance.getSize().y*0.25f);

        currentScroll.sizeDelta = new Vector2(CurrentImage.rectTransform.sizeDelta.x, CurrentImage.rectTransform.sizeDelta.y * 0.75f);
        currentScroll.anchoredPosition = new Vector2(0, 0);
    
        AnsverScroll.anchoredPosition = new Vector2(0, 0);
        var sol = CanvasBeh.Instance.getSize() * 0.93f;
        sol = new Vector2(
            0,
            sol.y- CurrentImage.rectTransform.sizeDelta.y
            );
        AnsverScroll.sizeDelta = sol;
    }
  
    private void CleareContent(Transform t,Vector2 startSize)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            Destroy(t.transform.GetChild(i).gameObject); 
        }
        t.GetComponent<RectTransform>().sizeDelta = startSize;
        
    }

    private bool IsEnable2Print(string chosen) {
        
        if (OrderAnswers.Count>0)
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

    public void Ansvering(int i,string oa, Dictionary<Lang, string>q, Dictionary<Lang, string>a) {
        OrderNumber.Add(i);
        OrderAnswers.Add(oa);
        PrintAnsvers.Add(q);
        PrintAnsvers.Add(a);
        UpdateText();
        FillField();
        UpdateText();
        UpdateAnswer();

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
    public void UpdateText() {
        if (OrderAnswers.Count>0)
        {
            var quest = QuestManager.Instance.getAsking();
            var answ = ServiceStuff.
                            Instance.
                            currLangPack.
                            GetPatientAnswers(ServiceStuff.Instance.Chose);
            var answB = answ.GetAnsverBloc(quest.QuestsName);
            
            AnsverField.text = answB[OrderNumber[OrderNumber.Count - 1]].uiTextD
                [ServiceStuff.Instance.getLang()];
        }
        if (OrderAnswers.Count == 0 | OrderAnswers == null)
        {

            AnsverField.text = "";}
    }
    public void AnsverOnClick()
    {
        ansverVisible = !ansverVisible;
        UpdateAnswer();
    }

    public void UpdateAnswer() { 
     
        AnsverScroll.gameObject.SetActive(ansverVisible);
        if (ansverVisible)
        {

            CleareContent(DialogContent, Vector2.zero);
            if (PrintAnsvers!=null & PrintAnsvers.Count>0)
            {
                for (int i = 0; i < PrintAnsvers.Count-1; i++)
                {
                    var repl = Instantiate(answerTextPrfab, DialogContent);
                    Text replText = repl.GetComponent<Text>();
                    repl.name = PrintAnsvers[i][Lang.en];
                    replText.text = PrintAnsvers[i][ServiceStuff.Instance.getLang()];
                    replText.rectTransform.sizeDelta = new Vector2(
                       textPrfab.GetComponent<RectTransform>().sizeDelta.x,
                       textPrfab.GetComponent<RectTransform>().sizeDelta.y);
                    DialogContent.sizeDelta += new Vector2(
                       0,
                       textPrfab.GetComponent<RectTransform>().sizeDelta.y);
                }
            }
            AnsverScrollBar.value=0;
        }
      
    
    }

    QuestPref item;
    public void SetEvent(QuestPref i)
    {
        item = i;
    }
}



