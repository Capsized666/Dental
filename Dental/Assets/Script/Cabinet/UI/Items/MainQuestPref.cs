using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainQuestPref : MonoBehaviour
{

    GameQuests HeadQuest;
    RectTransform rtCurrent;
    Dictionary<Lang, string> uiText = new Dictionary<Lang, string>();
    Text currentText;
    QuestEvent[] currEvent;
    bool expand=false;

    public void Setup(GameQuests g) {
        HeadQuest = g;

    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if (expand)
        {


        }
        
    }
}
