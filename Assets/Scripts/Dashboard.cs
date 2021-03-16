using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{
    Vector3 speed;
    Vector3 old_position;
    public Transform ArrowSpeedometer;
    public Transform ArrowTachospeedometer;
    public Transform ArrowVoltage;
    public Transform ArrowOilPressure;
    public Transform ArrowTemprature;

    Transform carTransform;
    CarController carController;

    private float maxSpeed = 35;
    #region Speedometer
    private float minAngleSpeed = -43;
    private float maxAngleSpeed = 43;
    #endregion
    #region Tachospeedometer
    private float minAngleTachoSpeed = -130;
    private float maxAngleTachoSpeed = 45;
    #endregion

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
        ShowSpeed();
        ShowTachospeed();
        ShowTemprature();
        ShowOilPressure();
    }

    private void ShowTemprature()
    {
        ArrowTemprature.localRotation = Quaternion.AngleAxis(Mathf.Lerp(minAngleSpeed, maxAngleSpeed, 25f / 40f), Vector3.up);
    }

    private void ShowOilPressure()
    {
        ArrowOilPressure.localRotation = Quaternion.AngleAxis(Mathf.Lerp(1,1,2), Vector3.up);
    }


    private void ShowSpeed()
    {
        ArrowSpeedometer.localRotation = Quaternion.AngleAxis(Mathf.Lerp(minAngleSpeed, maxAngleSpeed, speed.magnitude / maxSpeed), Vector3.up);
    }
    private void ShowTachospeed()
    {
        ArrowTachospeedometer.localRotation = Quaternion.AngleAxis(Mathf.Lerp(minAngleTachoSpeed, maxAngleTachoSpeed, speed.magnitude / maxSpeed), Vector3.up);
    }
}
