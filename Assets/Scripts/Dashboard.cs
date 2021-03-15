using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{
    Vector3 speed;
    Vector3 old_position;
    public CarController CarController;
    public Text ViewSpeed;

    Transform carTransform;

    void Start()
    {
        old_position = transform.position;
        carTransform = transform;
    }
    void FixedUpdate()
    {
        speed = ((carTransform.transform.position - old_position) / Time.fixedDeltaTime); 
        old_position = carTransform.transform.position; 
        ViewSpeed.text = (speed.magnitude * 60).ToString();
    }
}
