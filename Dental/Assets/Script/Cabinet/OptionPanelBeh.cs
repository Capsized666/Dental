using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanelBeh : MonoBehaviour
{
    public RectTransform rtLang;
    public RectTransform rtLanguage;
    public RectTransform rtLabel;
    public RectTransform rtback;
    RectTransform rt;
    // Start is called before the first frame update
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
        rt.sizeDelta = new Vector2(screen.x * 0.5f, screen.y * 0.25f);
        rt.anchoredPosition = Vector2.zero;

        rtLang    .sizeDelta= new Vector2(screen.x * 0.16f, screen.y * 0.07f);
        rtLanguage.sizeDelta= rtLang.sizeDelta;
        rtLabel   .sizeDelta= rtLanguage.sizeDelta;
        rtback    .sizeDelta= rtLabel.sizeDelta;

        /*
        rtpause.sizeDelta = new Vector2(screen.x * 0.16f, screen.y * 0.07f);
        rtoption.sizeDelta = rtpause.sizeDelta;
        rtexit.sizeDelta = rtpause.sizeDelta;
        rtback.sizeDelta = rtpause.sizeDelta;

        rtpause.anchoredPosition = Vector2.zero;

        rtoption.anchoredPosition = new Vector2(0, rtexit.sizeDelta.y * 2.4f);
        rtexit.anchoredPosition = new Vector2(0, rtexit.sizeDelta.y * 1.2f);
        rtback.anchoredPosition = Vector2.zero;
         */
    }
}
