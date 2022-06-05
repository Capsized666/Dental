using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainQuestPref : MonoBehaviour
{

    Quest Quest;
    Dictionary<Lang, string>    uiText      = new Dictionary<Lang, string>();
    QuestEvent                  HeadQust;
    //QuestEvent.EventStatus      HeadStatus;
    QuestEvent[]                currEvents;
    [Space]
    GameObject                  questprefab;
    List<GameObject>            questList   = new List<GameObject>();

    [Space]
    RectTransform   rtCurrent;
    Button          CurrentButton;
    Text            currentText;
    bool            expand                  =false;



    public void Setup(Quest g,GameObject quest) {
        Quest       = g;
        questprefab = quest;
        HeadQust    = g.GetHeadQuest();
        currEvents  = g.GetBodyQuest();
        uiText      = g.currentGQ.GetDetalis(g.currentGQ.QuestsName);
        currentText.text = uiText[ServiceStuff.Instance.getLang()];
        rtCurrent.sizeDelta = new Vector2(0,CanvasBeh.Instance.getSize().y*0.15f);
        UpdateButton(HeadQust.status);
        CurrentButton.onClick.AddListener(()=> {
            expand = !expand;
            if (HeadQust.status!=QuestEvent.EventStatus.DONE)
            {
            }
            if (HeadQust.status == QuestEvent.EventStatus.DONE)
            {
                //CurrentButton.interactable = false;
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

    public string QustName() {
        return HeadQust.name;
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
            var scrpt = butt.GetComponent<QuestPref>();
            scrpt.Slavery(this);
            SendEvent(scrpt);
        }
    }

    private void SendEvent(QuestPref item)
    {
        var questObgect = GameObject.Find(item.currEvent.name);
        switch (item.currEvent.name)
        {
            case "Vocal":
                var v =questObgect.GetComponent<Vocal>();
                v.SetEvent(item);
                break;
            case "MedicalCard":
                var m = questObgect.GetComponent<MedicalCard>();
                m.SetEvent(item);
                break;
            default:
                break;
        }
    }

    public void UpdateButton(QuestEvent.EventStatus s)
    {
        HeadQust.UpdateQuestEvent(s);
        switch (s)
        {
            case QuestEvent.EventStatus.WAITING:
                currentText.color = Color.black;
                break;
            case QuestEvent.EventStatus.CURRENT:
                currentText.color = Color.red;
                break;
            case QuestEvent.EventStatus.DONE:
                currentText.color = Color.green;
                foreach (var item in HeadQust.pathlist)
                {
                    if (item.end.status== QuestEvent.EventStatus.WAITING)
                    {
                        item.end.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
                    }
                }

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
        QuestRules();
    }

    private void QuestRules()
    {
        switch (HeadQust.status)
        {
            case QuestEvent.EventStatus.WAITING:
                break;
            case QuestEvent.EventStatus.CURRENT:
                if (currEvents[0].status== QuestEvent.EventStatus.WAITING)
                {
                    foreach (var item in questList)
                    {
                        if (item.GetComponent<QuestPref>().currEvent == currEvents[0])
                        {
                            item.GetComponent<QuestPref>().UpdateButton(QuestEvent.EventStatus.CURRENT);
                        }
                    }
                }
                int i = 0;
                foreach (var item in currEvents)
                {
                    if (item.status == QuestEvent.EventStatus.DONE)
                    {
                        i++;
                    }
                }
                if (i==currEvents.Length)
                {
                    UpdateButton(QuestEvent.EventStatus.DONE);
                }
                //print(currEvents[0].status);
                
                /*
                int done = 0;
                for (int i = 0; i < currEvents.Length; i++)
                {
                    if (currEvents[i].status==QuestEvent.EventStatus.DONE)
                    {
                        done++;
                    }
                }
                if (true)
                {

                }
                */
                break;
            case QuestEvent.EventStatus.DONE:
                break;
            default:
                break;
        }
    }

    private void Expand(bool e)
    {
        var ySize = CanvasBeh.Instance.getSize().y * 0.15f;
        var xSize = CanvasBeh.Instance.getSize().x * 0.90f;
        currentText.rectTransform.sizeDelta = new Vector3(0,ySize);
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
        }
    }
}



    
