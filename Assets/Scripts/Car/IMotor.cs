using UnityEngine;

public interface IMotor
{
    void HandleMotor(ref WheelCollider wheelCollider1, ref WheelCollider wheelCollider2, float delta);
}
