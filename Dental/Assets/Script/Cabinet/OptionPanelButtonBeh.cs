using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionPanelButtonBeh : MonoBehaviour, IPointerDownHandler
{
    public enum optionButton
    {
        lang = 0,
        mode = 1,
        back = 2
    }
    public optionButton currentcase;

    public TextMeshProUGUI tmpText;
    Dictionary<Lang, string> nameDic = new Dictionary<Lang, string>();
    public PauseBeh _pm;


    private void Awake()
    {
        _pm = FindObjectOfType<PauseBeh>();
    }
    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        nameDic = ServiceStuff.Instance.getUIDict(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        tmpText.text = nameDic[ServiceStuff.Instance.getLang()];
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        switch (currentcase)
        {
            case optionButton.lang:
                ServiceStuff.Instance.changeLang();
                break;
            case optionButton.mode:
                ServiceStuff.Instance.changeMode();
                break;
            case optionButton.back:
                _pm.setState(pauseState.Pause);
                break;
            default:
                break;
        }
    }
}
