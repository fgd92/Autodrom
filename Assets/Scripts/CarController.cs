using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public float RPMWheel = 0;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    private float minAngle = -30;
    private float maxAngle = 30;


    [SerializeField] 
    private float motorForce;
    [SerializeField] 
    private float breakForce;
    [SerializeField]
    private float maxSteerAngle;

    [Header("Wheel Collider")]
    [SerializeField] 
    private WheelCollider frontLeftWheelCollider;
    [SerializeField] 
    private WheelCollider frontRightWheelCollider;
    [SerializeField] 
    private WheelCollider rearLeftWheelCollider;
    [SerializeField] 
    private WheelCollider rearRightWheelCollider;

    [Header("Wheel transform")]
    [SerializeField]
    private Transform frontLeftWheelTransform;
    [SerializeField]
    private Transform frontRightWheelTransform;
    [SerializeField] 
    private Transform rearLeftWheelTransform;
    [SerializeField]
    private Transform rearRightWheelTransform;

    [Header("Rule")]
    [SerializeField]
    private Transform rule;

    private void FixedUpdate()
    {
        GetInput();
        RotateRule();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

        RPMWheel = (frontLeftWheelCollider.rpm + frontRightWheelCollider.rpm + rearLeftWheelCollider.rpm + rearRightWheelCollider.rpm) / 4;
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);

        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void RotateRule()
    {
        if(currentSteerAngle > -30 && currentSteerAngle < 30)
            rule.Rotate(Vector3.up, horizontalInput * maxSteerAngle);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot; 
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}