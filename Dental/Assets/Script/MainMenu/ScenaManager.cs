using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.SceneManagement;
using System;

public enum gamestate
{
    moving,
    asking,
    notes
}
public sealed class ScenaManager : MonoBehaviour
{
    public static ScenaManager Instance;
    public Scene previosScene;
    public Scene currentScene;
    public SceneInstance loadScene;
    public event Action onChangeScene; 

    public gamestate currentState { get; set; } = gamestate.moving;
    bool clearScene = false;

    private void Awake()
    {
        
            if (Instance == null)
            { // Ёкземпл€р менеджера был найден
                Instance = this; // «адаем ссылку на экземпл€р объекта
            }
            else if (Instance == this)
            { // Ёкземпл€р объекта уже существует на сцене
                Destroy(gameObject); // ”дал€ем объект
            }
            DontDestroyOnLoad(this);
        currentState = gamestate.moving;
        previosScene = SceneManager.GetActiveScene();
        currentScene = SceneManager.GetActiveScene();
        onChangeScene += OnEnable;
        //print("Der Eskapist A");
    }
    void OnEnable()
    {
        ChekScene();
    }
        

    private void ChekScene()
    {
        //kill duplikat
        var foundObjects = FindObjectsOfType<GameObject>();
        foreach (var item in foundObjects)
        {
            var chek = item.GetComponent<ScenaManager>();
            if (chek != null & chek != Instance)
            {
                DestroyImmediate(item);
                break;
            }
        }
       // print("Der Eskapist E");
        //calibrate by scene
        if (SceneManager.GetActiveScene().name != "Cabinet")
        {
            currentState = gamestate.moving;
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    private void Start()
    {
       // print("Der Eskapist S");
    }

    void FixedUpdate()
        {
            Managing();
        }
    
    private void Managing()
        {
        currentScene = SceneManager.GetActiveScene();
        if (previosScene!=currentScene)
        {
            onChangeScene();
            previosScene = currentScene;
        }

        if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                switch (currentState)
                {
                    case gamestate.moving:
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        break;
                    case gamestate.asking:
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.Confined;
                        break;
                    case gamestate.notes:
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.Confined;
                        break;
                    default:
                        break;
                }

        }
       

        }

    public void LoadScene(string scene) {
            Addressables.LoadSceneAsync(scene, LoadSceneMode.Single).Completed += (asyncHandle) =>
             {
                 loadScene = asyncHandle.Result;
             };
        }
    public void ChangeSceneT()
    {
        if (onChangeScene != null)
        {
            onChangeScene();
            //print($"tres"+onInfoTextShow.ToString());
        }

    }

} 

