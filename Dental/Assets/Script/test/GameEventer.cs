using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="CustumEvents",menuName ="SI/Create Game Event")]
public class GameEventer : ScriptableObject
{
    private List<GameEventListener> auditory =new List<GameEventListener>() ;

    public void AddPerson(GameEventListener listener){auditory.Add(listener);}
    public void DelPerson(GameEventListener listener){auditory.Remove(listener);}

    public void Init() {
        for (int i = 0; i < auditory.Count; i++)
        {
            auditory[i].EventRised();
        }
    }

}
