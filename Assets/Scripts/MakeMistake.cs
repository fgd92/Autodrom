using UnityEngine;

public class MakeMistake : MonoBehaviour
{
    [SerializeField]
    private int score = 3;
    [SerializeField]
    private bool isDestroy = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent.GetComponent<Exercise>().AddMistakeInvoke(score);
            if (isDestroy)
                Destroy(gameObject);
        }
    }
}
