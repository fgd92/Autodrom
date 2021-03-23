using UnityEngine;

public class Conus : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {            
            transform.parent.parent.GetComponent<Exercise>().AddMistakeInvoke(5);
            GetComponent<Conus>().enabled = false;
        }
    }
}
