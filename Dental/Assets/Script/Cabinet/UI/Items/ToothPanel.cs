using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ToothState {
    none=0,
    C,
    P,
    Pt,
    Lp,
    Gp,
    R,
    A,
    Cd,
    Pl,
    F,
    ar,
    r,
    H,
    Am,
    res,
    pin,
    i,
    Rp,
    Dc,
}
public class ToothPanel : MonoBehaviour
{
    public RectTransform curentRect;
    public Dropdown curentdrop;
    public MedicalCard Master { get; private set; }
    Place current;
    float scaler = 1;
    ToothState curState;

    public void Current(Place f, float s)
    {
        current = f;
        scaler = s;
        MakeUpdate();
    }
    public void Slavery(MedicalCard boss)
    {
        Master = boss;
    }
    public Place GetCurrent()
    {
        return current;
    }
    public void SetVisible(bool t)
    {
        gameObject.SetActive(t);
    }
    // Start is called before the first frame update
    void Awake()
    {
        curentRect = GetComponent<RectTransform>();
        curentdrop = GetComponent<Dropdown>();
        curentdrop. ClearOptions();

        var droplist = new List<string>();
        foreach (ToothState item in Enum.GetValues(typeof(ToothState)))
        {
            droplist.Add(item != ToothState.none ? item.ToString() : " ");
        }
        curentdrop.AddOptions(droplist);
    }
    public Vector2 getPlace()
    {
        return new Vector2(curentRect.anchoredPosition.x / scaler,
            -curentRect.anchoredPosition.y / scaler);
    }
    public Vector2 getSize()
    {
        return curentRect.sizeDelta / scaler;
    }
    public void MakeUpdate(
        //string wStr
        )
    {
        curentdrop.value = 0;
        //if (Writen)
        //{
        //    gameObject.name = $"{current.name}";
        //}
        //curentText.text = $"{wStr}";

        curentRect.sizeDelta = current.getSizeVector() * scaler;
        curentRect.anchoredPosition = new Vector2(current.getPointVector().x,
               -current.getPointVector().y) * scaler;
    }
}
