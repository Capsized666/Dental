using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitPanelButtonBeh : MonoBehaviour, IPointerDownHandler
{
    public enum exitButton { 
        yes=0,
        no=1,
    }
    public exitButton currentButton;

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
        switch (currentButton)
        {
            case exitButton.yes:
                _pm.setTimeScale(1);
                ComendantOnScene.Instance.loadScene(scenes.MainMenu);
                break;
            case exitButton.no:
                _pm.setState(pauseState.Pause);
                break;
            default:
                break;
        }
    }
}
