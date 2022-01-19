using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstPlaneButtonBeh : MonoBehaviour, IPointerDownHandler
{
    public enum FPButton { 
    newgame=0,
    option=1,
    exit=2
    }
    public FPButton currentcase;
    public FirstPlaneBeh parent;
    
    public TextMeshProUGUI tmpText;
    Dictionary<Lang, string> nameDic = new Dictionary<Lang, string>();
    public menuManager _mm;

    void Awake()
    {
        _mm = FindObjectOfType<menuManager>();
        parent = gameObject.GetComponentInParent<FirstPlaneBeh>();
        tmpText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
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
            case FPButton.newgame:
                _mm.setState(menuState.newGame);
                break;
            case FPButton.option:
                _mm.setState(menuState.Option);
                break;
            case FPButton.exit:
                Application.Quit();
                break;
            default:
                break;
        }
    }
}
