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
    protected override void StartCall()
    {
        wheels = GetComponent<Wheels>();
        playerInput.Braked += PlayerInput_Braked;
        playerInput.Moved += PlayerInput_Moved;
        playerInput.Rotated += PlayerInput_Rotated;
    }

    private void OnDestroy()
    {
        playerInput.Braked -= PlayerInput_Braked;
        playerInput.Moved -= PlayerInput_Moved;
        playerInput.Rotated -= PlayerInput_Rotated;
    }

    private void PlayerInput_Braked(bool isBracked)
    {
        IsBreaking = isBracked;
        wheels.ApplyBreaking(currentbreakForce);
    }
    private void PlayerInput_Moved(float delta)
    {
        wheels.Work(delta, motorForce);
    }

    private void PlayerInput_Rotated(float delta)
    {
        wheels.HandleSteering(delta);
    }
}
