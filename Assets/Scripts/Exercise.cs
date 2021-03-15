using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnEnd();
public class Exercise : MonoBehaviour
{
    public GameObject Player;
    public Transform StartPoint;
    public event OnEnd OnEndEvent;
    public int MaxScore;


    void Start()
    {
        Instantiate(Player, StartPoint.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnEndEvent?.Invoke();
    }
}
