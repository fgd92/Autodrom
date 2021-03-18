using UnityEngine;

public class PathLine : MonoBehaviour
{
    private bool isPassed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isPassed)
            {
                isPassed = true;
                transform.parent.GetComponent<PathController>().CountPathParts();
            }
        }
    }
}
