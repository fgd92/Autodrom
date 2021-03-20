using UnityEngine;

public abstract class CarBase : MonoBehaviour
{
    public IMotor Motor;
    public IWheel Wheels;

    public float Speed;

    Vector3 speed;
    Vector3 old_position;

    private void Start()
    {
        old_position = transform.position;
    }
    private void CalculateSpeed()
    {
        speed = ((transform.position - old_position) / Time.fixedDeltaTime);
        old_position = transform.position;
        Speed = speed.magnitude;
    }

    private void FixedUpdate()
    {
        CalculateSpeed();
    }
}