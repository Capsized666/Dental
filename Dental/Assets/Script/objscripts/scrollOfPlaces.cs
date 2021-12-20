using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class placeList {
    public Places[] places;

}
[System.Serializable]
public class Places
{
    public string Name;
    public Variant[] variant;

}
[System.Serializable]
public class Variant
{
    public string ua;
    public string ru;
    public string en;
}


public class scrollOfPlaces : MonoBehaviour
{
    public placeList currentPuck = new placeList();
    public TextAsset langPack;
    RectTransform rt;
    RectTransform parentRT;
    int ChildNumber;
    GuideMaster MasterGuide;

    public GameObject prefab;
    public GameObject content;


    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        currentPuck = JsonUtility.FromJson<placeList>(langPack.text);
    }
       
    void Start()
    {
        parentRT = gameObject.transform.parent.GetComponentInParent<RectTransform>();
        MasterGuide = FindObjectOfType<GuideMaster>();
        EraseChild();
        mListOfPlaces();
    }


    private void EraseChild()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }
       
    public void mListOfPlaces()
    {
        var allbuild = MasterGuide.currentBuid.getAllBuilding();
        foreach (var item in allbuild)
        {
            var flooars = MasterGuide.currentBuid.getAllFloarInBuilding(item);
            foreach (var floar in flooars)
            {
                var buildflor = MasterGuide.currentBuid.getFloorInBuildByName(item, int.Parse(floar));
                var roomz = buildflor.getAllRooms();
                foreach (var room in roomz)
                {
                    var plases = buildflor.getRoomByName(room).getAllPoint();
                    foreach (var pls in plases)
                    {
                        //print($"{item}/{floar}/{room}/{pls}");
                        var go = Instantiate(prefab);
                        var ifs = go.GetComponent<ItemForScroll>();
                        ifs.setGM(MasterGuide);//this;
                        ifs.setStatement(new string[4] { item ,floar, room ,pls });
                        var str = $"{item}-{floar}\n{room}-{pls}";
                        ifs.setName(str);
                        go.transform.SetParent(content.transform);

                    }
                }
            }
        }
    }


    void Update()
    {
        ChildNumber = rt.GetSiblingIndex();
    }
    void LateUpdate()
    {
        rectCalc();
    }
    private void rectCalc()
    {
        rt.sizeDelta = new Vector2(parentRT.sizeDelta.x * 0.95f, (parentRT.sizeDelta.y - 50) * 0.97f);
        rt.anchoredPosition = new Vector2(0, -(ChildNumber) * (rt.sizeDelta.y * 1.02f) - 57);
        if (ChildNumber != rt.GetSiblingIndex())
        {
            ChildNumber = rt.GetSiblingIndex();
        }
    }
}