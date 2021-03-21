using UnityEngine;

public interface IMotor
{
    bool IsBreaking { get; set; }

    void HandleMotor(ref WheelCollider wheelCollider1, ref WheelCollider wheelCollider2, float delta);
}
