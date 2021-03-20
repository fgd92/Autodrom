using UnityEngine;

public class Wheels : ScriptableObject, IWheel
{
    private float maxSteerAngle;
    private float currentSteerAngle;
    private WheelCollider frontLeftWheelCollider;
    private WheelCollider frontRightWheelCollider;
    private WheelCollider rearLeftWheelCollider;
    private WheelCollider rearRightWheelCollider;

    private Transform frontLeftWheelTransform;
    private Transform frontRightWheelTransform;
    private Transform rearLeftWheelTransform;
    private Transform rearRightWheelTransform;

    public void SetMaxSteerAngle(float angle)
    {
        maxSteerAngle = angle;
    }
    public float GetMaxSteerAngle()
    {
        return maxSteerAngle;
    }

    public void HandleSteering(float angle)
    {
        currentSteerAngle = angle;
        frontLeftWheelCollider.steerAngle = angle;
        frontRightWheelCollider.steerAngle = angle;
    }
    public float GetRPMWheel()
    {
        return (frontLeftWheelCollider.rpm + frontRightWheelCollider.rpm 
                           + rearLeftWheelCollider.rpm + rearRightWheelCollider.rpm) / 4;
    }

    public void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    public void UpdateWheels()
    {
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }
    public void ApplyBreaking(float breakForce)
    {
        frontRightWheelCollider.brakeTorque = breakForce;
        frontLeftWheelCollider.brakeTorque = breakForce;
        rearLeftWheelCollider.brakeTorque = breakForce;
        rearRightWheelCollider.brakeTorque = breakForce;
    }
}