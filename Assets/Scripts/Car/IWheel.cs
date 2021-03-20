using UnityEngine;

public interface IWheel
{
    void UpdateWheels();
    void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform);
    void HandleSteering(float angle);
    public void ApplyBreaking(float breakForce);
}
