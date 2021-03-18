using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer : Car
{
    public GameObject Tractor;

    void FixedUpdate()
    {
        Vector3 tractorPosition = Tractor.transform.position;
        Vector3 trailerPositon = transform.position;
        tractorPosition = new Vector3(tractorPosition.x, 0, tractorPosition.z);
        trailerPositon = new Vector3(trailerPositon.x, 0, trailerPositon.z);
        //float angle = Vector3.Angle(transform.forward, (tractorPosition - trailerPositon).normalized);
        //float angle = Vector3.SignedAngle(tractorPosition, trailerPositon, transform.forward);
        
        HandleSteering(Tractor.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y);
        UpdateWheels();
    }
}
