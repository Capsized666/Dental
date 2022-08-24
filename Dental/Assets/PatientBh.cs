using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientBh : MonoBehaviour
{


    int openstate = Animator.StringToHash("openstate");   
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGingivaState(bool b) {
        animator.SetBool(openstate, b);
    }

}
