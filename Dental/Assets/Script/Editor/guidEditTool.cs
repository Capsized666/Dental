using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;

[CustomEditor(typeof(EditTool))]
[CanEditMultipleObjects]
public class guidEditTool : Editor
{
    private EditTool ET;

    public void OnEnable()
    {
        ET = (EditTool)target;


    }



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUI.changed & !Application.isPlaying)
        {
            SetObjectDirty(ET.gameObject);
        }
        EditorGUILayout.LabelField($"Rubilovo");

        //EditorGUILayout.LabelField($"Zdanie\t\t {ET.CurrentBuild}\t Etag\t\t {ET.CurrentFloor}");
        //EditorGUILayout.LabelField($"Comnata\t {ET.CurrentRoom}\t Positciya\t {ET.CurrentPos }");

        OptionsButton();


    }



    void OptionsButton()
    {
        if (Application.isPlaying)
        {

        EditorGUILayout.BeginVertical("box");
        if (GUILayout.Button("Load Objects", GUILayout.Height(30)))
        {
            
        }

        if (GUILayout.Button("Write Objects", GUILayout.Height(30)))
        {
            ET.WriteObjects();
            //ET.AddInteractImage();
        }
        EditorGUILayout.EndVertical();



        /*
            EditorGUILayout.BeginHorizontal("box");


            EditorGUILayout.BeginVertical("box");
            if (GUILayout.Button("Add connect", GUILayout.Height(30)))
            {

                //variats0 = !variats0;
               // ET.AddConection();
            }
            if (GUILayout.Button("Add position", GUILayout.Height(30)))
            {


               // ET.AddPosition();
            }

            EditorGUILayout.BeginVertical("box");
            if (GUILayout.Button("Add room", GUILayout.Height(30)))
            {
                //variats2 = true ? false : true;
              //  ET.AddRoom();
            }
            if (GUILayout.Button("Add flor & building", GUILayout.Height(30)))
            {
              //  ET.AddFB();
                //variats3 = true ? false : true;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal("box");

            if (GUILayout.Button("Update", GUILayout.Height(30)))
            {
               // ET.SavePos();
            }
            if (GUILayout.Button("WriteToJson", GUILayout.Height(30)))
            {
               // ET.JsonWrite();
            }
            if (GUILayout.Button("Move", GUILayout.Height(30)))
            {
                
                ET.Moving(new string[4] {
                    ET.CurrentBuild,
                    ET.CurrentFloor.ToString(),
                    ET.CurrentRoom,
                    ET.CurrentPos
                }
                    );
                 
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        */
        }
        if (Application.isEditor)
        {

        }
    }



    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }

}
