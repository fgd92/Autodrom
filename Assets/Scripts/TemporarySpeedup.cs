using UnityEngine;

public class TemporarySpeedup : MonoBehaviour
{
    [SerializeField]
    private float speedupPower;
    private float lastSpeed;

    private Engine engine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            engine = other.gameObject.GetComponent<Engine>();
            
            lastSpeed = engine.GetMotorForce();
            engine.SetMotorForce(lastSpeed * speedupPower);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {           
            engine.SetMotorForce(lastSpeed);
        }
    }
}
