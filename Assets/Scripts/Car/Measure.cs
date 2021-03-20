using System;
using UnityEngine;

public class Measure : MonoBehaviour
{
    public TypeMeasure TypeMeasure;
    [SerializeField]
    private GameObject Arrow;
    [SerializeField]
    private float MinAngle;
    [SerializeField]
    private float MaxAngle;
    [SerializeField]
    private float MaxValue;
    public void SetValue(float currentValue)
    {
        if(Arrow != null)
            Arrow.transform.localRotation = Quaternion.AngleAxis(Mathf.Lerp(MinAngle, MaxAngle, currentValue / MaxValue), Vector3.up);
    }
}
