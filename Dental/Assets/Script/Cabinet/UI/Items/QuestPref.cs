using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public enum QuestResult { 
    NONE,
    DONE,
    WELLDONE,
}

public class QuestPref : MonoBehaviour
{
    public QuestEvent currEvent { get; private set; }
    //QuestEvent.EventStatus currStatus;
    RectTransform rtCurrent;
    Dictionary<Lang, string> uiText = new Dictionary<Lang, string>();   
    Text currentText;
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
                break;
            default:
                break;
        }
    }
    void Awake()
    {
        rtCurrent = GetComponent<RectTransform>();
        currentText = GetComponentInChildren<Text>();
    }
    void Update()
    {
 //       QuestRules();
    }

    public void GetAnsvers(string[] answ) 
    {
        var must = currEvent.curentquest.MustOrder;
        var right = currEvent.curentquest.RightOrder;
        if (right == answ)
        {
            qResult = QuestResult.WELLDONE;
            UpdateButton(QuestEvent.EventStatus.DONE);
            return;
        }
        //if (must == answ)
        //{
        //    UpdateButton(QuestEvent.EventStatus.DONE);
        //}
        bool[] answIndek = new bool[must.Length];
        for (int j = 0; j < must.Length; j++)
        {
            for (int i = 0; i < answ.Length; i++)
            {
                if (answ[i]==must[j])
                {
                    answIndek[j] = true;
                }
            }
        }
        foreach (var item in answIndek)
        {
            if (!item)
            {
                return;
            }
        }
        qResult = QuestResult.DONE;
        UpdateButton(QuestEvent.EventStatus.DONE);
    }

    private void QuestRules()
    {
        switch (currEvent.status)
        {
            case QuestEvent.EventStatus.WAITING:
                break;
            case QuestEvent.EventStatus.CURRENT:
                //Component a = questObgect.GetComponent(currEvent.name)as MonoBehaviour;// as Type.GetType(currEvent.name);
                //ChekAnswers(a);
                
                break;
            case QuestEvent.EventStatus.DONE:
                break;
            default:
                break;
        }
    }

    private void ChekAnswers(ITransferData a)
    {
       
    }
}
