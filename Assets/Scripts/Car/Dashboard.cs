using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
public class Dashboard : MonoBehaviour, IDashboard
{
    public Measures Measures;
    public float Speed { get; set; }
    private Vector3 speed;
    private Vector3 old_position;
    private void Start()
    {
        old_position = transform.position;
    }
    private void FixedUpdate()
    {
        CalculateSpeed();
        ShowSpecifications();
    }

    private void ShowSpecifications()
    {
        SetValueMeasure(TypeMeasure.Speedometer, Speed);
        SetValueMeasure(TypeMeasure.Temprature, Speed);
    }
    private void CalculateSpeed()
    {
        speed = ((transform.position - old_position) / Time.fixedDeltaTime);
        old_position = transform.position;
        Speed = speed.magnitude;
    }
    public void SetValueMeasure(TypeMeasure typeMeasure, float value)
    {
        Measure measure = Measures[typeMeasure];

        if (measure != null)
        {
            measure.SetValue(value);
        }
        else
        {
            Debug.LogError(typeMeasure + " отсуствует на машине.");
        }
    }
}
