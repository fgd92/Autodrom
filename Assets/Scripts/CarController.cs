using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public LayerMask LayerMaskButton;

    private Rigidbody _rigidbody;
    private Camera camera;

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

    [SerializeField]
    private Transform rule;
    [SerializeField]
    private Transform button;

    private void Start()
    {
        camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
    }
    IEnumerator PressingButton()
    {
        Vector3 startPosition = button.position;
        Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y - 0.015f, startPosition.z);
        float time = 0.5f;

        for (float i = 0; i < time; i+= Time.deltaTime)
        {
            button.localPosition = Vector3.Lerp(startPosition, targetPosition, i / time);

            yield return null;
        }
    }
    private void FixedUpdate()
    {
        RaycastHit raycastHit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out raycastHit, LayerMaskButton))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (raycastHit.transform.name == button.name)
                {
                    StartCoroutine(PressingButton());
                }
            }
        }

        GetInput();
        RotateRule();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);

        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void RotateRule()
    {
        float angle = Mathf.RoundToInt(frontRightWheelTransform.localRotation.eulerAngles.y);

        if (angle > 180)
        {
            angle = 360 - angle;
        }

        if (angle > minAngle && angle < maxAngle)
        {
            rule.Rotate(Vector3.up, horizontalInput * 5);
        }
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