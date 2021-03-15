using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnEnd();
public class Exercise : MonoBehaviour
{
    public GameObject Player;
    public Transform StartPoint;
    public event OnEnd OnEndEvent;

    void Start()
    {
        Instantiate(Player, StartPoint.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
            OnEndEvent?.Invoke();
    }
}
