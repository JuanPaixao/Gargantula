using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathematicalSupport
{
    public static Vector3 ViewPortNormatize()
    {
        return ((Camera.main.ScreenToViewportPoint(Input.mousePosition) - new Vector3(0.5f, 0.5f, 0.5f)) * 2);
    }
    public static float ZRotation(Vector3 vec)
    {
        float degrees = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        return (degrees);
    }
    
}
