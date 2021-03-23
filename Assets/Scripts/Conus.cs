using UnityEngine;

public class Conus : MonoBehaviour
{
    private bool once = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            AddScore();
            print(gameObject.name + " collision");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wheel"))
        {
            AddScore();
            print(gameObject.name + " Trigger");
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
