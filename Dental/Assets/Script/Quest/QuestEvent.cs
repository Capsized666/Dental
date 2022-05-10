using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvent 
{
    public enum EventStatus { WAITING,CURRENT,DONE};
    public EventStatus status;


    public string name;
    public Dictionary<Lang, string> description;
    public string id { get; }
    public int order=-1;
    public GameQuests qvestInfo; 


    public List<QuestPath> pathlist = new List<QuestPath>();
    

    public QuestEvent(GameQuests q) {

        id = Guid.NewGuid().ToString();
        qvestInfo = q;
        name = q.QuestsName;
        description = q.GetDetalis(name);
        status = EventStatus.WAITING;

    }
    public QuestEvent(string n,string d)
    {
        id = Guid.NewGuid().ToString();
        name = n;
        description = new Dictionary<Lang, string>() 
        { {Lang.en, "Erorr" } };
        status = EventStatus.WAITING;

    }
    public void UpdateQuestEvent(EventStatus es)
    {
        status = es;
    }

}
