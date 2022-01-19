using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PacientLabelBeh : MonoBehaviour,IPointerDownHandler
{
    RectTransform curentRt;
    RectTransform textRt;
    public TextMeshProUGUI tmpText;
    List<Dictionary<Lang, string>> namePacient = new List<Dictionary<Lang, string>>();

    public void setName(string name) {
        gameObject.name = name;
    }
    void Start()
    {
        curentRt= GetComponent<RectTransform>() ;
        tmpText = GetComponentInChildren<TextMeshProUGUI > ();
        textRt  = tmpText.GetComponent<RectTransform>() ;
    }

    // Update is called once per frame
    void Update()
    {
        sizeManager();
        namingPacient();
    }

    private void OnMouseDown()
    {
        
    }

    private void namingPacient()
    {//ServiceStuff.Instance.getLang()

        if (namePacient.Count>=2)
        {

        tmpText.text = namePacient[0][ServiceStuff.Instance.getLang()]+"\n"+
                       namePacient[1][ServiceStuff.Instance.getLang()];
        }
    }
     
    private void sizeManager()
    {
        textRt.sizeDelta = new Vector2(curentRt.sizeDelta.x * 0.95f, curentRt.sizeDelta.y * 0.95f);
    }

    public void AddDictionary(Dictionary<Lang, string> dic) {
        namePacient.Add(dic);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //print($"kill bill");


    }
}