using UnityEngine;

public class MakeMiddleMistkae : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent.GetComponent<Exercise>().AddMiddleMistakeInvoke();
        }
    }
}
