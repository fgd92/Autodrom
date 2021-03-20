using UnityEngine;

public class StopZoneDetector : MonoBehaviour
{
    public Transform FinishLinePlane;

    private Exercise exercise;
    private Dashboard dashboard;
    private bool isTrailerFirst;
    private bool isTractorFirst;
    private float minAngle = 150;
    private float maxAngle = 210;

    private void Awake()
    {
        exercise = transform.parent.GetComponent<Exercise>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (!exercise.exercisesScriptable.IsParkWithTailer)
        {
            if (other.CompareTag("Player"))
            {
                if (Mathf.RoundToInt(dashboard.Speed) == 0)
                {
                    if (exercise.exercisesScriptable.IsPark)
                    {
                        //вычисление угла для заезда задом
                        Vector3 collider = transform.position;
                        collider = new Vector3(collider.x, 0, collider.z);
                        float angle = Vector3.Angle(other.transform.forward, collider);

                        if (angle > minAngle && angle < maxAngle)
                        {
                            CheckDistance(other);
                        }
                    }
                    else
                    {
                        CheckDistance(other);
                    }
                }
            }
        }
        else
        {
            if (isTrailerFirst)
            {
                if (Mathf.RoundToInt(dashboard.Speed) == 0)
                {
                    exercise.EndExercise(true);
                }
            }
        }
    }

    private void CheckDistance(Collider other)
    {
        //вычисление расстояния от финишной линии
        Vector3 finishLinePos = new Vector3(FinishLinePlane.position.x, 0, FinishLinePlane.position.z);
        Vector3 tractorPos = new Vector3(other.transform.position.x, 0, other.transform.position.z);
        if (Vector3.Distance(finishLinePos, tractorPos) > 6f)
        {
            //остановился более 0.5 метров перед линией
            exercise.EndExercise(true);
        }
        else
        {
            exercise.EndExercise(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dashboard = other.gameObject.GetComponent<Dashboard>();
        }

        if (!isTractorFirst && !isTrailerFirst)
        {
            isTractorFirst = other.CompareTag("Player");
            isTrailerFirst = other.CompareTag("Trailer");
        }
    }
}
