using UnityEngine;

public class MakeMiddleMistake : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent.GetComponent<Exercise>().AddMistakeInvoke(3);
        }
    }
}
