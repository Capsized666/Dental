using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


public class GuideMapBeh : MonoBehaviour
{
    public GuideMaster gm;
    public GuideBeh MasterGuide;

    public TextAsset jsonInstruction;
    public SpriteAtlas atlasMap;
    public Sprite currentPlace;
    public Sprite otherPlace;
    
    [Space]
    public MapObjBeh mapObj;
    public Image CurrentBacground;
    public GameObject pointToShow;

    [Space]
    //public Map currentMap = new Map();
    public List<Image> pointsInRoom;
    public string[] position;
    bool isClose;
    private RectTransform rt;
    [Space]
    public float size;
    public int pointOnFloar;

    void Awake()
    {
        pointsInRoom = new List<Image>();
        rt = GetComponent<RectTransform>();
        
        //currentMap = JsonUtility.FromJson<Map>(jsonInstruction.text);
        //var p    = MCUI.Instance.getCanvasSize()*0.32f;
        //new Vector2(p.x,p.y);
        isClose = false;
        currentPlace = atlasMap.GetSprite("pointPlace");
        otherPlace   = atlasMap.GetSprite("point");
        CurrentBacground = GetComponent<Image>();
        pointOnFloar = 0;
    }
    void Start()
    {
        rt.sizeDelta = MCUI.Instance.getCanvasSize() * size;
        updateMap();
    }
    private void LateUpdate()
    {
        if (rt.sizeDelta != MCUI.Instance.getCanvasSize() * size) {
            rt.sizeDelta = MCUI.Instance.getCanvasSize() * size;
        }
        positionInSpace();
    }

    public void setGM(GuideMaster _gm)
    {
        gm = _gm;
    }
        
    public bool isCorect() {
        return pointOnFloar == mapObj.pointList.Count;
    }

    private void positionInSpace()
    {
        if (isClose)
        {
            rt.anchoredPosition = new Vector2(
                Mathf.Lerp(rt.anchoredPosition.x, 0, Time.deltaTime*1.5f)
                , Mathf.Lerp(rt.anchoredPosition.y, 0, Time.deltaTime * 1.5f)
                );
        }
        else
        {
            rt.anchoredPosition = new Vector2(
                Mathf.Lerp(rt.anchoredPosition.x, -rt.sizeDelta.x, Time.deltaTime * 1.5f)
                , Mathf.Lerp(rt.anchoredPosition.y, 0, Time.deltaTime * 1.5f)
                );
        }
    }

    public void changeVisible()
    {
        isClose = isClose ? false : true;
    }

    public bool State() {
        return isClose;
    }

    private void updateMap()
    {
        foreach (var item in pointsInRoom)
        {
            Destroy(item);
        }
        pointsInRoom = new List<Image>();
    }





    public void changePlace(string[] args, building build) {

        pointOnFloar = 0;
        mapObj.CleearChildObj();
        if (atlasMap.GetSprite($"{args[0]}")!=null)//_{args[1]}
        {
             mapObj.SetMapBackground(atlasMap.GetSprite($"{args[0]}"));//"{args[0]}_{args[1]}"
        }

        for (int i = 0; i < build.Rooms.Length; i++)
        {
            var roomName = build.Rooms[i].name;

            for (int j = 0; j < build.Rooms[i].point.Length; j++)
            {
                var pointName = build.Rooms[i].point[j].name;
                var go = Instantiate(pointToShow);
                var pb = go.GetComponent<PointBeh>();
                if (roomName == args[2] & pointName == args[3]) {
                    go.GetComponent<Image>().sprite = currentPlace;
                }
                //pb.MasterGuide = MasterGuide;
                pb.MasterGuide = gm;
                pb.statement = new string[4] {args[0],args[1],roomName,pointName };
                pb.SetProportion (build.Rooms[i].point[j].GetCordin());
                mapObj.AddPoint(go);
                pointOnFloar+=1;
            }
        }

    }
}