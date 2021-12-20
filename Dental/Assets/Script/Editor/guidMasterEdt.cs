using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;

[CustomEditor(typeof(GuideMaster))]

public class guidMasterEdt : Editor
{

    private GuideMaster GB;

    public void OnEnable()
    {
        GB = (GuideMaster)target;
       

    }



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUI.changed & !Application.isPlaying)
        {
            SetObjectDirty(GB.gameObject);
        }


        EditorGUILayout.LabelField($"==Tekushee Mesto==");
        EditorGUILayout.LabelField($"Zdanie\t\t {GB.CurrentBuild}\t Etag\t\t {GB.CurrentFloor}");
        EditorGUILayout.LabelField($"Comnata\t {GB.CurrentRoom}\t Positciya\t {GB.CurrentPos }");

        OptionsButton();


    }



    void OptionsButton() {
        if (Application.isPlaying )
        {

        
            EditorGUILayout.BeginVertical("box");
            if (GUILayout.Button("Add InfoTableImage", GUILayout.Height(30)))
            {


              
               GB.AddInteractImage();
            }
            if (GUILayout.Button("Add InfoTableMovi", GUILayout.Height(30)))
            {


               GB.AddInteractVideo();
            }

            EditorGUILayout.BeginHorizontal("box");


            EditorGUILayout.BeginVertical("box");
            if (GUILayout.Button("Add connect", GUILayout.Height(30)))
            {

                //variats0 = !variats0;
                GB.AddConection();
            }
            if (GUILayout.Button("Add position", GUILayout.Height(30)))
            {

            
                GB.AddPosition();
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box");
            if (GUILayout.Button("Add room", GUILayout.Height(30)))
            {
                //variats2 = true ? false : true;
                GB.AddRoom();
            }
            if (GUILayout.Button("Add flor & building", GUILayout.Height(30)))
            {
                GB.AddFB();
                //variats3 = true ? false : true;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal("box");

            if (GUILayout.Button("Update", GUILayout.Height(30)))
            {
                GB.SavePos();
            }
            if (  GUILayout.Button("WriteToJson", GUILayout.Height(30)))
            {
                GB.JsonWrite();
            }
            if (GUILayout.Button("Move", GUILayout.Height(30)))
            {
                GB.Moving(new string[4] { 
                    GB.CurrentBuild,
                    GB.CurrentFloor.ToString(),
                    GB.CurrentRoom,
                    GB.CurrentPos
                }
                    );
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }



    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }

}