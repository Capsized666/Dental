using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Quest 
{
    public List<QuestEvent> questEvents = new List<QuestEvent>();
     
    public Quest(){
    }
    public GameQuests currentGQ;

    public QuestEvent HeadEvent { get; private set; }
    public QuestEvent lastEvent { get; private set; }

    public QuestEvent GetCurentQuest() {
        foreach (var item in questEvents)
        {
            if (item.status==QuestEvent.EventStatus.CURRENT)
            {
                return item;
            }
        }
        return null;
    }
        
    public QuestEvent GetHeadQuest()
    {
        QuestEvent head = new QuestEvent();

        for (int i = 0; i < questEvents.Count; i++)
        {

            if (questEvents[i].curentquest.Name == currentGQ.QuestsName)
            {
                head = questEvents[i];
                return head;
            }
        }
        return null;
    }
    public Quest AddQuestEvents(GameQuests d)
    {
        currentGQ = d;
        for (int i = 0; i < d.PlayZone.Length; i++)
        {
            QuestEvent questEvent = new QuestEvent(d.PlayZone[i]
                , d.GetDetalis(d.PlayZone[i].Name));
            questEvent.SetQZ(d.PlayZone[i]);
            questEvents.Add(questEvent);
        }
        setPath();
        return this;
    }

    private void setPath()
    {
        QuestEvent head = GetHeadQuest();
        if (head!=null)
        {
            head.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
            QuestEvent last = new QuestEvent();
            
            for (int i = 0; i < questEvents.Count; i++)
            {
            
                for (int w = 0; w    < head.curentquest.MustOrder.Length; w++)
                {
                    var nameQ= head.curentquest.MustOrder[w];
                    if (questEvents[i].name==nameQ)
                    {
                        if (w==0)
                        {
                            AddPath(head.curentquest, questEvents[i].curentquest);
                            last = questEvents[i];
                            //Debug.Log($"{questEvents[i].name}?{last.name}");

                        }
                        if (w > 0)
                        {
                            AddPath(last.curentquest, questEvents[i].curentquest);
                            last = questEvents[i];
                            //Debug.Log($"{questEvents[i].name}?{last.name}");
                        }
                    }
                }
            }
            //Debug.Log($"|{last.name}|");
            lastEvent = last;
            AddPath(last.curentquest,head.curentquest);
            //head.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
        }

    }
    public void AddPath(questZone fromQuestEvent, questZone toQuestEvent) {

        QuestEvent from = new QuestEvent();// FindQuestEvent(fromQuestEvent);
        QuestEvent to   = new QuestEvent();// FindQuestEvent(toQuestEvent);

        for (int i = 0; i < questEvents.Count; i++)
        {

            if (fromQuestEvent.Name== questEvents[i].name)
            {
                from = questEvents[i];
            }
            if (toQuestEvent.Name == questEvents[i].name)
            {
                to = questEvents[i];
            }
        }

        if (from != null && to != null&from!=to)
        {
            QuestPath p = new QuestPath(from, to);
            from.pathlist.Add(p);
        }
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
            //Debug.Log(n.name+" "+n.order);
            foreach (var item in n.pathlist)
            {
                Debug.Log($"{item.start.name},{item.end.name}") ;
            }
        }

    }
}

    //QuestEvent FindQuestEvent(string id)
    //{
    //    foreach (QuestEvent n in questEvents)
    //    {
    //        if (n.id == id)
    //        {
    //            return n;
    //        }
    //    }
    //    return null;
    //}

        /*setpath
        
        for (int i = 0; i < head.curentquest.RightOrder.Length-1; i++)
        {
            //if (head.curentquest.RightOrder[i]==questEvents[i])
            //{
            //
            //}
            

        }
         */
    //public AddQuestEvent(string n,string d)
    //{
    //    QuestEvent questEvent = new QuestEvent(n, d);
    //    questEvents.Add(questEvent);
    //    return questEvent;
    //}
    //public void AddQuestEvents(GameQuests d)
    //{
    //    currentGQ = d;
    //    for (int i = 0; i < d.PlayZone.Length; i++)
    //    {
    //        QuestEvent questEvent = new QuestEvent(d.PlayZone[i]
    //            , d.GetDetalis(d.PlayZone[i].Name));
    //        questEvent.SetQZ(d.PlayZone[i]);
    //    }
    //    setPath();
    //}