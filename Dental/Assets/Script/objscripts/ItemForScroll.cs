using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemForScroll : MonoBehaviour, IPointerDownHandler
{
    
    GuideMaster MasterGuide;
    Image currentImg;
    public string[] statement;
    public Text curentplase;
    string txt;

    void Start()
    {
        //curentplase.text = txt;
       // MasterGuide = FindObjectOfType<GuideMaster>();
    }

    void LateUpdate()
    {
        if (MasterGuide.CurrentBuild == statement[0] &
            MasterGuide.CurrentFloor.ToString() == statement[1] &
            MasterGuide.CurrentRoom == statement[2] &
            MasterGuide.CurrentPos == statement[3]
            )
        {
            curentplase.color = Color.green;

        }
        else {
            curentplase.color = Color.white;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        MasterGuide.Moving(statement);
    }

    public void setGM(GuideMaster gm) {
        MasterGuide = gm;
    }
    public void setStatement(string[] gm)
    {
        statement = gm;
    }
    public void setName(string gm)
    {
        curentplase.text = gm;
    }
}