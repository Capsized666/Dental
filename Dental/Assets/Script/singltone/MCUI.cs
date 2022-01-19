using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Consoleum;
/// <summary>
/// Класс содержащий все ui 
/// </summary>
/// 

public class MCUI : MonoBehaviour
{
    public static MCUI Instance { get; private set; }

    private Canvas canvas;
    private CanvasScaler cScaler;

    public List<GameObject> UIObject { get; private set; }


    /// <summary>
    /// Добавление дочернего обьета
    /// </summary>
    /// <param name="go">Object for adding</param>
    public void AddObgetOnScene(GameObject go)
    {
        //ObjectOnScene.Add(go);
        go.transform.SetParent(this.gameObject.transform);
        go.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        UIObject = RollCall();


    }



    public string[] getAllObject()
    {
        var names = GetRecursChild(this.gameObject.transform, 0);
        return names.ToArray();
    }

    private List<string> GetRecursChild(Transform t, int r)
    {
        List<string> ls = new List<string>();
        string[] folders = GetChildS(t, r);
        for (int i = 0; i < folders.Length; i++)
        {
            ls.Add(folders[i] + "|" + r.ToString());
            ls.AddRange(GetRecursChild(t.GetChild(i), r + 1));
        }

        return ls;
    }

    private string[] GetChildS(Transform t, int r)
    {
        var ret = new string[t.childCount];

        for (int i = 0; i < t.childCount; i++)
        {

            ret[i] = $"{tabs(r)}{t.GetChild(i).name}";
        }
        return ret;
    }

    private string tabs(int r)
    {
        var s = "";
        for (int i = 0; i < r; i++)
        {
            s += "\t";
        }
        return s;
    }

    private List<GameObject> RollCall()
    {
        return GetRecursGO(this.gameObject.transform);
    }

    private List<GameObject> GetRecursGO(Transform t)
    {
        var ls = new List<GameObject>();

        foreach (Transform item in t)
        {
            ls.Add(item.gameObject);
            ls.AddRange(GetRecursGO(item.transform));
        }

        return ls;
    }

    public string[] getOOSNames()
    {
        UIObject = RollCall();
        string[] sr = new string[UIObject.Count];
        var i = 0;
        foreach (var item in UIObject)
        {
            sr[i] = item.name;
            i++;
        }
        return sr;
    }

    public Vector2 getCanvasSize() {
        return new Vector2(canvas.pixelRect.width, canvas.pixelRect.height);
    }


    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        UIObject = new List<GameObject>();
        canvas = GetComponent<Canvas>();
        cScaler= GetComponent<CanvasScaler>();
        cScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        cScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        cScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
        cScaler.referencePixelsPerUnit = 100;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    
}
