using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Tractor : Car
{
    public Reductor Reductor;

    [NonSerialized]
    public float RPMWheel = 0;

    private void FixedUpdate()
    {
        GetInput();
        RotateRule();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

        RPMWheel = (frontLeftWheelCollider.rpm + frontRightWheelCollider.rpm + rearLeftWheelCollider.rpm + rearRightWheelCollider.rpm) / 4;
    }
}
