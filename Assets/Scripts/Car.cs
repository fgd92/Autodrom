using UnityEngine;

public class Car : MonoBehaviour
{
    protected const string HORIZONTAL = "Horizontal";
    protected const string VERTICAL = "Vertical";

    protected float horizontalInput;
    protected float verticalInput;
    protected float currentSteerAngle;
    protected float currentbreakForce;
    protected bool isBreaking;

    [SerializeField]
    protected float motorForce;
    [SerializeField]
    protected float breakForce;
    [SerializeField]
    protected float maxSteerAngle;

    [Header("Wheel Collider")]
    [SerializeField]
    protected WheelCollider frontLeftWheelCollider;
    [SerializeField]
    protected WheelCollider frontRightWheelCollider;
    [SerializeField]
    protected WheelCollider rearLeftWheelCollider;
    [SerializeField]
    protected WheelCollider rearRightWheelCollider;

    [Header("Wheel transform")]
    [SerializeField]
    protected Transform frontLeftWheelTransform;
    [SerializeField]
    protected Transform frontRightWheelTransform;
    [SerializeField]
    protected Transform rearLeftWheelTransform;
    [SerializeField]
    protected Transform rearRightWheelTransform;

    [Header("Rule")]
    [SerializeField]
    private Transform rule;

    protected void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);

        isBreaking = Input.GetKey(KeyCode.Space);
    }

    protected void RotateRule()
    {
        if (rule != null)
        {
            if (currentSteerAngle > -maxSteerAngle && currentSteerAngle < maxSteerAngle)
            {
                rule.Rotate(Vector3.up, horizontalInput * maxSteerAngle);
            }
        }
    }
    protected void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    protected void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    protected void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    protected void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    protected void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}