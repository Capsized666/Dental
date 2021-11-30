using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

[Serializable]
public struct obj4load {
    public string name;
    public float[] position;
    public float[] rotation;
    public float[] scale;
    public string parent;
    public string[] child;

    public void setPos(Transform t) {
        position = new float[3]{
                t.position.x,
                t.position.y,
                t.position.z
                };
        rotation = new float[4]{
                t.rotation.w,
                t.rotation.x,
                t.rotation.y,
                t.rotation.z
                };
        scale = new float[3] {
                t.lossyScale.x,
                t.lossyScale.y,
                t.lossyScale.z
                };
        if (t.parent == null)
        {
            parent = "root";
        }
        else {
            parent = t.parent.name;
        }
        if (t.childCount != 0)
        {
            child = new string[t.childCount];
            for (int i = 0; i < t.childCount; i++)
            {
                child[i]= t.GetChild(i).gameObject.name;            
            }
        }

    }
    public void getPos(out Vector3 p, out Quaternion r, out Vector3 s)
    {
        p = new Vector3(position[0],position[1],position[2]);
        r = new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
        s = new Vector3(scale[0], scale[1], scale[2]);
    }
    public void empty() {
        position = new float[3]{
                0,
                0,
                0
                };
        var q = Quaternion.identity;
        rotation = new float[4]{
                q.w,
                q.x,
                q.y,
                q.z
                };
        scale = new float[3] {
                1,
                1,
                1
                };
        parent = "root";
        child = null;
        name = "empty";
    }
}
[Serializable]
public struct main {
    public obj4load[] objectList;
    public obj4load getByName(string name) {
        
        foreach (var item in objectList)
        {
            if (name==item.name)
            {
                return item;
            }    
        }
        obj4load ver = new obj4load();
        ver.empty();
        return ver;
    }
} 

public sealed class Comendant : MonoBehaviour
{
    bool guiOn;
    Scene scene;
    public static Comendant Instance;
    public TextAsset IgnoreList;
    public string State;
    public string nameOfFile;

    public List<GameObject> Recvisit = new List<GameObject>();
    public main curretstruct;
    public main bufferstruct;

    string[] ignore; 
    void Awake()
     {
        curretstruct = new main();
        if (Instance != this)
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
     }
    void OnEnable()
    {
        guiOn = true;
        var cam = FindObjectsOfType(typeof(Comendant));
        foreach (Comendant item in cam)
        {
            if (item!= Instance)
            {
                DestroyImmediate(item.gameObject);
            }
        }
        initState();
        //StartCoroutine(LoadLost());
    }


    IEnumerator LoadLost() {
        //yield return null;
        
        if (curretstruct.objectList.Length> bufferstruct.objectList.Length)
        {
            foreach (var item in curretstruct.objectList)
            {
                if (item.name == Recvisit.)
                {

                }
            }
        }
        if (curretstruct.objectList.Length == bufferstruct.objectList.Length)
        {

            StopCoroutine(LoadLost());
        }

        yield return new WaitForEndOfFrame();
    }

