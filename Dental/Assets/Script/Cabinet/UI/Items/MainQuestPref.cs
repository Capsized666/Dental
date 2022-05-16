using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainQuestPref : MonoBehaviour
{

    Quest Quest;
    Dictionary<Lang, string> uiText = new Dictionary<Lang, string>();
    QuestEvent              HeadQust;
    QuestEvent.EventStatus  HeadStatus;
    QuestEvent[]            currEvents;
    [Space]
    GameObject questprefab;
    List<GameObject> questList = new List<GameObject>();

    [Space]
    RectTransform rtCurrent;
    Button CurrentButton;
    Text currentText;
    bool expand=false;



    public void Setup(Quest g,GameObject quest) {
        Quest       = g;
        questprefab = quest;
        HeadQust    = g.GetHeadQuest();
        currEvents   = g.GetBodyQuest();
        uiText      = g.currentGQ.GetDetalis(g.currentGQ.QuestsName);
        currentText.text = uiText[ServiceStuff.Instance.getLang()];
        rtCurrent.sizeDelta = new Vector2(0,CanvasBeh.Instance.getSize().y*0.15f);
        UpdateButton(HeadQust.status);
        CurrentButton.onClick.AddListener(()=> {
            if (HeadStatus!=QuestEvent.EventStatus.DONE)
            {
                expand = !expand;
            }
            if (HeadStatus == QuestEvent.EventStatus.DONE)
            {
                CurrentButton.interactable = false;
            }
        });
        RollOver();
    }
    GameObject CrtQButton(QuestEvent e)
    {
        GameObject b = Instantiate(questprefab);
        var scrpt = b.GetComponent<QuestPref>();

        var f = Quest.currentGQ.GetDetalis(e.name);
        scrpt.Setup(e, f);

        return b;
    }
    private void RollOver()
    {
        foreach (var item in currEvents)
        {
            var butt = CrtQButton(item);
            butt.transform.SetParent(rtCurrent);
            butt.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 150);
            rtCurrent.sizeDelta += new Vector2(0, 150);
            questList.Add(butt);
        }
       
    }    
    public void UpdateButton(QuestEvent.EventStatus s)
    {
        HeadStatus = s;
        switch (s)
        {
            case QuestEvent.EventStatus.WAITING:
                currentText.color = Color.black;
                break;
            case QuestEvent.EventStatus.CURRENT:
                currentText.color = Color.green;
                break;
            case QuestEvent.EventStatus.DONE:
                currentText.color = Color.yellow;
                break;
            default:
                break;
        }
    }
    void Awake()
    {
        rtCurrent       = GetComponent<RectTransform>();
        currentText     = GetComponentInChildren<Text>();
        CurrentButton   = GetComponent<Button>();
    }
        

    
    void Update()
    {
        Expand(expand);
    }
        
         

        
        

                //print("wqw");
                //        print("wqw");
                //        print("wqw+");
            //if (rtCurrent.sizeDelta.y * 1.1f == ySize)
            //{
            //}
            //if (rtCurrent.sizeDelta.y==ySize)
            //{
            //}
            //print("wqw-");
            //    print("wqw-");
    private void Expand(bool e)
    {
        var ySize = CanvasBeh.Instance.getSize().y * 0.15f;
        var xSize = CanvasBeh.Instance.getSize().x * 0.90f;

        if (e)
        {
            rtCurrent.sizeDelta = new Vector2(0,
               Mathf.Lerp(rtCurrent.sizeDelta.y, (ySize * (currEvents.Length + 1)), Time.deltaTime * 6)
                );

            for (int i = questList.Count; i >0; i--)
            {
                var rt = questList[i-1].GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(
                    Mathf.Lerp(rt.sizeDelta.x, xSize, Time.deltaTime * 6),
                    Mathf.Lerp(rt.sizeDelta.y, ySize, Time.deltaTime * 6)
                    );
                rt.anchoredPosition = new Vector2(
                    Mathf.Lerp(rt.anchoredPosition.x, 0, Time.deltaTime * 6),
                    Mathf.Lerp(rt.anchoredPosition.y, (ySize+20)*Mathf.Abs(questList.Count - i), Time.deltaTime * 6)
                    );
            }
                //var t = rtCurrent.childCount;
                //for (int i = t-1; i > 0; i--)
                //{
                //    var questTransf = rtCurrent.transform.GetChild(i);
                //    QuestPref q = GetComponent<QuestPref>();
                //    if (q !=null)
                //    {
                //        var rt = questTransf.GetComponent<RectTransform>();
                //}
                //}
        }
        else {
            rtCurrent.sizeDelta = new Vector2(0,
               Mathf.Lerp(rtCurrent.sizeDelta.y, ySize, Time.deltaTime*6));
            for (int i = questList.Count; i > 0; i--)
            {
                var rt = questList[i - 1].GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(
                    Mathf.Lerp(rt.sizeDelta.x, 0, Time.deltaTime * 6),
                    Mathf.Lerp(rt.sizeDelta.y, 0, Time.deltaTime * 6)
                    );
                rt.anchoredPosition = new Vector2(
                    Mathf.Lerp(rt.anchoredPosition.x, 0, Time.deltaTime * 6),
                    Mathf.Lerp(rt.anchoredPosition.y, 0, Time.deltaTime * 6)
                    );
            }
            //var t = rtCurrent.childCount;
            //for (int i = 0; i < t; i++)
            //{
            //    var questTransf = rtCurrent.transform.GetChild(i);
            //    QuestPref q = new QuestPref();
            //    if (questTransf.TryGetComponent<QuestPref>(out q))
            //    {
            //        var rt = questTransf.GetComponent<RectTransform>();
            //        rt.sizeDelta = new Vector2(
            //            Mathf.Lerp(rt.sizeDelta.x, 0, Time.deltaTime * 6),
            //            Mathf.Lerp(rt.sizeDelta.y, 0, Time.deltaTime * 6)
            //            );
            //        rt.anchoredPosition = new Vector2(
            //            Mathf.Lerp(rt.sizeDelta.x, 0, Time.deltaTime * 6),
            //            Mathf.Lerp(rt.sizeDelta.y, 0, Time.deltaTime * 6)
            //            );
            //    }
            //}
        }
    }
}



    
