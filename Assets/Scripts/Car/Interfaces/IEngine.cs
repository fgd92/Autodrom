using UnityEngine;

public interface IEngine
{
    bool IsBreaking { get; set; }

    void Work(ref WheelCollider wheelCollider1, ref WheelCollider wheelCollider2, float delta);
}
