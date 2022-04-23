using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class ServiceStuff:MonoBehaviour {

    public static ServiceStuff Instance;
   
    [SerializeField]
    Lang currlang;
    [SerializeField]
    public mainSetting currLangPack;
    [SerializeField]
    gameMode curentMode;
    public TextAsset textasset;
    string pacient;
    public string Chose { get { return pacient; }set {  pacient= value ; } }

    private void Awake()
    {

        
            if (Instance == null)
            { // Экземпляр менеджера был найден
                Instance = this; // Задаем ссылку на экземпляр объекта
            }
            else if (Instance == this)
            { // Экземпляр объекта уже существует на сцене
                Destroy(gameObject); // Удаляем объект
            }
            currlang =Lang.en;
        curentMode = gameMode.practical;
        loadLang();
        DontDestroyOnLoad(Instance);

    }
    private void OnEnable()
    {
        ChekScene();
    }
    private void Start()
    {
        
    }
    private void loadLang()
    {
        string s = textasset.text;
        currLangPack = JsonUtility.FromJson<mainSetting>(s);
        currLangPack.fillAllText();
    }

    public Dictionary<Lang, string> getUIDict(string name) {
        return currLangPack.FindByName(name);    
    }
    public Lang getLang() { return currlang; }
    public gameMode getMode( ){ return curentMode;}
    public void changeLang() {
        switch (currlang)
        {
            case Lang.ua:
                currlang = Lang.ru;
                break;
            case Lang.ru:
                currlang = Lang.en;
                break;
            case Lang.en:
                currlang = Lang.ua;
                break;
        }
    }
    public void changeMode() {
        curentMode = curentMode == gameMode.practical ? gameMode.examinate: gameMode.practical;
    }

    private void ChekScene()
    {
        var foundObjects = FindObjectsOfType<GameObject>();
        foreach (var item in foundObjects)
        {
            var chek = item.GetComponent<ServiceStuff>();
            if (chek != null & chek != Instance)
            {
                DestroyImmediate(item);
                break;
            }        
        }
    }


}





public class EventString : UnityEvent<string> { }
public enum gameMode
{
    practical = 0,
    examinate = 1
}
public enum Lang
{
    ua = 0,
    ru = 1,
    en = 2
}
[Serializable]
public struct mainSetting
{
    public enum TextType { 
        doctor=1,
        pacient=2
    }

    //public List<ServiceText> uitext;
    public serviceText[] ServiceText;
    public questionText[] DQuestion;
    public answerText[] PAnswer;

    public void fillAllText()
    {

        foreach (var item in ServiceText)
        {
            item.fillText();
        }
        foreach (var item in DQuestion)
        {
            item.fillText();
            item.fillAllText();
        }
        foreach (var item in PAnswer)
        {
            item.fillAllText();
        }
    }

    public Dictionary<Lang, string> FindByName(string s)
    {
        foreach (var item in ServiceText)
        {
            if (s == item.uiname)
            {
                return item.uiTextD;
            }
        }
        return new Dictionary<Lang, string>();
    }

    public Dictionary<Lang, string> FindDioalogText(TextType tt, string name,string patientNum=null) {
        switch (tt)
        {
            case TextType.doctor:
                foreach (var item in DQuestion)
                {
                    if (name == item.uiname)
                    {
                        return item.uiTextD;
                    }
                    foreach (var itm in item.ServiceText)
                    {
                        if (itm.uiname==name)
                        {
                            return itm.uiTextD;
                        }
                    } 
                }     
                break;
            case TextType.pacient:
                foreach (var item in PAnswer)
                {

                    if (patientNum==item.Patient)
                    {
                        foreach (var itm in item.PassportData)
                        {
                            if (itm.uiname ==name)
                            {
                                return itm.uiTextD;
                            }
                        }
                        foreach (var itm in item.PatientComplaints)
                        {
                            if (itm.uiname == name)
                            {
                                return itm.uiTextD;
                            }
                        }
                    }
                }
                break;
        }
        return new Dictionary<Lang, string>();
    }
}
[Serializable]
public class questionText
{
    public string uiname;
    public string[] uitext;
    public serviceText[] ServiceText;
    public Dictionary<Lang, String> uiTextD = new Dictionary<Lang, string>();
    public void fillText()
    {
        uiTextD.Add(Lang.ua, uitext[0]);
        uiTextD.Add(Lang.ru, uitext[1]);
        uiTextD.Add(Lang.en, uitext[2]);
    }
    public void fillAllText() {
        foreach (var item in ServiceText)
        {
            item.fillText();
        }
    }
}
[Serializable]
public class answerText
{
    public string Patient;
    public serviceText[] PassportData;
    public serviceText[] PatientComplaints;
    public void fillAllText()
    {
        foreach (var item in PassportData)
        {
            item.fillText();
        }
        foreach (var item in PatientComplaints)
        {
            item.fillText();
        }
    }
}
[Serializable]
public class serviceText
{
    public string uiname;
    public string[] uitext;
    public Dictionary<Lang, String> uiTextD = new Dictionary<Lang, string>();

    public void fillText()
    {
        uiTextD.Add(Lang.ua, uitext[0]);
        uiTextD.Add(Lang.ru, uitext[1]);
        uiTextD.Add(Lang.en, uitext[2]);
    }

}


//Addressables.LoadAssetAsync<GameObject>("langforgame").Completed += OnLoadDone;

/*
var path = Path.Combine(Application.dataPath + "/Resources", "langforgame.json");

FileStream fileStream = new FileStream(path,
           FileMode.OpenOrCreate,
           FileAccess.ReadWrite,
           FileShare.None);


if (fileStream.CanRead)
{
    byte[] arr = new byte[fileStream.Length];
    fileStream.Read(arr, 0, arr.Length);
    s = System.Text.Encoding.Default.GetString(arr);
    fileStream.Close();
}
 */
//print(s);