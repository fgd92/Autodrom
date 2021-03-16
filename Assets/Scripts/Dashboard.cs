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

    Transform carTransform;
    CarController carController;

    private float minAngle = -43;
    private float maxAngle = 43;
    private float maxSpeed = 45;


    void Start()
    {
        old_position = transform.position;
        carController = FindObjectOfType<CarController>();
        carTransform = carController.transform;
    }
    void FixedUpdate()
    {
        ShowSpeed();
    }

    private void ShowSpeed()
    {
        speed = ((carTransform.transform.position - old_position) / Time.fixedDeltaTime);
        old_position = carTransform.transform.position;
        ArrowSpeedometer.localRotation = Quaternion.AngleAxis(Mathf.LerpAngle(minAngle, maxAngle, speed.magnitude / maxSpeed), Vector3.up);
    }
}
