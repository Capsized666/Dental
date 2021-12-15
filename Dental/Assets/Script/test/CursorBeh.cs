using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorBeh : MonoBehaviour
{

    public static CursorBeh Instance;

    public Image _cursorImage;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        Cursor.visible = false;
        _cursorImage = GetComponent<Image>();
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChekCursor();
        //Vector2 curpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = curpos;
    }

    private void ChekCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            _cursorImage.rectTransform.anchoredPosition = Vector2.zero;
        }
        if (Cursor.lockState == CursorLockMode.Confined)
        {
            Vector2 curpos = Input.mousePosition;
            var pos = curpos- CanvasBeh.Instance.getSize() / 2 ;

            _cursorImage.rectTransform.anchoredPosition = pos;
        }
    }
}
