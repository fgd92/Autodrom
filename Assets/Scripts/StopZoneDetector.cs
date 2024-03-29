﻿using UnityEngine;

public class StopZoneDetector : MonoBehaviour
{
    public Transform FinishLinePlane;

    private Exercise exercise;
    private Dashboard dashboard;
    private bool isTrailerFirst;
    private bool isTractorFirst;
    private float minAngle = 150;
    private float maxAngle = 210;
    private TriggerTaskEvents triggerTask;
    private void Awake()
    {
        exercise = transform.parent.GetComponent<Exercise>();
        triggerTask = GetComponent<TriggerTaskEvents>();
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
                        if (BackCheck(other))
                            CheckDistance(other);
                    }
                    else
                    {
                        CheckDistance(other);
                    }
                }
                else
                {                                 
                    if (exercise.exercisesScriptable.IsPark && BackCheck(other))
                        triggerTask.InvokeTriggerTaskEvent();

                }
            }
        }
        else
        {
            if (isTrailerFirst)
            {
                if (dashboard!= null && Mathf.RoundToInt(dashboard.Speed) == 0)
                {
                    exercise.EndExercise(false);
                }
            }
        }
    }

    private bool BackCheck(Collider other)
    {
        //вычисление угла для заезда задом
        Vector3 collider = transform.position;
        collider = new Vector3(collider.x, 0, collider.z);
        float angle = Vector3.Angle(other.transform.forward, collider);

        return angle > minAngle && angle < maxAngle;        
    }

    private void CheckDistance(Collider other)
    {
        //вычисление расстояния от финишной линии
        Vector3 finishLinePos = new Vector3(FinishLinePlane.position.x, 0, FinishLinePlane.position.z);
        Vector3 tractorPos = new Vector3(other.transform.position.x, 0, other.transform.position.z);
        Vector3 perp = Vector3.zero;

        if (Mathf.RoundToInt(FinishLinePlane.rotation.eulerAngles.y) == 0)
        {
            perp = new Vector3(tractorPos.x, 0 ,finishLinePos.z);
        }
        else
        {
            perp = new Vector3(finishLinePos.x, 0, tractorPos.z);
        }
        float distance = Vector3.Distance(perp, tractorPos) - 3.5f;

        if (distance > 1f)
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
