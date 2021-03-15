using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AddScore();
public class Conus : MonoBehaviour
{
    public event AddScore AddScoreEvent;

    private void OnCollisionEnter(Collision collision)
    {
wa        if (collision.transform.CompareTag("Player"))
            AddScoreEvent?.Invoke();
    }
}
