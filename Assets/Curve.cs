using System;
using UnityEngine;

// Curve class to organize lines and to simplify handling these curves
public class Curve{
    public LineRenderer lineRenderer
    { get; set; }
    public float angle
    { get; set; }
    public ColorType colorType
    { get; set; }
    public float offset = 0f;
    public Curve(LineRenderer line, float angle, ColorType type){
        this.lineRenderer = line;
        this.angle = angle;
        this.colorType = type;
    }
}