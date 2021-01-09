using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    private static LightControl instance;

    public static LightControl Instance
    {
        get => instance;
    }

    private void Awake()
    {
        instance = this;
    }


    public void Reset()
    {
        transform.rotation = Quaternion.AngleAxis(90, Vector3.right);
    }
    
    // pitch   高度角
    // azimuth 方位角
    public void SetAngle(float pitch, float azimuth)
    {
        azimuth = azimuth + 180;
        Quaternion q = Quaternion.AngleAxis(-azimuth, Vector3.up);
        Vector3 forward = q * (Vector3.forward);
        Vector3 right = Vector3.Cross(forward, Vector3.up);
        //
        Quaternion q1 = Quaternion.AngleAxis(pitch, right);

        transform.rotation = q * q1;
    }
}
