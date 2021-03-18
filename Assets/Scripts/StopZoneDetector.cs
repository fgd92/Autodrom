using UnityEngine;

public class StopZoneDetector : MonoBehaviour
{
    public Transform FinishLinePlane;

    private Dashboard dashboard;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Mathf.RoundToInt(dashboard.Speed) == 0)
            {
                Vector3 finishLinePos = new Vector3(FinishLinePlane.position.x,0,FinishLinePlane.position.z);
                Vector3 tractorPos = new Vector3(other.transform.position.x, 0, other.transform.position.z);
                
                if (Vector3.Distance(finishLinePos, tractorPos) > 6f)
                {
                    //остановился более 0.5 метров перед линией
                    transform.parent.GetComponent<Exercise>().EndExercise(true);
                }
                else
                {
                    transform.parent.GetComponent<Exercise>().EndExercise(false);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dashboard = other.gameObject.GetComponent<Dashboard>();
        }
    }
}
