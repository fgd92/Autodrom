using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{
    Vector3 speed;
    Vector3 old_position;

    public Measure Speedometer;
    public Measure Tachometer;
    public Measure Voltage;
    public Measure OilPressure;
    public Measure Temprature;

    Transform carTransform;
    CarController carController;

    void Start()
    {
        old_position = transform.position;
        carController = FindObjectOfType<CarController>();
        carTransform = carController.transform;
    }
    private void CalculateSpeed()
    {
        speed = ((carTransform.transform.position - old_position) / Time.fixedDeltaTime);
        old_position = carTransform.transform.position;
    }
    void FixedUpdate()
    {
        CalculateSpeed();

        Speedometer.SetAngle(speed.magnitude);
        Tachometer.SetAngle(carController.RPMWheel);
        Temprature.SetAngle(25);
    }
}
