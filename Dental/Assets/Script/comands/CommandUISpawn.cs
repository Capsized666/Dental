using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;


namespace Consoleum
{
    public class CommandUISpawn : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }



        //RectTransform rt = new RectTransform();
        Vector2 ancMin= Vector2.zero;
        Vector2 ancMax = Vector2.zero;
        string objN = "";
        string NameObj = "";
        

        //UIspawn Image alx=0,19 aly=0,19 ahx=0,81 ahy=0,81 objN=Menu
        public CommandUISpawn()
        {
            Name = "Spawn UI element";
            Command = "UIspawn";
            Description = "Spawn objects from adressable system";
            Help = "Options :\n" +
                    "Necessary   : <NameObj> where <NameObj> is string name of object in Addressable system\n" +
                    "NonNecessary: xp=<value> where <value> is float of the x coordinate\n" +
                    "            : yp=<value> where <value> is float of the y coordinate\n" +
                    "            : zp=<value> where <value> is float of the z coordinate\n" +
                    "            : xr=<value> where <value> is float of the x rotation\n" +
                    "            : yr=<value> where <value> is float of the y rotation\n" +
                    "            : zr=<value> where <value> is float of the z rotation\n" +
                    "            : objN=<value> where <value> is string for naming object\n";// +

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                var argument = args[i];
                var argSplit = Regex.Split(argument, @"\=");

                switch (argSplit[0])
                {
                    case "alx":
                        ancMin = new Vector2(float.Parse(argSplit[1]), ancMin.y);
                        break;
                    case "ahx":
                        ancMax = new Vector2(float.Parse(argSplit[1]), ancMax.y);
                        break;
                    case "aly":
                        ancMin = new Vector2(ancMin.x, float.Parse(argSplit[1]));
                        break;
                    case "ahy":
                        ancMax = new Vector2(ancMax.x, float.Parse(argSplit[1]));
                        break;
                    case "objN":
                        objN = argSplit[1];
                        break;
                    default:
                        NameObj = argSplit[0];
                        break;



                }
            }
            Addressables.LoadAssetAsync<GameObject>(NameObj).Completed += OnLoadAsset;
            //try
            //{
            //}
            //
            //catch (Exception e)
            //{
            //    Debug.LogWarning("Spawn object faild\nChek name of object");
            //}


        }

        void OnLoadAsset(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {

                //gameObjectsOnScene.Add(handle.Result);
                //gameObjectsOnScene[gameObjectsOnScene.Count - 1] = Instantiate(gameObjectsOnScene[gameObjectsOnScene.Count - 1], this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
                //gameObjectsOnScene[gameObjectsOnScene.Count - 1].name = handle.Result.name;
                var gerbObject = handle.Result;
                gerbObject = MonoBehaviour.Instantiate(gerbObject);

                gerbObject.GetComponent<RectTransform>().anchorMin= ancMin;
                gerbObject.GetComponent<RectTransform>().anchorMax= ancMax;
                


                gerbObject.name = objN == "" ? NameObj : objN;
                MCUI.Instance.AddObgetOnScene(gerbObject);
            }
            if (handle.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogWarning("Spawn object faild");
            }
        }

        public static CommandUISpawn CreateCommand()
        {
            return new CommandUISpawn();
        }
    }
}




