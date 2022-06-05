using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPref : MonoBehaviour
{
    Vocal curator;
    Text QuestionText;
    Button QuestButton;
    int Order;
    string Answer;
    public Dictionary<Lang, string> uiQuest { get; set; } = new Dictionary<Lang, string>();
    public Dictionary<Lang, string> uiAnsw { get; set; } = new Dictionary<Lang, string>();

    private void Awake()
    {
        QuestionText = gameObject.GetComponentInChildren<Text>();
        QuestButton = gameObject.GetComponentInChildren<Button>();
    }
    void Start() {
       
    }
    public void SetOrderNumber(int i) {
        Order = i;
    }
    public void SetAnswer(string i)
    {
        Answer = i; 
        
    }
    public void RefreshData() {
        QuestionText.text = uiQuest[ServiceStuff.Instance.getLang()];
    }
    public void SetCurator(Vocal c) {
        curator = c;
    }
    public void RefreshData(Lang l)
    {
        QuestionText.text = uiQuest[l];
        QuestButton.onClick.AddListener(()=> {
            curator.Ansvering(Order,Answer, uiQuest, uiAnsw);
            
        }) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
