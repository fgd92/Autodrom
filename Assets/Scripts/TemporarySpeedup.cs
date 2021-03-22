using UnityEngine;

public class TemporarySpeedup : MonoBehaviour
{
    [SerializeField]
    private float speedupPower;
    private float lastSpeed;

    private Tractor tractor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tractor = other.gameObject.GetComponent<Tractor>();
            //сохранить прошлую скорость
            //установить новую

            lastSpeed = tractor.motorForce;
            tractor.motorForce = lastSpeed * speedupPower;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //установить старую

            tractor.motorForce = lastSpeed;
        }
    }
}
