using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    public static QuestManager Instance;
    [SerializeField]
    public List<Quest> quests = new List<Quest>();
    List<MainQuestPref> questList = new List<MainQuestPref>();

    [Space]
    public RectTransform mainPlane;
    public GameObject prefabHead;

    public GameObject prefabregul;
    public RectTransform Content;
    public RectTransform Result;
   

    GameQuests gameflow;

    public GameQuests GameFlow { get { return gameflow; } }


    bool visiable   = false;
    bool result = false;
    private void OnDisable()
    {
        UIEventSystem.Instance.onQuestBarShow -= Show;
        UIEventSystem.Instance.onQuestBarHide -= Hide;
        UIEventSystem.Instance.onResultShow -= ResultShow;
        UIEventSystem.Instance.onResultHide -= ResultHide;


    }
    public QuestResult Grade()
    {
        var gl = QuestResult.WELLDONE;
        foreach (var item in questList)
        {
            if (item.Grade() < gl)
            {
                gl = item.Grade();
            }
        }
        return gl;
    }
    private void Awake()
    {
        if (Instance != this)
        {
            Instance = this;
        };
        
        mainPlane = GetComponent<RectTransform>();
        Result.gameObject.SetActive(false);
    }
    void Start()
    {
        CreateQuect();
        CreateUI();
        UIEventSystem.Instance.onQuestBarShow += Show;
        UIEventSystem.Instance.onQuestBarHide += Hide;
        UIEventSystem.Instance.onResultShow += ResultShow;
        UIEventSystem.Instance.onResultHide += ResultHide;
    }
    void Update()
    {
        if (visiable)
        {
            mainPlane.anchoredPosition =
                new Vector2(Mathf.Lerp(mainPlane.anchoredPosition.x, 0, Time.deltaTime * 6),
               0);
        }
        else
        {
            mainPlane.anchoredPosition = new Vector2(
                Mathf.Lerp(mainPlane.anchoredPosition.x, CanvasBeh.Instance.getSize().x, Time.deltaTime * 6),
                0);
        }
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

    public void ResultShow() {
        Result.gameObject.SetActive(true);
        result = true;
        ScenaManager.Instance.currentState = gamestate.asking;
    }

    public void ResultHide() {
        result = false;
        ScenaManager.Instance.currentState = gamestate.moving;
    }

    private void CleareContent(Transform t, Vector2 startSize)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            Destroy(t.transform.GetChild(i).gameObject);
        }
        t.GetComponent<RectTransform>().sizeDelta = startSize;
    }

    private void CreateQuect()
    {
        foreach (var item in ServiceStuff.Instance.currLangPack.GQuests)
        {
            if (item.QuestsName == "Game")
            {
                gameflow = item;
            }
            
        }
        for (int i = 0; i < gameflow.PlayZone[0].MustOrder.Length; i++)
        {
           
            foreach (var item in ServiceStuff.Instance.currLangPack.GQuests)
            {
               
                if (item.QuestsName == gameflow.PlayZone[0].MustOrder[i]
                    )
                {
                    var quest = new Quest();
                    quests.Add(quest.AddQuestEvents(item));
                }
            }
        }
        for (int i = 0; i < quests.Count-1; i++)
        {
            quests[i].AddPath(quests[i].lastEvent.curentquest,
                quests[i + 1].HeadEvent.curentquest);
        }
        quests[0].BFS(quests[0].questEvents[0].id);
        quests[0].questEvents[0].UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
    }
    private void CreateUI()
    {
        CleareContent(Content, Vector2.zero);
        var CanSize = CanvasBeh.Instance.getSize();
        mainPlane.sizeDelta = CanSize * 0.89f;
        for (int i = 0; i < quests.Count; i++)
        {
            //for (int j = 0; j < quests[i].questEvents.Count; j++)
            //{
                var butt = CrtQButton(quests[i]);
                butt.transform.SetParent(Content);
                butt.GetComponent<RectTransform>().sizeDelta=new Vector2(0,150);
                Content.sizeDelta+= new Vector2(0, 150);
            //}
        }
    }

    GameObject CrtQButton(Quest quest)
    {
        GameObject b = Instantiate(prefabHead);
        var scrpt = b.GetComponent<MainQuestPref>();
        //var f = getAsking().GetDetalis(e.name);
        scrpt.Setup(quest,prefabregul);
        questList.Add(scrpt);
        return b;
    }

    public GameQuests getAsking()
    {
        foreach (var item in quests)
        {
            if (item.GetCurentQuest ()!=null)
            {
                return item.currentGQ;
            }
        }
        return null;
    }

}
