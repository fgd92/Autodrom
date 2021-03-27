using UnityEngine;

public class Conus : MonoBehaviour
{
    private bool once = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Trailer"))
        {
            AddScore();            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wheel"))
        {
            AddScore();
        }
    }
    private void AddScore()
    {
        if (once) return;

        transform.parent.parent.GetComponent<Exercise>().AddMistakeInvoke(5);        
        once = true;
        Destroy(GetComponent<Conus>());
    }
}
