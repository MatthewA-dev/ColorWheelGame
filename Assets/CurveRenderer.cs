using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CurveRenderer : MonoBehaviour
{
    public int segments = 50;
    public float xradius = 5;
    public float yradius = 5;
    // used to add linerenderers easily to lines dictionary
    public List<LineRenderer> lineWrapper = new List<LineRenderer>();
    // Making the assumption that there are enough ColorTypes for each line renderer
    public static List<Curve> lines = new List<Curve>();
    private static CircleCollider2D coll;
    void Awake()
    {
        Array colorValues = Enum.GetValues(typeof(ColorType));
        for (int i = 0; i < lineWrapper.Count; i++){
            LineRenderer line = lineWrapper[i];
            ColorType type = (ColorType) colorValues.GetValue(i);
            line.positionCount = segments + 1;
            lines.Add(new Curve(line, 360f / lineWrapper.Count, type));
        }
        coll = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        // Build circle
        List<float> floats = new List<float>();
        foreach (Curve curve in lines)
        {
            floats.Add(curve.angle);
            CreatePoints(curve.lineRenderer, floats, curve.offset);
        }
    }
    void CreatePoints (LineRenderer line, List<float> floats, float offset)
    {
        float x;
        float y;
        foreach (float floatPart in floats)
        {
            offset += floatPart;
        }
        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * offset) * xradius;
            y = Mathf.Cos (Mathf.Deg2Rad * offset) * yradius;
            line.SetPosition (i,new Vector3(x,y,0) );
            offset += (floats[floats.Count - 1] / segments);
        }
    }
    public void Rotate(float input){
        foreach (Curve curve in lines)
        {
            curve.offset -= input;
        }
    }
}
