using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public enum scenes { 
    MainMenu,
    Cabinet
}

public sealed class ComendantOnScene : MonoBehaviour
{
    bool guiOn;
    Scene scene;
    LoadScreen loadScreen;
    int startCount = 0;
    string[] ignore;
    //public IEnumerator coroutine;

    public static ComendantOnScene Instance;

    [Space]
    public Scene[] curentScenes;
    public TextAsset IgnoreList;
    [SerializeField]
    public static string State { get; private set; } = "ret";
    public string nameOfFile;
    [Space]
    public List<GameObject> Recvisit = new List<GameObject>();
    public main curretstruct;
    //public main bufferstruct;
    public addressableGuidNameList adressList;
    void Awake()
    {
        loadArdessable(out adressList);
        //coroutine = LoadLost();
        curretstruct = new main();
        if (Instance != this)
        {
            Instance = this;
        }
        loadScreen = GetComponentInChildren<LoadScreen>();
        DontDestroyOnLoad(this);
    }

    void OnEnable()
    {
        guiOn = false;
        initState();
        startCount = Recvisit.Count;
        if (loadScreen!=null)
        {
            loadScreen.setProgress(0, 1);
        }
        Time.timeScale = Time.timeScale != 1 ? 1 : Time.timeScale;
        StartCoroutine(LoadLost());
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
                    //print(asynAction.GetHashCode()+" v");

                    var asynAction = Addressables.LoadAssetAsync<GameObject>
                        (curretstruct.objectList[i].name);
                    curretstruct.objectList[i].HashCode = asynAction.GetHashCode();
                    asynAction.Completed += OnLoadAsset;
                    loadScreen.setProgress(Recvisit.Count,
                                            curretstruct.objectList.Length);
                    yield return new WaitForSecondsRealtime(.4f);
                    
                }

            }
        }
        yield break;
        //yield return null;
    }
    void FixedUpdate()
    {
        if (curretstruct.objectList.Length == Recvisit.Count)
        {
            loadScreen.setProgress(1, 1);
            loadScreen.setHide(true);
            
        }
        if (scene != SceneManager.GetActiveScene())
        {
            initState();
        }
#if UNITY_EDITOR
        EditorWork();
#endif


    }

#if UNITY_EDITOR
    private void EditorWork()
    {
        if (curentScenes.Length != SceneManager.sceneCount)//GetAllScenes().Length)
        {
            curentScenes = SceneManager.GetAllScenes();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            print("Writing Json");
            UpdateScene();
            writeJson();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StopAllCoroutines();
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

            if (GUI.Button(new Rect(10, 80, 380, 60), $"It is {scene.buildIndex}. Next scene? "))
            {
                //int nextSceneIndex = UnityEngine.Random.Range(0, 4);
                //State = $"{SceneManager.sceneCountInBuildSettings}";

                SceneManager.LoadScene(
                    SceneManager.sceneCountInBuildSettings == scene.buildIndex + 1 ? 0 : scene.buildIndex + 1
                    , LoadSceneMode.Single);//); 
            }
            if (GUI.Button(new Rect(10, 140, 380, 60), $"It is {scene.buildIndex}.Add Next scene? "))
            {
                //int nextSceneIndex = UnityEngine.Random.Range(0, 4);
                State = $"{SceneManager.sceneCountInBuildSettings}";

                SceneManager.LoadScene(
                    SceneManager.sceneCountInBuildSettings == scene.buildIndex + 1 ? 0 : scene.buildIndex + 1
                    , LoadSceneMode.Additive);//Single); 
            }
            if (GUI.Button(new Rect(10, 200, 380, 60), "Load " + State))
            {
                try
                {
                    State = $"{scene.buildIndex} of {SceneManager.sceneCountInBuildSettings}";

                }
                catch (Exception e)
                {
                    print(e);
                }

            }
        }
    }
#endif
    public void setState(string s) {
        State = s;
    }
    private void writeJson()
    {
        main resurseList = new main();
        List<obj4load> fill = new List<obj4load>();
        foreach (var item in Recvisit)
        {
            obj4load nobj = new obj4load();
            //nobj.name = item.name;
            nobj.fillFields(item.transform);
            fill.Add(nobj);
        }

        fill = sortArray(fill);
        resurseList.objectList = fill.ToArray();

        var path = Path.Combine(Application.dataPath + "/Resources", nameOfFile);
        var s = JsonUtility.ToJson(resurseList, true);

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

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
    }
    private List<obj4load> sortArray(List<obj4load> objOld)
    {
        List<obj4load> objNew = new List<obj4load>();
        for (int i = 0; i < objOld.Count; i++)
        {
            if (objOld[i].parent == "root")
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
        var p = IgnoreList.text;
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

        nameOfFile = $"data_{scene.name}";
        nameOfFile += ".json";
        if (scene.name == "Cabinet")
        {
            nameOfFile = $"data_{scene.name}_{State}";
            nameOfFile += ".json";
        }

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
        try
        {
            curretstruct = JsonUtility.FromJson<main>(s);
        }

        catch (Exception)
        {
            print($"eBATY");

        }
        //bufferstruct = new main();
    }
    private void ChekScene()
    {
        var fObjects = FindObjectsOfType<GameObject>();
        foreach (var item in fObjects)
        {
            var chek = item.GetComponent<ComendantOnScene>();
            if (chek != null & chek != Instance)
            {
                DestroyImmediate(item);
                break;
            }
        }
        var foundObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var item in foundObjects)
        {
            if (ChekIgnore(item.name))
            {
                Recvisit.Add(item);
                Recvisit.AddRange(getAllChildren(item));
            }
        }
    }
    private List<GameObject> getAllChildren(GameObject go) {
        var res = new List<GameObject>();
        
        for (int i = 0; i < go.transform.childCount; i++)
        {
            res.Add(go.transform.GetChild(i).gameObject);
            res.AddRange(getAllChildren(go.transform.GetChild(i).gameObject));
        }
        return res;
    }

    private bool chekFamilyTree(GameObject item)
    {
        if (item.transform.parent != null)
        {
            return chekFamilyTree(item.transform.parent.gameObject);
        }
        else
        {
            return ChekIgnore(item.name);
        }
    }
    private bool ChekIgnore(string s)
    {
        for (int i = 0; i < ignore.Length; i++)
        {
            if (ignore[i].Contains(s))
            {
                return false;
            }
        }
        return true;
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
    public void loadScene(scenes s)
    {
        SceneManager.LoadScene(s.ToString(), LoadSceneMode.Single);
    }
}