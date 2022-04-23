using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comend : MonoBehaviour
{
    public static Comend Instance;

    private void Awake()
    {
        if (Instance != this)
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }


    private void OnEnable()
    {
        
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
