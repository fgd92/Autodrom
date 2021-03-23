
using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private bool isBraking = false;

    public float Horizontal { get; private set; }
    public float Vertival { get; private set; }

    public event Action<bool> Braked;
    public event Action<float> Rotated;
    public event Action<float> Moved;

    public void GetInput()
    {
        Rotated?.Invoke(Horizontal = Input.GetAxis(HORIZONTAL));
        Moved?.Invoke(Vertival = Input.GetAxis(VERTICAL));
        Braked?.Invoke(isBraking = Input.GetKey(KeyCode.Space));
    }

    void Update()
    {
        GetInput();
    }
}