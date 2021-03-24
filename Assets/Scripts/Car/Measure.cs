using System;
using UnityEngine;

[Serializable]
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
    private float currentValue;
    public void SetValue(float currentValue)
    {
        if (Arrow != null)
        {
            this.currentValue = currentValue;
            Arrow.transform.localRotation = Quaternion.AngleAxis(Mathf.Lerp(MinAngle, MaxAngle, currentValue / MaxValue), Vector3.up);
        }
    }
}
