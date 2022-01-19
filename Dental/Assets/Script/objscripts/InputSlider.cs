using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputSlider : MonoBehaviour, IPointerDownHandler
{

    RectTransform rect;
    RectTransform iBarRect;
    public bool isHide = true;
    Vector2 size;
    public void OnPointerDown(PointerEventData eventData)
    {
        isHide = isHide ? false : true;
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        var ib = FindObjectOfType<InputBar>();
        iBarRect = ib.gameObject.GetComponent<RectTransform>();
        size = ib.offset;
    }

    // Update is called once per frame
    void Update()
    {
        rect.sizeDelta = new Vector2(Mathf.Abs(MCUI.Instance.getCanvasSize().y * size.y), 
                                     Mathf.Abs(MCUI.Instance.getCanvasSize().y * size.y) );
    }
}
                    
                    

