using UnityEngine;

public class Conus : MonoBehaviour
{
    private bool once = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
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
        Destroy(GetComponent<Conus>());
        once = true;
    }
}
