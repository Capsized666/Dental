using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;



namespace Consoleum
{
    public class CommandSky : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        //string objN = "";
        //string NameObj = "";

        public CommandSky()
        {
            Name = "Skybox";
            Command = "sky";
            Description = "Change sky box";
            Help = "sky <material Name>";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            if (args[0]=="?") {
                Debug.Log($"{UnityEngine.RenderSettings.skybox.name}"); 
                return;
            }
            Addressables.LoadAssetAsync<Material>(args[0]).Completed += OnLoadAsset;
        }


        void OnLoadAsset(AsyncOperationHandle<Material> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var gerbObject = handle.Result;
                UnityEngine.RenderSettings.skybox = gerbObject;
            }
            if (handle.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogWarning("Spawn object faild");
            }
        }
        public static CommandSky CreateCommand()
        {
            return new CommandSky();
        }
    }
}
