using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineBeh : MonoBehaviour
{
    [SerializeField] private Material   outlineMat;
    [SerializeField] private float      outlineScale;
    [SerializeField] private Color  outlineColor;
    private Renderer outlineRenderer;
    /*
    void Start()
    {
        outlineRenderer = OutlineRender(outlineMat, outlineScale, outlineColor);
        outlineRenderer.enabled = true;
    }

    private Renderer OutlineRender(Material outlineMat, float outlineScale, Color outlineColor)
    {
        GameObject outlineObj = Instantiate(this.);
    }
     */
}
