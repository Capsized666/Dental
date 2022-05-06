using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPref : MonoBehaviour
{
    Text QuestionText;
    Button QuestButton;
    int OrderOfQuest;
    string Answer;
    public Dictionary<Lang, string> uiQuest = new Dictionary<Lang, string>();
    public Dictionary<Lang, string> uiAnsw = new Dictionary<Lang, string>();


    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
