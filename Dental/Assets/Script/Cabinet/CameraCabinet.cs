using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraCabinet : MonoBehaviour
{
    enum WorkState { 
        FPV=1,
        Free,
    }
    WorkState currentstate=WorkState.FPV;
    public Inputs mInputs;
    public float rotateSpeed;
    private Vector2 m_Rotation;
    bool action;
    bool quest;
    //public TextInfo textInfo;
    // Start is called before the first frame update
    void Awake()
    {
        mInputs = new Inputs();
        mInputs.Player.Action.started +=
            ctx =>
            {
                action = true;
            };
        mInputs.Player.Action.canceled +=
            ctx =>
            {
                action = false;
            };
        mInputs.Player.Quest.started +=
    ctx =>
    {
        quest = true;
        //print(quest);
    };
        mInputs.Player.Quest.canceled +=
            ctx =>
            {
                quest = false;
                //print(quest);
            };
    }

    private void OnEnable()
    {
        mInputs.Enable();
    }
    private void OnDisable()
    {
        mInputs.Disable();
    }
   
    void Update()
    {
        var look = mInputs.Player.Look.ReadValue<Vector2>();
        if (ScenaManager.Instance.currentState == gamestate.moving)
        {

        Look(look);
        RayRutine();
        }
    }

    private void RayRutine()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        
        RaycastHit hit;

            
        if (Physics.Raycast( ray,out hit ,2f))
        {
            switch (ScenaManager.Instance.currentState)
            {
                case gamestate.moving:
                    switch (hit.collider.gameObject.name)
                    {
                        case "Pacient":
                            UIEventSystem.Instance.InfoTextShowT(hit.collider.gameObject.name);//ui dynamic
                            if (action)
                            {
                                UIEventSystem.Instance.AskingBarShowT();
                                UIEventSystem.Instance.InfoTextHideT();
                            }
                            break;
                        case "Exit":
                            UIEventSystem.Instance.InfoTextShowT(hit.collider.gameObject.name);//ui dynamic
                            if (action)
                            {
                                //ScenaManager.Instance.currentState = gamestate.asking;
                                //SceneManager.LoadScene(0);
                                UIEventSystem.Instance.ResultShowT(); 
                            }
                            break;
                        case "Medcard":
                            UIEventSystem.Instance.InfoTextShowT(hit.collider.gameObject.name);//ui dynamic
                            if (action)
                            {
                                UIEventSystem.Instance.MedicalCardShowT();
                            }
                            break;
                        default:
                                
                            break;
                            
                    }
                    break;
                case gamestate.asking:
                    break;
                default:
                    //UIEventSystem.Instance.InfoTextHideT();
                    break;
            }
        }
            else {
            UIEventSystem.Instance.InfoTextHideT(); }
        if (quest)
        {
           
            UIEventSystem.Instance.QuestBarShowT();
        }
            
        
        //if (ScenaManager.Instance.currentState == gamestate.moving&& quest)
        //{
        //    UIEventSystem.Instance.AskingBarShowT();
        //}
    }

    private void Look(Vector2 rotate)
    {
        if (rotate.sqrMagnitude < 0.01)
            return;
        var scaledRotateSpeed = rotateSpeed * Time.deltaTime;
        m_Rotation.x = Mathf.Clamp(m_Rotation.x - rotate.y * scaledRotateSpeed, -89, 89);
        if (currentstate==WorkState.Free)
        {
            m_Rotation.y += rotate.x * scaledRotateSpeed;
        }
        transform.localEulerAngles = m_Rotation;

    }
}
