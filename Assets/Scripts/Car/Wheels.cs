using UnityEngine;

public class Wheels : CarComponent
{
    [Space(4)]
    private float maxSteerAngle = 30;
    private float currentSteerAngle;
    [SerializeField]
    private WheelCollider frontLeftWheelCollider;
    [SerializeField]
    private WheelCollider frontRightWheelCollider;
    [SerializeField]
    private WheelCollider rearLeftWheelCollider;
    [SerializeField]
    private WheelCollider rearRightWheelCollider;

    [SerializeField]
    private Transform frontLeftWheelTransform;
    [SerializeField]
    private Transform frontRightWheelTransform;
    [SerializeField]
    private Transform rearLeftWheelTransform;
    [SerializeField]
    private Transform rearRightWheelTransform;

    protected override void StartCall()
    {
    }
    private void FixedUpdate()
    {
        UpdateWheels();
    }

    public void HandleSteering(float delta)
    {
        float angle = maxSteerAngle * delta;
        currentSteerAngle = angle;
        frontLeftWheelCollider.steerAngle = angle;
        frontRightWheelCollider.steerAngle = angle;
    }
    public void RotateWheels(float angle)
    {
        currentSteerAngle = angle;
        frontLeftWheelCollider.steerAngle = angle;
        frontRightWheelCollider.steerAngle = angle;
        rearRightWheelCollider.steerAngle = angle;
        rearLeftWheelCollider.steerAngle = angle;
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

    public void Work(float delta, float motorForce)
    {
        rearRightWheelCollider.motorTorque = delta * motorForce;
        rearLeftWheelCollider.motorTorque = delta * motorForce;
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