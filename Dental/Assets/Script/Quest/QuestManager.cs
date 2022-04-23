using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest quest = new Quest();

    private void Start()
    {
        QuestEvent a = quest.AddQuestEvent("test1", "descript 1");
        QuestEvent b= quest.AddQuestEvent("test2", "descript 2");

        quest.AddPath(a.id, b.id);

        quest.BFS(a.id);
        quest.PrintPath();
    }

}
