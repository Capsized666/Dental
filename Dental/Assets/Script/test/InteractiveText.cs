using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveText : MonoBehaviour
{

    CanvasBeh _canvas;
    RectTransform _rt;
    Text _txt;

    
    void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _txt = GetComponent<Text>();
        _txt.text = "";
    }
    private void Start()
    {
        
        _canvas = CanvasBeh.Instance;
    }

    void Update()
    {
        if (
            _canvas.getSize() != _rt.sizeDelta*0.16f
            )
        {
            _rt.sizeDelta = _canvas.getSize() * 0.16f;
        }
    }

    public void SetText(string s) {
        _txt.text = s;    
    }
    
}
