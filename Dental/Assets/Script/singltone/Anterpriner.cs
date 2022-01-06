using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Consoleum;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.IO;
/// <summary>
/// Класс запускающий консольные команды из скрипта
/// </summary>


public class Anterpriner : MonoBehaviour
    {
        public static Anterpriner Instance { get; private set; }
        public static string State = "_null";
        
    
        [SerializeField]
        private TextAsset startList;
        [SerializeField]
        private TextAsset todoList;
        private string[] listOfOperation;

        [SerializeField]
        public bool WS { get { return workingState; }  set { workingState = value; } }
        private bool workingState=false;

        private Display[] displays;
        public Display[] Displays { get { return displays; } set { displays = value; } }    

        private void Awake()
        {
            if (Instance != null)
            {
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            readlist(0);
            Displays=Display.displays;
        }

        private void OnEnable()
        {
            
        }

        void Start()
        {
            
        }
        void FixedUpdate()
        {

            DoComandList(workingState);
            
        }

        private void DoComandList(bool wS)
        {
            if (wS)
            {
                if (listOfOperation[0] != null)
                {
                    DeveloperConsole.Instance.ParseInput(listOfOperation[0]);
                    print(listOfOperation[0]);
                    var midlist = MakeFirstStep(listOfOperation);
                    listOfOperation = new string[midlist.Length];
                    listOfOperation = midlist;
                    return;
                }
            }
        }

        private string[] MakeFirstStep(string[] loo)
        {
            string[] output = new string[loo.Length];
            for (int i = 1; i < output.Length; i++)
            {
                output[i - 1] = loo[i];
            }
            return output;
        }
        private void readlist(int v)
        {
            if (v == 0)
            {
                listOfOperation = startList.text.Split('\n');
            }
            else
            {
                listOfOperation = todoList.text.Split('\n');
            }
        }

        private void readFile(string[] args) {

        if (args.Length==1)
        {
            var path = Path.Combine(Application.dataPath , args[0]);
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
            todoList = new TextAsset(s);
        }

    }

        private void writeFile(string[] args)
        {
            
            if (args.Length==2)
            {
                var path = Path.Combine(Application.dataPath + args[0]);
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
                        byte[] arr = System.Text.Encoding.Default.GetBytes(args[1]);
                        fileStream.Write(arr, 0, arr.Length);
                    }
                    fileStream.Close();
                }
                catch (System.Exception e)
                {
                    print($"Pizda togo sho {e.ToString()}");
                }

            }
  
        }




    /*
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


    */



}

                    
                


        



