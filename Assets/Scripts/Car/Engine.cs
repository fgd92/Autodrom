using UnityEngine;

[CreateAssetMenu(fileName ="Engine", menuName = "Car/Components/Engine")]
public class Engine : CarComponent, IEngine
{
    private float motorForce;
    private float breakForce;
    private float currentbreakForce;
    private bool isBreaking;
    public bool IsBreaking 
    {
        get 
        {
            return isBreaking;
        } 
        set
        {
            isBreaking = value;
            currentbreakForce = isBreaking ? breakForce : 0f;
        }
    }

    public void SetBreakForce(float value)
    {
        breakForce = value;
    }
    public float GetBreakForce()
    {
        return breakForce;
    }
    public void SetForce(float value)
    {
        motorForce = value;
    }

    public float GetForce()
    {
        return motorForce;
    }

    public void Work(ref WheelCollider wheelCollider1, ref WheelCollider wheelCollider2, float delta)
    {
        wheelCollider1.motorTorque = delta * motorForce;
        wheelCollider2.motorTorque = delta * motorForce;
    }
}
