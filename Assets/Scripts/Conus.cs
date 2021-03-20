using UnityEngine;

public delegate void AddGrossMistake();
public class Conus : MonoBehaviour
{
    public event AddGrossMistake AddGrossMistake;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
            AddGrossMistake?.Invoke();
    }
}
