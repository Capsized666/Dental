using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class QuestEvent 
{
    public enum EventStatus { WAITING,CURRENT,DONE};
    public EventStatus status { get; private set; }


    public string name;
    public Dictionary<Lang, string> description;
    public string id { get; }
    public int order=-1;
    public questZone curentquest;


    public List<QuestPath> pathlist = new List<QuestPath>();

    public QuestEvent()
    {

    }
    public QuestEvent(questZone q, Dictionary<Lang, string> d) {

        id = Guid.NewGuid().ToString();
        name = q.Name;
        description = d;
        status = EventStatus.WAITING;

    }
    public QuestEvent(string n, Dictionary<Lang, string> d)
    {
        id = Guid.NewGuid().ToString();
        name = n;
        description = d;
        status = EventStatus.WAITING;

    }
    public void UpdateQuestEvent(EventStatus es)
    {
        status = es;
    }
    public void SetQZ(questZone qz) {
        curentquest = qz;
    }
}
