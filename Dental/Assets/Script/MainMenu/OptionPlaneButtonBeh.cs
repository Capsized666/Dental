using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionPlaneButtonBeh : MonoBehaviour, IPointerDownHandler
{
    public enum OPButton
    {
        lang = 0,
        mode = 1,
        back = 2
    }
    public OPButton currentcase;

    public TextMeshProUGUI tmpText;
    Dictionary<Lang, string> nameDic = new Dictionary<Lang, string>();
    public menuManager _mm;

    private void Awake()
    {
        _mm = FindObjectOfType<menuManager>();
    }
    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        nameDic = ServiceStuff.Instance.getUIDict(gameObject.name);
    }
    void Update()
    {
        tmpText.text = nameDic[ServiceStuff.Instance.getLang()];
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        switch (currentcase)
        {
            case OPButton.lang:
                ServiceStuff.Instance.changeLang();
                break;
            case OPButton.mode:
                ServiceStuff.Instance.changeMode();
                break;
            case OPButton.back:
                _mm.setState(menuState.firstPlane);
                break;
        }
    }
}