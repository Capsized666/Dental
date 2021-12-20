using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CloseImage : MonoBehaviour, IPointerDownHandler
{
    RectTransform rect;
    UniversalScreenBeh Manager;
    Image cImage;
    public float size;

    public void SetManager(UniversalScreenBeh m) {
        Manager = m;
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        cImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        alpha();
        sizeManager();
    }

    void alpha() { 
        var alpha = Manager.curentImage.color.a;
        cImage.color = new Color
            (cImage.color.r, cImage.color.g, cImage.color.b, alpha);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Manager.closeWindow();
    }
    private void sizeManager()
    {
        if (MCUI.Instance.getCanvasSize().y * size != rect.sizeDelta.y
            )
        {
            rect.sizeDelta = new Vector3(MCUI.Instance.getCanvasSize().y * size,
                MCUI.Instance.getCanvasSize().y * size);
        }



    }
}
