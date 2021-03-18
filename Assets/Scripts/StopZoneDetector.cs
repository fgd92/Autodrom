using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopZoneDetector : MonoBehaviour
{
    public Transform FinishLinePlane;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {           
            //if (Mathf.RoundToInt(other.gameObject.GetComponent<Dashboard>()?.Speed))
            //{
            //    if (FinishLinePlane.position.x - other.transform.position.x > 0.5f)
            //    {
            //        print("остановился более 0.5 метров перед линией");
            //    }
            //    else
            //    {
            //        print("OK");
            //    }
            //}
        }
    }
}
