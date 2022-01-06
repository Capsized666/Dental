using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModeBeh : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    Dictionary<Lang, string> nameDic = new Dictionary<Lang, string>();
    public menuManager _mm;
    void Start()
    {
        _mm = FindObjectOfType<menuManager>();
        tmpText = GetComponent<TextMeshProUGUI>();
        nameDic = ServiceStuff.Instance.getUIDict(ServiceStuff.Instance.getMode().ToString());
        //print(ServiceStuff.Instance.getMode().ToString());

    }

    // Update is called once per frame
    void Update()
    {
        nameDic = ServiceStuff.Instance.getUIDict(ServiceStuff.Instance.getMode().ToString());
        tmpText.text = nameDic[ServiceStuff.Instance.getLang()];
    }
}
