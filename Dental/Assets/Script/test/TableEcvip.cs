using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TableEcvip : MonoBehaviour
{
    Vector3 cameraVector;
    string _nameOf;
    [SerializeField]
    private EventString _evString;

    void Start()
    {
        cameraVector =  new Vector3(0, 10, 0);
        _nameOf = "Start play";
    }

    
    void Update()
    {
        if (CameraBeh.Instance.parent() ==transform)
        {
            CameraBeh.Instance.setPosition(cameraVector+transform.position);
            CameraBeh.Instance.lookAt(transform);

        }
    }
    private void OnMouseDown()
    {
        
            
        if (CameraBeh.Instance.parent() != transform)
        {
            CameraBeh.Instance.setParrent(transform);
            _evString.Invoke("");
        }
    }
    private void OnMouseEnter()
    {
        if (CameraBeh.Instance.parent() != transform)
        {
            _evString.Invoke(_nameOf);
        }

    }
    private void OnMouseExit()
    {
        if (CameraBeh.Instance.parent()!=transform)
        {
            _evString.Invoke("");
        }
    }
    public void AddListener(UnityAction<string> even) {
        _evString.AddListener(even);
    }
    public void DelListener(UnityAction<string> even)
    {
        _evString.RemoveListener(even);
    }

}

