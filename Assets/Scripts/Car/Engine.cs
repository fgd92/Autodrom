using UnityEngine;

[RequireComponent(typeof(Wheels))]
public class Engine : CarComponent
{
    Wheels wheels;
    [SerializeField]
    private float motorForce;
    [SerializeField]
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
    public void SetMotorForce(float value)
    {
        motorForce = value;
    }
    public float GetMotorForce()
    {
        return motorForce;
    }
    protected override void StartCall()
    {
        wheels = GetComponent<Wheels>();
        playerInput.Braked += PlayerInput_Braked;

    }
    private void FixedUpdate()
    {
        wheels.Work(playerInput.Vertival, motorForce);
        wheels.HandleSteering(playerInput.Horizontal);
    }
    private void OnDestroy()
    {
        playerInput.Braked -= PlayerInput_Braked;
    }

    private void PlayerInput_Braked(bool isBracked)
    {
        IsBreaking = isBracked;
        wheels.ApplyBreaking(currentbreakForce);
    }
}
