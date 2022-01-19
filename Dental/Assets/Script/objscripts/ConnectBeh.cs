using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConnectBeh : MonoBehaviour//, IPointerClickHandler
{
    public Conects statement;
    //public GuideBeh MasterGuide;
    public GuideMaster MasterGuide;


    private void OnMouseDown()
    {
        MasterGuide.Moving(statement.name);
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    print("eeeee"); 
    //    MasterGuide.Moving(statement.name);
    //
    //}


}
