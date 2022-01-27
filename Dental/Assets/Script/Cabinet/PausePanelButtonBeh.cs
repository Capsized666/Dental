using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PausePanelButtonBeh : MonoBehaviour, IPointerDownHandler
{
    public enum pauseButton { 
        option=1,
        exit=2,
        back=3
    }
    public pauseButton currentButton;
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

    void Update()
    {
        tmpText.text = nameDic[ServiceStuff.Instance.getLang()];
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
        switch (currentButton)
        {
            case pauseButton.option:
                _pm.setState(pauseState.Options);
                break;
            case pauseButton.exit:
                _pm.setState(pauseState.ExitQ);
                break;
            case pauseButton.back:
                _pm.setState(pauseState.game);
                _pm.setPause(false);
                break;
            default:
                break;
        }
    }
}