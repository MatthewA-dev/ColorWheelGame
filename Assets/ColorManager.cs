using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorManager : MonoBehaviour{
    public static Dictionary<ColorType, Color> colors = new Dictionary<ColorType, Color>();
    void Start(){
        foreach (Curve curve in CurveRenderer.lines){
            colors.Add(curve.colorType, curve.lineRenderer.endColor);
        }
    }
    void Update(){
        
    }
}
