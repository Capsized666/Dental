using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{

    Text ResultText;
    Dictionary<Lang, string> notice;
    Dictionary<Lang, string> grade;
    // Start is called before the first frame update
    void Awake()
    {
        ResultText = GetComponentInChildren<Text>();
        notice = ServiceStuff.Instance.getUIDict("Grade");//
        grade = ServiceStuff.Instance.getUIDict("NONE");
        ResultText.text = $"{notice[ServiceStuff.Instance.getLang()]} >{grade[ServiceStuff.Instance.getLang()]}<"; 
    
    }

    private void Start()
    {
        UIEventSystem.Instance.onResultShow += Graduet;
    }
    private void OnDisable()
    {
        UIEventSystem.Instance.onResultShow -= Graduet;
    }
    // Update is called once per frame
    void Update()
    {
        Graduet();
    }

    public void Graduet() {
        QuestManager.Instance.Grade();
        grade = ServiceStuff.Instance.getUIDict($"{QuestManager.Instance.Grade()}");
        ResultText.text = $"{notice[ServiceStuff.Instance.getLang()]} >{grade[ServiceStuff.Instance.getLang()]}<";


    }
    public void MainMenu() {
        ScenaManager.Instance.LoadScene(0);
    }
    public void Retry()
    {
        QuestManager.Instance.ResultHide();
        ScenaManager.Instance.LoadScene(SceneManager.GetActiveScene().name);
    }
}
