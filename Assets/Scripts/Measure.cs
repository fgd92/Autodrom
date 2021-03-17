using System;
using UnityEngine;

[Serializable]
public class Measure
{
    public GameObject Arrow;
    public float MinAngle;
    public float MaxAngle;
    public float MaxValue;
    public void SetAngle(float currentValue)
    {
        if(Arrow != null)
            Arrow.transform.localRotation = Quaternion.AngleAxis(Mathf.Lerp(MinAngle, MaxAngle, currentValue / MaxValue), Vector3.up);
    }
}
