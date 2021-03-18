using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{

    public Measure Speedometer;
    public Measure Tachometer;
    public Measure Voltage;
    public Measure OilPressure;
    public Measure Temprature;
    public float Speed;

    Transform carTransform;
    Tractor carController;
    Vector3 speed;
    Vector3 old_position;

    void Start()
    {
        old_position = transform.position;
        carController = FindObjectOfType<Tractor>();
        carTransform = carController.transform;
    }
    private void CalculateSpeed()
    {
        speed = ((carTransform.transform.position - old_position) / Time.fixedDeltaTime);
        old_position = carTransform.transform.position;
        Speed = speed.magnitude;
    }
    void FixedUpdate()
    {
        CalculateSpeed();

        Speedometer.SetAngle(Speed);
        Tachometer.SetAngle(carController.RPMWheel);
        Temprature.SetAngle(25);
    }
}
