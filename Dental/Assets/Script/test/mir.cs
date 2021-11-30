using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mir : MonoBehaviour
{
    public Cubemap cubeMap;
    public Camera cam;
    Material curmat;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("change", 1, 0.1f);
        curmat = gameObject.GetComponent<Renderer>().material;

        cubeMap = new Cubemap(512,TextureFormat.ARGB32,true);
        if (curmat == null)
        {
            Debug.Log("cw");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    void change()
    {
        //cam.transform.rotation = Quaternion.identity;
        cam.RenderToCubemap(cubeMap);
        curmat.SetTexture("_Cubemap", cubeMap);
    }

}
