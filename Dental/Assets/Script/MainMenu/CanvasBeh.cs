
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBeh : MonoBehaviour
{
    
    public static CanvasBeh Instance;
    Canvas canvas;
    InteractiveText _interText;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        canvas = GetComponent<Canvas>();
        _interText = GetComponentInChildren<InteractiveText>();
    }

    public Vector2 getSize() {
        var a = canvas.GetComponent<RectTransform>().sizeDelta;
        return new Vector2(a.x,a.y);
    }

    private void FixedUpdate()
    {
        
    }

}
