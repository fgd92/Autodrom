using UnityEngine;

public class TemporarySpeedup : MonoBehaviour
{
    [SerializeField]
    private float speedupPower;
    private float lastSpeed;

    private Engine playerEngine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerEngine = other.gameObject.GetComponent<Engine>();
            
            lastSpeed = playerEngine.GetMotorForce();
            playerEngine.SetMotorForce(lastSpeed * speedupPower);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {           
            playerEngine.SetMotorForce(lastSpeed);
        }
    }
}
