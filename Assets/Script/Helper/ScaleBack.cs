using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBack
{
    public static Vector3 GetOriginalScale(Vector3 parentScale)
    {
        float xScale = 1 / parentScale.x;
        float yScale = 1 / parentScale.y;
        float zScale = 1 / parentScale.z;
        return new Vector3(xScale, yScale, zScale);
    }
}
