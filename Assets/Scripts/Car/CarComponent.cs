using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public abstract class CarComponent : MonoBehaviour
{
    protected PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        StartCall();
    }
    protected abstract void StartCall();
}
