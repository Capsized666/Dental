using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Consoleum;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.IO;

/// <summary>
/// Класс хранящий и реализующий взаимодействие с ними
/// </summary>

public class Comendant : MonoBehaviour
{
    public static Comendant Instance { get; private set; }

    public List<GameObject> Recvisit;

    public main curretstruct;

    public addressableGuidNameList adressList;
    public static string State = "ret";
    /// <summary>
    /// Добавление дочернего обьета
    /// </summary>
    /// <param name="go">Object for adding</param>
    public void AddObgetOnScene(GameObject go)
    {
        //Recvisit.Add(go);
        go.transform.SetParent(this.gameObject.transform);
        Recvisit = RollCall();
    }

    public string[] getAllObject() {
       var names = GetRecursChild(this.gameObject.transform,0);
       return names.ToArray();
    }
    
    private List<string> GetRecursChild(Transform t,int r)
    {
       List<string> ls = new List<string>();
       string[] folders = GetChildS(t,r);
        for (int i = 0; i < folders.Length; i++)
        {
           ls.Add(folders[i]+"|"+ r.ToString());
           ls.AddRange(GetRecursChild(t.GetChild(i),r+1));
        }

       return ls;
    }
    
    private string[] GetChildS(Transform t,int r)
    {
        var ret = new string[t.childCount];
        
        for (int i = 0; i < t.childCount; i++)
        {
            
            ret[i] = $"{tabs(r)}{t.GetChild(i).name}";
        }
        return ret;
    }

    private string tabs(int r) {
        var s = "";
        for (int i = 0; i < r; i++)
        {
            s += "\t";
        }
        return s;
    }

    private List<GameObject> RollCall() {
        return GetRecursGO(this.gameObject.transform);
    }

    private List<GameObject> GetRecursGO(Transform t)
    {
        var ls =new List<GameObject>();

        foreach (Transform item in t)
        {
            ls.Add(item.gameObject);
            ls.AddRange(GetRecursGO(item.transform));
        }

        return ls;
    }
    /// <summary>
    /// Print name of All Obj on Scene 
    /// </summary>
    /// <returns></returns>
    public string[] getOOSNames() {
        Recvisit = RollCall();
        string[] sr = new string[Recvisit.Count];
        var i = 0;
        foreach (var item in Recvisit)
        {
            sr[i] = item.name;
            i++;
        }
        return sr;
    }




    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
        Recvisit = new List<GameObject>();
        curretstruct = new main();
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
    private void loadArdessable(out addressableGuidNameList adressList)
    {
        var path = Path.Combine(Application.dataPath + "/Resources", "assetTable.json");
        FileStream fileStream = new FileStream(path,
                          FileMode.OpenOrCreate,
                          FileAccess.ReadWrite,
                          FileShare.None);
        string s = "";
        if (fileStream.CanRead)
        {
            byte[] arr = new byte[fileStream.Length];
            fileStream.Read(arr, 0, arr.Length);
            s = System.Text.Encoding.Default.GetString(arr);
            fileStream.Close();
        }
        adressList = JsonUtility.FromJson<addressableGuidNameList>(s);
    }
    IEnumerator LoadLost()
    {

        if (curretstruct.objectList.Length > Recvisit.Count)
        {

            for (int i = 0; i < curretstruct.objectList.Length; i++)
            {
                if (adressList.canBeLoaded(curretstruct.objectList[i].name))
                {
                    //UpdateScene();
                    //AsyncOperationHandle asynAction = new AsyncOperationHandle();
                    var asynAction = Addressables.LoadAssetAsync<GameObject>
                        (curretstruct.objectList[i].name);
                    curretstruct.objectList[i].HashCode = asynAction.GetHashCode();
                    //print(asynAction.GetHashCode()+" v");
                    asynAction.Completed += OnLoadAsset;

                    /*
                    loadScreen.setProgress(Recvisit.Count,
                                            curretstruct.objectList.Length);
                     */
                    yield return new WaitForSecondsRealtime(.4f);
                }
            }
        }


        yield break;
        //yield return null;
    }


    void OnLoadAsset(AsyncOperationHandle<GameObject> handle)
    {
        //print(handle.GetHashCode() +  " w");
        switch (handle.Status)
        {
            case AsyncOperationStatus.None:
                break;
            case AsyncOperationStatus.Succeeded:

                var spawnObject = handle.Result;

                spawnObject = Instantiate(spawnObject);
                spawnObject.name = spawnObject.name.Replace("(Clone)", "");

                Vector3 p, s = Vector3.zero;
                Quaternion r = Quaternion.identity;


                var cur = curretstruct.getByHashCode(handle.GetHashCode());
                cur.getPos(out p, out r, out s);
                spawnObject.transform.position = p;
                spawnObject.transform.rotation = r;
                spawnObject.transform.localScale = s;
                Recvisit.Add(spawnObject);


                if (cur.parent != "root")
                {
                    var par = curretstruct.findParent(cur.name, cur.parent);
                    spawnObject.transform.SetParent(findParent(par));
                }

                break;
            case AsyncOperationStatus.Failed:
                Debug.LogWarning("Spawn object faild");
                break;
        }
        Addressables.Release(handle);
    }
    Transform findParent(obj4load go)
    {
        foreach (var item in Recvisit)
        {
            if (go.compareTransform(item))
            {

                return item.transform;

            }
        }
        return null;
    }
}
