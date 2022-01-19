using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceCheckerBeh : MonoBehaviour
{
    RectTransform rt;
    RectTransform parentRT;
    public Text placeTab;
    public Dropdown dd;
    [Space]
    public RectTransform textrt;
    public RectTransform ddrt;

    listOfPlaces lop;
    int ChildNumber;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        dd.options.Clear();
        dd.onValueChanged.AddListener(delegate { ddItemSelected(dd); });
    }


    private void ddItemSelected(Dropdown dd)
    {
        int ind = dd.value;
        var mail = dd.options[ind].text;

    }

    public void setListOfPlaces(listOfPlaces lo) {
        lop = lo;
    }

    public void setOptions(string[] args) {
        foreach (var item in args)
        {
            dd.options.Add(new Dropdown.OptionData() { text = item }) ;
        }
    }

    void Start()
    {
        ChildNumber = rt.GetSiblingIndex();
        parentRT = gameObject.transform.parent.GetComponentInParent<RectTransform>();
    }


    void LateUpdate()
    {
        rectCalc();
    }

    private void rectCalc()
    {
        rt.sizeDelta = new Vector2(parentRT.sizeDelta.x * 0.95f, (parentRT.sizeDelta.y-50) * 0.22f);
        rt.anchoredPosition = new Vector2(0 , -(ChildNumber) * (rt.sizeDelta.y * 1.02f)-50);
        if (ChildNumber != rt.GetSiblingIndex())
        {
            ChildNumber = rt.GetSiblingIndex();
        }
        textrt.sizeDelta = new Vector2(rt.sizeDelta.x * 0.62f, rt.sizeDelta.y * 0.45f);
        textrt.anchoredPosition = new Vector2(0, rt.sizeDelta.y * 0.04f);

        ddrt.sizeDelta = new Vector2(rt.sizeDelta.x * 0.62f, rt.sizeDelta.y * 0.45f);
        ddrt.anchoredPosition = new Vector2(0, -rt.sizeDelta.y * 0.04f);

    }
}
