using UnityEngine;

public delegate void AddScore();
public class Conus : MonoBehaviour
{
    public event AddScore AddScoreEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
            AddScoreEvent?.Invoke();
    }
}
