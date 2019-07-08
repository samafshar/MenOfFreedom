using UnityEngine;
using System.Collections;

public static class PhysPlus
{

    public static float GetAccelerationByParams(float v, float v0, float t)
    {
        return (v - v0) / t;
    }

    public static float GetAccelerationByParams_NoTime(float v, float v0, float dx)
    {
        dx = Mathf.Abs(dx);
        return (v*v - v0*v0) / (2*dx);
    }

    public static float GetDXByParams(float a, float t, float v0)
    {
        return Mathf.Abs( 0.5f * a * t * t + v0 * t);
    }
}
