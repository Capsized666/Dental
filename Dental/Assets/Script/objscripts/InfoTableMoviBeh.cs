using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InfoTableMoviBeh : MonoBehaviour
{
    public string Link = "";

    public VideoClip Movi2Show;
    //private bool isLoadet=false;


    void OnMouseDown()
    {
        var usb = FindObjectOfType<UniversalScreenBeh>();
        var vpl = FindObjectOfType<VideoPlayerBeh>();
        if (usb != null & Movi2Show != null)
        {
            //
            //print($"Vides KLK {Link}");
            //
            //usb.SetSprate(Image2Show);
            usb.SetMovingState(StateOfMoving.Movi);
            usb.SetState(StateOfLoadScreen.Moving);
            vpl.LoadVideo(Movi2Show);
            vpl.PlayVideo();


        }

    }



    // Start is called before the first frame update
    void Start()
    {
            Addressables.LoadAssetAsync<VideoClip>(Link).Completed += OnLoadAsset;
    }
        

    // Update is called once per frame
    void Update()
    {

    }



    void OnLoadAsset(AsyncOperationHandle<VideoClip> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Movi2Show = handle.Result;
        }
        if (handle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogWarning("Spawn object faild");
        }

    }
}
            

/*
 
 
 */