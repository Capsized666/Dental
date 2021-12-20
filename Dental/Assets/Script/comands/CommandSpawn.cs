using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;


namespace Consoleum
{
    public class CommandSpawn : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        Vector3 position = Vector3.zero;
        Vector3 rotation = Vector3.zero;
        string objN = "";
        string NameObj = "";



        public CommandSpawn()
        {
            Name = "Spawn";
            Command = "spawn";
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
            NameObj = args[0];

            for (int i = 0; i < args.Length; i++)
            {
                var argument = args[i];
                var argSplit = Regex.Split(argument,@"\=");

                switch (argSplit[0])
                {
                    case "xp":
                        position.x = float.Parse(argSplit[1]);
                        break;
                    case "yp":
                        position.y = float.Parse(argSplit[1]);
                        break;
                    case "zp":
                        position.z = float.Parse(argSplit[1]);
                        break;
                    case "xr":
                        rotation.x = float.Parse(argSplit[1]);
                        break;
                    case "yr":
                        rotation.y = float.Parse(argSplit[1]);
                        break;
                    case "zr":
                        rotation.z = float.Parse(argSplit[1]);
                        break;
                    case "objN":
                        objN = argSplit[1];
                        break;
                    
                }
            }
            
            //Debug.Log($"{args[0]} {NameObj}");
            Addressables.LoadAssetAsync<GameObject>(NameObj).Completed += OnLoadAsset;
        
        }

      



        void OnLoadAsset(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {

                //gameObjectsOnScene.Add(handle.Result);
                //gameObjectsOnScene[gameObjectsOnScene.Count - 1] = Instantiate(gameObjectsOnScene[gameObjectsOnScene.Count - 1], this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
                //gameObjectsOnScene[gameObjectsOnScene.Count - 1].name = handle.Result.name;
                var gerbObject = handle.Result;
                gerbObject = MonoBehaviour.Instantiate(gerbObject, position, Quaternion.Euler(rotation));
                gerbObject.name = objN == "" ? NameObj : objN;
                Comendant.Instance.AddObgetOnScene(gerbObject);
            }
            if (handle.Status == AsyncOperationStatus.Failed) {
                Debug.LogWarning("Spawn object faild");
            }
        }


        public static CommandSpawn CreateCommand()
        {
            return new CommandSpawn();
        }
    }
}




