using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier
{
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) 
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * oneMinusT * p0 +
            3f * oneMinusT * oneMinusT * t * p1 +
            3f * oneMinusT * t * t * p2 +
            t * t * t * p3;
    }
    public static Vector3 GetPoint(Vector3[] pointsBezier,float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * oneMinusT * pointsBezier[0] +
            3f * oneMinusT * oneMinusT * t * pointsBezier[1] +
            3f * oneMinusT * t * t * pointsBezier[2] +
            t * t * t * pointsBezier[3];
    }
    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) 
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (p1 - p0) +
            6f * oneMinusT * t * (p2 - p1) +
            3f * t * t * (p3 - p2);
    }
    public static Vector3 GetFirstDerivative(Vector3[] pointsBezier, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (pointsBezier[1] - pointsBezier[0]) +
            6f * oneMinusT * t * (pointsBezier[2] - pointsBezier[1]) +
            3f * t * t * (pointsBezier[3] - pointsBezier[2]);
    }
}
