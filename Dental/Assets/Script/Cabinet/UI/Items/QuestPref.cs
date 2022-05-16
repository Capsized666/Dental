using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPref : MonoBehaviour
{
    QuestEvent currEvent;
    QuestEvent.EventStatus currStatus;
    RectTransform rtCurrent;
    Dictionary<Lang, string> uiText = new Dictionary<Lang, string>();   
    Text currentText;


    public void Setup(QuestEvent e, Dictionary<Lang, string> txt) {
        currEvent = e;
        currStatus = e.status;
        uiText = txt;
        if (currentText==null)
        {
            currentText = GetComponentInChildren<Text>();
        }
        currentText.text = uiText[ServiceStuff.Instance.getLang()]; 

        //transform.SetParent(parent.transform);
    }
    public void UpdateButton(QuestEvent.EventStatus s)
    {
        currStatus = s;
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
        rtCurrent = GetComponent<RectTransform>();
        currentText = GetComponentInChildren<Text>();







    }
    void Update()
    {
        
    }
}
