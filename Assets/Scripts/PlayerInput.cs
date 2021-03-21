
using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private bool isBraking = false;

    public float Horizontal { get; private set; }
    public float Vertival { get; private set; }

    public event Action Braked;

    public void GetInput()
    {
        Horizontal = Input.GetAxis(HORIZONTAL);
        Vertival = Input.GetAxis(VERTICAL);

        isBraking = Input.GetKey(KeyCode.Space);

        if (isBraking)
        {
            Braked?.Invoke();
        }
    }

    void Update()
    {
        GetInput();
    }
}