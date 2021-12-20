using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractivBeh : MonoBehaviour
{
    public AudioSource sound;

    private void OnMouseDown()
    {
        
        sound.Play();

    }
}
