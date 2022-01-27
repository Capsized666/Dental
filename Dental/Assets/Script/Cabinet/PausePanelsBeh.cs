using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PausePanelsBeh : MonoBehaviour
{
    //public pauseState currentState;
    RectTransform rt;
    public RectTransform rtpause;
    public RectTransform rtback;
    public RectTransform rtoption;
    public RectTransform rtexit;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    void Update()
    {
        sizeManager(CanvasBeh.Instance.getSize());
    }
    private void sizeManager(Vector2 screen)
    {
        rt.sizeDelta = new Vector2(screen.x*0.25f,screen.y*0.5f);
        rtpause.sizeDelta = new Vector2(screen.x * 0.16f, screen.y * 0.07f);
        rtoption.sizeDelta = rtpause.sizeDelta;
        rtexit.sizeDelta = rtpause.sizeDelta;
        rtback.sizeDelta = rtpause.sizeDelta;

        rt.anchoredPosition = Vector2.zero;
        rtpause.anchoredPosition = Vector2.zero;

        rtoption.anchoredPosition   = new Vector2(0,rtexit.sizeDelta.y*2.4f);
        rtexit.anchoredPosition     = new Vector2(0,rtexit.sizeDelta.y*1.2f);
        rtback.anchoredPosition     = Vector2.zero;
    }
}
