using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Dashboard : MonoBehaviour, IDashboard
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private bool SpaceHolds =false;

    public Measures Measures;
    public float Speed { get; set; }
    private Vector3 speed;
    private Vector3 old_position;
    private void Start()
    {
        old_position = transform.position;
    }
    private void Update()
    {
        CalculateSpeed();
        ShowSpecifications();
    }
    public void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);

        SpaceHolds = Input.GetKey(KeyCode.Space);
    }

    private void ShowSpecifications()
    {
        SetValueMeasure(TypeMeasure.Speedometer, Speed);
        SetValueMeasure(TypeMeasure.Tachometer, Speed);
    }
    private void CalculateSpeed()
    {
        speed = ((transform.position - old_position) / Time.deltaTime);
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
