using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirorbeh : MonoBehaviour
{
    public Cubemap cubemap;
    Material curmat;
    GameObject cum;
    Camera cam;
    void Start()
    {
        curmat = gameObject.GetComponent<Renderer>().material;
        cubemap = new Cubemap(1024, TextureFormat.RGBA32, false);
        cum = new GameObject("Cum", typeof(Camera));
        cam = cum.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        cum.transform.position = gameObject.transform.position;
        cum.transform.rotation = Quaternion.identity;
        cam.RenderToCubemap(cubemap);
        curmat.SetTexture("_Cubemap", cubemap);
       // DestroyImmediate(cum);
    }
    private void OnMouseDown()
    {
        //print("aw");
        
    }
        
    public void OnTriggerEnter(Collider other)
    {
        
    }
}
