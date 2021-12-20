using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelBeh : MonoBehaviour
{
    public Slider slid;
    public Text nameOfParametr;
    public RectTransform SLRT;

    RectTransform rt;
    RectTransform parentRT;
    int ChildNumber;


    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        parentRT = gameObject.transform.parent.GetComponentInParent<RectTransform>();
        ChildNumber = rt.GetSiblingIndex();

    }
    void Update()
    {
        rt.sizeDelta = new Vector2(parentRT.sizeDelta.x * 0.95f, parentRT.sizeDelta.y * 0.10f);
        rt.anchoredPosition = new Vector2(0, -(ChildNumber)*(rt.sizeDelta.y *1.02f));
        nameOfParametr.rectTransform.sizeDelta =
            new Vector2(rt.sizeDelta.x * 0.95f, rt.sizeDelta.y * 0.50f);
        SLRT.sizeDelta =
        new Vector2(rt.sizeDelta.x * 0.95f, rt.sizeDelta.y * 0.38f);


        if (ChildNumber!= rt.GetSiblingIndex())
        {
            ChildNumber = rt.GetSiblingIndex();
        }
    }


    public void setParametrName(string s) {
        nameOfParametr.text = s;
    }

    public float getSliderValue() {
        return slid.value;
    }
    

}
