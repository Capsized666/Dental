using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InfoTableImageBeh : MonoBehaviour
{

    public string Link = "";
    
    public Sprite Image2Show;
    private bool isLoadet = false;



    void OnMouseDown()
    {
        
        var usb = FindObjectOfType<UniversalScreenBeh>();

        if (usb != null & Image2Show != null)
        {
            //
            //print($"Image KLK {Link}");
            //
            usb.SetSprate(Image2Show);
            usb.SetMovingState(StateOfMoving.Image);
            usb.SetState(StateOfLoadScreen.Moving);


        }

    }

    void Awake()
    {
        
    }

    private void Start()
    {
       
            Addressables.LoadAssetAsync<Sprite>(Link).Completed += OnLoadAsset;
    }

    void Update()
    {
        if (Image2Show == null & Link != "" & !isLoadet)
        {
            isLoadet = true;
        }
    }

    void OnLoadAsset(AsyncOperationHandle<Sprite> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Image2Show = handle.Result;
        }
        if (handle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogWarning("Spawn object faild");
            print(Link);
        }

    }

    

}
