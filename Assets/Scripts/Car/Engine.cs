using UnityEngine;

[CreateAssetMenu(fileName ="Engine", menuName = "Car/Components/Engine")]
public class Engine : CarComponent, IEngine
{
    private FloatParameter motorForce;
    private FloatParameter breakForce;
    private FloatParameter currentbreakForce;
    private BooleanParameter isBreaking;
    public bool IsBreaking 
    {
        get 
        {
            return isBreaking.Value;
        } 
        set
        {
            isBreaking.Value = value;
            currentbreakForce.Value = isBreaking.Value ? breakForce.Value : 0f;
        }
    }

    public void SetBreakForce(float value)
    {
        breakForce.Value = value;
    }
    public float GetBreakForce()
    {
        return breakForce.Value;
    }
    public void SetForce(float value)
    {
        motorForce.Value = value;
    }

    public float GetForce()
    {
        return motorForce.Value;
    }

    public void Work(ref WheelCollider wheelCollider1, ref WheelCollider wheelCollider2, float delta)
    {
        wheelCollider1.motorTorque = delta * motorForce.Value;
        wheelCollider2.motorTorque = delta * motorForce.Value;
    }
}
