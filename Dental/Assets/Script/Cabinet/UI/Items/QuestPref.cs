using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using UnityEngine.Events;

public enum QuestResult { 
    NONE,
    DONE,
    WELLDONE,
}

public class QuestPref : MonoBehaviour
{
    MainQuestPref Master =null;

     
    public QuestEvent currEvent { get; private set; }
    //QuestEvent.EventStatus currStatus;
    RectTransform rtCurrent;
    Dictionary<Lang, string> uiText = new Dictionary<Lang, string>();   
    Text currentText;
    Button currentBtn;
    public QuestResult qResult = QuestResult.NONE;
    
    public void Setup(QuestEvent e, Dictionary<Lang, string> txt) {
        currEvent = e;
        //currStatus = e.status;
        uiText = txt;
        if (currentText==null)
        {
            currentText = GetComponentInChildren<Text>();
        }
        currentText.text = uiText[ServiceStuff.Instance.getLang()];
        //questObgect = GameObject.Find(e.name);
    }
    public void UpdateButton(QuestEvent.EventStatus s)
    {
        currEvent.UpdateQuestEvent(s);
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
                foreach (var item in currEvent.pathlist)
                {
                    if (item.end.status == QuestEvent.EventStatus.WAITING &
                        item.end.status != QuestEvent.EventStatus.DONE
                        )
                    {
                        item.end.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
                    }
                }
                break;
            default:
                break;
        }
    }
    public void UpdateButton(QuestEvent.EventStatus s,QuestResult qrv ) 
    {
        currEvent.UpdateQuestEvent(s);
        switch (s)
        {
            case QuestEvent.EventStatus.WAITING:
                currentText.color = Color.black;
                break;
            case QuestEvent.EventStatus.CURRENT:
                currentText.color = Color.red;
                break;
            case QuestEvent.EventStatus.DONE:
                qResult = qrv;
                currentText.color = Color.green;
                foreach (var item in currEvent.pathlist)
                {
                    item.end.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
                }
                break;
            default:
                break;
        }
    }
    void Awake()
    {
        rtCurrent = GetComponent<RectTransform>();
        currentText = GetComponentInChildren<Text>();
         GetComponentInChildren<Button>().onClick.AddListener(Clic);
        //currentBtn =
    }

    void Clic()
    {
        switch (qResult)
        {
            case QuestResult.NONE:

                break;
            case QuestResult.DONE:
                currentText.text = $"{qResult.ToString()}";
                currentText.color = Color.green;
                break;
            case QuestResult.WELLDONE:
                currentText.text = $"{qResult.ToString()}";
                currentText.color = Color.green;
                break;
        }
    }
        void Update()
    {
        QuestRules();
    }

    public void Slavery(MainQuestPref m) {
        Master = m;
    }
    public string QuestName() {
        return Master.QustName();
    }
    public void GetAnsvers(string[] answ) 
    {
        var must = currEvent.curentquest.MustOrder;
        var right = currEvent.curentquest.RightOrder;
        bool[] answIndek = new bool[must.Length];
        bool[] answFull = new bool[right.Length+ must.Length];
        
        if (must.Length <= answ.Length)
        {
            for (int i = 0; i < answ.Length; i++)
            {
                if (i<must.Length )
                {
                    if (must[i] == answ[i])
                    {
                        answIndek[i] = true;
                        answFull[i] = true;
                    }

                }
                if (answ.Length >= right.Length + must.Length
                    &i< right.Length)
                {
                    if (right[i] == answ[i + must.Length])
                    {
                        answFull[i + must.Length] = true;
                    }
                }
                if (i >= must.Length&Vote(answIndek)) {
                    UpdateButton(QuestEvent.EventStatus.DONE, QuestResult.DONE);
                }
                if (i >= answ.Length-1&(answ.Length >= (right.Length + must.Length)) & Vote(answFull))
                {
                    UpdateButton(QuestEvent.EventStatus.DONE,
                    QuestResult.WELLDONE);      
                    return;
                }
            }
        }
        if (answ.Length>= (right.Length + must.Length))
        {
            qResult = QuestResult.DONE;
            UpdateButton(QuestEvent.EventStatus.DONE, QuestResult.DONE);
        }
    }

    bool Vote(bool[] voting) {
        for (int i = 0; i < voting.Length; i++)
        {
            if (!voting[i])
            {
                break;
            }
            if (i==voting.Length-1&voting[i])
            {
                return true;
            }
        }

        return false;

    }

    void QuestRules()
    {
        UpdateButton(currEvent.status);
    }


    }
