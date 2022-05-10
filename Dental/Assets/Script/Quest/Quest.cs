using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Quest
{
    public List<QuestEvent> questEvents = new List<QuestEvent>();
     
    public Quest(){
    }

    public QuestEvent AddQuestEvent(string n,string d)
    {
        QuestEvent questEvent = new QuestEvent(n, d);
        questEvents.Add(questEvent);
        return questEvent;
    }
    public QuestEvent AddQuestEvent(GameQuests d)
    {
        QuestEvent questEvent = new QuestEvent(d);
        questEvents.Add(questEvent);
        return questEvent;
    }

    public void AddPath(string fromQuestEvent,string toQuestEvent)
    {
        QuestEvent from =   FindQuestEvent(fromQuestEvent);
        QuestEvent to  =    FindQuestEvent(toQuestEvent);

        if (from!=null && to!=null)
        {
            QuestPath p = new QuestPath(from, to);
            from.pathlist.Add(p);
        }
    }
    QuestEvent FindQuestEvent(string id)
    {
        foreach (QuestEvent n in questEvents)
        {
            if (n.id==id)
            {
                return n;
            }
        }
        return null;
    }

    public void BFS(string id, int orderNumber = 1) {

        QuestEvent thisEvent = FindQuestEvent(id);
        thisEvent.order = orderNumber;
        foreach (QuestPath e in thisEvent.pathlist)
        {
            if (e.end.order==-1)
            {
                BFS(e.end.id, orderNumber + 1);
            }
        }
    }

    public void setLinearPath() {
        for (int i = 0; i < questEvents.Count-1; i++)
        {
            AddPath(questEvents[i].id, questEvents[i+1].id);
        }
        BFS(questEvents[0].id);
        questEvents[0].UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
    }
    public QuestEvent getCurent() {
        foreach (var item in questEvents)
        {
            if (item.status==QuestEvent.EventStatus.CURRENT)
            {
                return item;
            }
        }
        return null;
    }

    public void PrintPath()
    {
        foreach (QuestEvent n in questEvents)
        {
            Debug.Log(n.name+" "+n.order);
        }
    }
}
