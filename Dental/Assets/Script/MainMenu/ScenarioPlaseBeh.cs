using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioPlaseBeh : MonoBehaviour
{
    //public GameObject panelPref;
    //public HorizontalLayoutGroup hlgContent;


    public GameObject PacientLabelPref;
    List<RectTransform> ContentPanels = new List<RectTransform>();
    public RectTransform currentrt;
    public RectTransform content;
    public GridLayoutGroup gContent;


    void Awake()
    {
        currentrt = GetComponent<RectTransform>();
        //hlgContent = content.GetComponent<HorizontalLayoutGroup>();
        gContent = content.GetComponent<GridLayoutGroup>();
    }
    private void Start()
    {
        creatingLabel();
        
    }

    private void creatingLabel()
    {
        var lp = ServiceStuff.Instance.currLangPack;
        var pacientcount = lp.PAnswer.Length >= 3 ? 3 : lp.PAnswer.Length;  
        //for (int i = 0; i < pacientcount; i++)
        //{
        //    var go = Instantiate(panelPref);
        //    go.transform.SetParent(content);
        //}
        for (int i = 0; i < lp.PAnswer.Length; i++)
        {
            var go = Instantiate(PacientLabelPref);
            var rt = go.GetComponent<RectTransform>();
            var plb = go.GetComponent<PacientLabelBeh>();
            plb.setName(lp.PAnswer[i].Patient);
            plb.AddDictionary(lp.PAnswer[i].PassportData[0].uiTextD);
            plb.AddDictionary(lp.PAnswer[i].PassportData[1].uiTextD);
            go.transform.SetParent(content);
            ContentPanels.Add(rt);
            
        }
    }
            



    void Update()
    {
        sizeManager();
    }
    private void sizeManager()
    {
        int pc = ContentPanels.Count;
        
        Vector2 spacing = new Vector2(currentrt.sizeDelta.x*0.01f, currentrt.sizeDelta.y * 0.01f);
        Vector2 cell    = new Vector2(currentrt.sizeDelta.x/3-(4*spacing.x), 
            currentrt.sizeDelta.y / 3 - (4 * spacing.y));

         gContent.cellSize= cell;
         gContent.spacing = spacing;
    }
}