    void FixedUpdate()
    {
        if (scene!= SceneManager.GetActiveScene())
        {
            initState();  
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateScene();
            writeJson();
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            guiOn = true != guiOn ? true : false;
        }
            
    }
    void OnGUI()
    {
        if (guiOn)
        {
            GUI.skin.button.fontSize = 20;

            if (GUI.Button(new Rect(10, 80, 380, 60), "Next scene" + scene.buildIndex))
            {
                //int nextSceneIndex = UnityEngine.Random.Range(0, 4);
                SceneManager.LoadScene(
                    SceneManager.sceneCountInBuildSettings != scene.buildIndex + 1 ? scene.buildIndex + 1 : 0
                    , LoadSceneMode.Single);
            }

            if (GUI.Button(new Rect(10, 140, 380, 60), "Load " + State))
            {
                try
                {
                    Addressables.LoadAssetAsync<GameObject>(State).Completed += OnLoadAsset;
                }
                catch (Exception e)
                {
                    print(e);
                }

            }
        }   
    }


    
    private void writeJson()
    {
        main resurseList = new main();
        List<obj4load> fill = new List<obj4load>();
        foreach (var item in Recvisit)
        {
            obj4load nobj = new obj4load();
            nobj.name = item.name;
            nobj.setPos(item.transform);
            fill.Add(nobj);
        }

        fill = sortArray(fill); 
        resurseList.objectList =  fill.ToArray();

        var path = Path.Combine(Application.dataPath+ "/Resources", nameOfFile);
        var s = JsonUtility.ToJson(resurseList, true);
        
        try
        {
            File.Delete(path);
            FileStream fileStream = new FileStream(path,
                                       FileMode.OpenOrCreate,
                                       FileAccess.ReadWrite,
                                       FileShare.None);
            if (fileStream.CanWrite)
            {
                byte[] arr = System.Text.Encoding.Default.GetBytes(s);
                fileStream.Write(arr, 0, arr.Length);
            }
            fileStream.Close();
        }
        catch (System.Exception e)
        {
            print($"Pizda togo sho {e.ToString()}");
        }
            
            

            //File.WriteAllText(path, s);

    }
    private List<obj4load> sortArray( List<obj4load> objOld) {
        List<obj4load> objNew = new List<obj4load>();
        for (int i = 0; i < objOld.Count; i++)
        {
            if (objOld[i].parent=="root")
            {
                objNew.Add(objOld[i]);
            }
        }
        for (int i = 0; i < objOld.Count; i++)
        {
            if (objOld[i].parent != "root")
            {
                objNew.Add(objOld[i]);
            }
        }

        
        return objNew;
    }
    private void setIgnorlist()
    {
        var p=IgnoreList.text;
        ignore = p.Split('\n');
    }
    private void UpdateScene()
    {
        Recvisit = new List<GameObject>();
        ChekScene();
    }
    private void initState()
    {
        setIgnorlist();
        UpdateScene(); 
        scene = SceneManager.GetActiveScene(); 

        nameOfFile =$"data_{scene.name}";
        nameOfFile +=".json";
        var path = Path.Combine(Application.dataPath + "/Resources", nameOfFile);

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

        curretstruct = JsonUtility.FromJson<main>(s);
        bufferstruct = new main();
    }
    private void ChekScene()
    {
        var foundObjects = FindObjectsOfType<GameObject>();
        foreach (var item in foundObjects)
        {
           if (chekFamilyTree(item))
           {
               Recvisit.Add(item);
           }
        }
    }
            //if (ChekIgnore(item.name))
            //{
            //}

    private bool chekFamilyTree(GameObject item)
    {
        if (item.transform.parent!=null)
        {
            return chekFamilyTree(item.transform.parent.gameObject);
            //return false;
        }
        else
        {
            return ChekIgnore(item.name);
        }
        return true;
    }

    private bool ChekIgnore(string s) {
        for (int i = 0; i < ignore.Length; i++)
        {
            if (ignore[i].Contains(s))
            {
                return false;
            }

        }
        return true;
    }
    Transform findParent(string go) {
        foreach (var item in Recvisit)
        {
            if (item.name == go)
            {
                return item.transform;
            }
        }
        return null;
    } 
    void OnLoadAsset(AsyncOperationHandle<GameObject> handle)
    {
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

                curretstruct.getByName(spawnObject.name).getPos(out p,out r,out s);
                if (curretstruct.getByName(spawnObject.name).name!="empty"||
                    curretstruct.getByName(spawnObject.name).parent == "root")
                {
                    findParent(
                    curretstruct.getByName(spawnObject.name).parent
                        );
                }
                spawnObject.transform.position      = p;
                spawnObject.transform.rotation      = r;
                spawnObject.transform.localScale    = s;
                break;
            case AsyncOperationStatus.Failed:
                Debug.LogWarning("Spawn object faild");
                break;
        }
    }
        





            

    /*
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var spawnObject = handle.Result;
                var obj = obj4load.GetObjByNam(spawnObject.name);

            
                spawnObject.transform.position      = data4Load.GetPosition(spawnObject.name);
                spawnObject.transform.rotation      = data4Load.GetRotation(spawnObject.name);
                spawnObject.transform.localScale    = data4Load.GetScale(spawnObject.name);
                spawnObject.transform.SetParent(getParent(obj.parent));
            }
            if (handle.Status == AsyncOperationStatus.Failed) {
                Debug.LogWarning("Spawn object faild");
            }
    }
    Transform getParent(string s)
    {
        //print(s);
        foreach (Transform item in LoadPlace.transform)
        {
            if (item.gameObject.name == s)
            {
                return item;
            }
        }
        return null;
    }
    */
}
 
/*
 Addressables.LoadAssetAsync<GameObject>(item.name).Completed += OnLoadAsset;
 
 
 */