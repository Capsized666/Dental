using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewLabBeh : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    Dictionary<Lang, string> nameDic = new Dictionary<Lang, string>();
    
    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        nameDic = ServiceStuff.Instance.getUIDict(gameObject.name);
    }

    void Update()
    {
        tmpText.text = nameDic[ServiceStuff.Instance.getLang()];
    }
}
