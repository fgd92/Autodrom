using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer : Car
{
    public GameObject Tractor;

    void FixedUpdate()
    {
        HandleSteering(transform.rotation.eulerAngles.y - Tractor.transform.rotation.eulerAngles.y);
        UpdateWheels();
    }
}
