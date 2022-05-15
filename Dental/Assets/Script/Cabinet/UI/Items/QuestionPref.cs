using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPref : MonoBehaviour
{
    AskinBar curator;
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
        if (true)
        {

        }
    }
    public void SetOrderNumber(int i) {
        Order = i;
    }
    public void RefreshData() {
        QuestionText.text = uiQuest[ServiceStuff.Instance.getLang()];
    }
    public void SetCurator(AskinBar c) {
        curator = c;
    }
    public void RefreshData(Lang l)
    {
        QuestionText.text = uiQuest[l];
        QuestButton.onClick.AddListener(()=> {
            curator.Ansvering(Order, uiQuest, uiAnsw);
            
        }) ;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
