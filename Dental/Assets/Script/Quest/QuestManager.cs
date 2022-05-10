using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    public static QuestManager Instance;
    public Quest quest = new Quest();

    private void Awake()
    {
        if (Instance != this)
        {
            Instance = this;
        };
        CreateQuect();
    }

    private void CreateQuect()
    {
        foreach (var item in ServiceStuff.Instance.currLangPack.GQuests)
        {
            quest.AddQuestEvent(item);
        }
        quest.setLinearPath();
        
        //QuestEvent a = quest.AddQuestEvent("test1", "descript 1");
        //QuestEvent b= quest.AddQuestEvent("test2", "descript 2");
        //QuestEvent a = quest.AddQuestEvent("test1", "descript 1");
        //quest.AddPath(a.id, b.id);
        //
        //quest.BFS(a.id);
        quest.PrintPath();
    }

    private void Start()
    {
    }

}
