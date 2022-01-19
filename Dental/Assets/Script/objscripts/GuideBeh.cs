using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;

public class GuideBeh : MonoBehaviour
{

    public BuldingList currentBuid = new BuldingList();
    public TextAsset jsonInstruction;
    //public Material skybox;

    public GuideMaster gm;
    public GuideMapBeh gMapbeh;
    public UniversalScreenBeh usBeh;
    //public AssetLoader iLoader;

    public List<GameObject> Points2Draw;
    public List<GameObject> IntObj;
    [Space]
    //public string CurrentBuild = "Main";
    //public int CurrentFloor = 1;
    //public string CurrentRoom = "Hall";
    //public string CurrentPos = "pos0";
    //[Space]

    //написать загрузку с адресабле
    public GameObject prefabPoint;
    private bool InteraktiveErect;

    private void Awake()
    {
        InteraktiveErect = false;
        //currentBuid = JsonUtility.FromJson<BuldingList>(jsonInstruction.text);
        Points2Draw = new List<GameObject>();
        IntObj = new List<GameObject>();
    }

    private void Start()
    {
        updateOfAll();
    }

    public void updateOfAll()
    {
        updateEmbient();
        updateEnvironment();
    }

    private void Update()
    {

        if (usBeh.currState == StateOfLoadScreen.Look)
        {
            chekigLists();
        }

        var point = gm.GetPointInfo();
        

        if (RenderSettings.skybox.name == point.skyBox)
        {
            if (gMapbeh.isCorect())
            {
                if (usBeh.currState != StateOfLoadScreen.Moving)
                {
                    usBeh.SetState(StateOfLoadScreen.Look);
                }
            }
            else
            {
                updateEmbient();
            }
        }
        
        if (point.GetInteractivCount() == IntObj.Count & !InteraktiveErect)
        {
            InteraktiveErect = true;
            spawnInteraction();
        }
    }

    private void chekigLists()
    {
        var newlist = new List<GameObject>();
        for (int i = 0; i < Points2Draw.Count; i++)
        {
            if (Points2Draw[i]!=null) {
                newlist.Add(Points2Draw[i]);
            }
        }
        Points2Draw = newlist;
        newlist = new List<GameObject>();
        for (int i = 0; i < IntObj.Count; i++)
        {
            if (IntObj[i] != null)
            {
                newlist.Add(IntObj[i]);
            }
        }
        IntObj = newlist;


        //IntObj     
    }

    public void setGM(GuideMaster _gm)
    {
        gm = _gm;
    }

    private void spawnInteraction()
    {
           
        var point = gm.GetPointInfo();
        var cheker = new Dictionary<string,bool>();

        for (int i = 0; i < IntObj.Count; i++)
        {
            IntObj[i] = Instantiate(IntObj[i]);
            IntObj[i].name = IntObj[i].name.Replace("(Clone)", "");

            var objName = IntObj[i].name;
            var intobj = point.GetObjByName(IntObj[i].name);

            for (int j = 0; j < point.interactiv.Length; j++)
            {
                if (!cheker.ContainsKey(point.interactiv[j].link) &
                    point.interactiv[j].name == IntObj[i].name
                    ) { 
                    switch (objName)
                    {
                        case "InfoTableMovi":
                            var osV = IntObj[i].GetComponent<InfoTableMoviBeh>();
                            osV.Link = point.interactiv[j].link;
                            break;
                        case "InfoTableImage":
                            var osI = IntObj[i].GetComponent<InfoTableImageBeh>();
                            osI.Link = point.interactiv[j].link;
                            break;
                    }
                    cheker.Add(point.interactiv[j].link,true);
                    IntObj[i].transform.position    = point.interactiv[j].getPos();
                    IntObj[i].transform.rotation    = point.interactiv[j].getRot();
                    IntObj[i].transform.localScale  = point.interactiv[j].getScl();
                    IntObj[i].transform.SetParent(transform);
                    break;
                }
            }

        }
        

    }

    private void updateEmbient()
    {
        foreach (var item in Points2Draw)
        {
            Destroy(item);
        }

        Points2Draw = new List<GameObject>();
       
        var point = gm.GetPointInfo();

        //print($"{point.skyBox}");
        Consoleum.DeveloperConsole.Instance.ParseInput($"sky {point.skyBox}");

        usBeh.SetState(StateOfLoadScreen.Loading);

        foreach (var p in point.conects)
        {
            var go = Instantiate(prefabPoint);
            var cb = go.AddComponent<ConnectBeh>();
            cb.MasterGuide  = gm;//this;
            cb.statement    = p;
            go.transform.SetParent(gameObject.transform);
            Points2Draw.Add(go);
            go.transform.position   = p.getPos();

            go.transform.rotation   = p.getRot();
            go.transform.localScale = p.getScl();

        }

        gMapbeh.changePlace(gm.GetCurrentPos(), gm.GetBuildInfo());

    }

    public void Moving(/*string[] args*/)
    {
        //if (args.Length==1) {
        //    CurrentPos      = args[0];
        //}
        //if (args.Length == 2)
        //{
        //    CurrentRoom     = args[0];
        //    CurrentPos      = args[1];
        //}
        //if (args.Length == 3)
        //{
        //    CurrentFloor    = int.Parse(args[0]) ;
        //    CurrentRoom     = args[1];
        //    CurrentPos      = args[2];
        //}
        //if (args.Length == 4)
        //{
        //   CurrentBuild     = args[0];
        //    CurrentFloor    = int.Parse(args[1]); 
        //    CurrentRoom     = args[2];
        //    CurrentPos      = args[3];
        //}
        InteraktiveErect = false;
        updateEmbient();
        updateEnvironment();
    }

    private void updateEnvironment()
    {
        if (IntObj.Count > 0) { 
            foreach (var item in IntObj)
            {
                Destroy(item);
            }
        }
        IntObj = new List<GameObject>();
        var point = gm.GetPointInfo();
        //var build = currentBuid.getFloorInBuildByName(CurrentBuild, CurrentFloor);
        //var point = build.getRoomByName(CurrentRoom)
        //                 .getPointByName(CurrentPos);
        if (point.GetInteractivCount()>0) {
            foreach (var item in point.interactiv)
            {
                Addressables.LoadAssetAsync<GameObject>(item.name).Completed += OnLoadAsset;
            }
        }
    }


    void OnLoadAsset(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var gerbObject = handle.Result;
            var w   = gerbObject.name.Replace("(Clone)","");
            gerbObject.name = w; 
            IntObj.Add(gerbObject);
        }

        if (handle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogWarning("Spawn object faild");
        }

    }
}