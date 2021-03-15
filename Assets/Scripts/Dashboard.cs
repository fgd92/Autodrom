using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{
    Vector3 speed;
    Vector3 old_position;
    public Text ViewSpeed;

    Transform carTransform;
    CarController carController;

    void Start()
    {
        old_position = transform.position;
        carController = FindObjectOfType<CarController>();
        carTransform = carController.transform;
    }
    void FixedUpdate()
    {
        speed = ((carTransform.transform.position - old_position) / Time.fixedDeltaTime); 
        old_position = carTransform.transform.position; 
        ViewSpeed.text = Mathf.RoundToInt(speed.magnitude * 3.6f).ToString();
    }
}
