using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OperatorBeh : MonoBehaviour
{
    public static OperatorBeh Instance;


    List<Camera> eyeList = new List<Camera>();
    int countDisp;
    List<Display> showList = new List<Display>();
    List<AudioListener> earList = new List<AudioListener>();
    


    private void Awake()
    {
        Instance = this;
        updateLists();
    }

    private void updateLists()
    {
        eyeList = new List<Camera>();
        showList = new List<Display>();
        earList = new List<AudioListener>();

        for (int i = 0; i < Camera.allCamerasCount; i++)
        {
            eyeList.Add(Camera.allCameras[i]);
        }
        for (int i = 0; i < Display.displays.Length; i++)
        {
            showList.Add(Display.displays[i]);
        }
        countDisp= showList.Count;
        var foundObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var item in foundObjects)
        {
            if (containAudio(item)!=null)
            {
             earList.Add(containAudio(item));
            }

        }

    }

    AudioListener containAudio(GameObject g) {
        if (g.transform.childCount>0)
        {
            for (int i = 0; i < g.transform.childCount; i++)
            {
                containAudio(g.transform.GetChild(i).gameObject);
            }
        }

        return g.GetComponent<AudioListener>();
    }




    void Start()
    {

    }
    void Update()
    {
        updateLists();
    }
}
