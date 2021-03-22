using System;
using UnityEngine;

public class TriggerTaskEvents : MonoBehaviour
{
    enum Cases
    {
        Stop,
        Start,
        StopStart
    }
    public event Action<string> TriggerTaskEvent;
    [SerializeField]
    private Cases cases;
    [SerializeField]
    private bool isEnter;
    [SerializeField]
    private string taskText;
    [SerializeField]
    private string secondTaskText;

    private Dashboard dashboard;    
    private bool completed;
    private bool onceStart = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dashboard = other.GetComponent<Dashboard>();

            if (isEnter)
            {
                switch (cases)
                {
                    case Cases.Stop:
                        break;
                    case Cases.Start:
                        if (Mathf.RoundToInt(dashboard.Speed) > 0.5f)
                            TriggerTaskEvent?.Invoke(taskText);
                        break;
                }
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isEnter)
            {
                switch (cases)
                {
                    case Cases.Stop:
                        completed = true;
                        if (Mathf.RoundToInt(dashboard.Speed) == 0)
                            TriggerTaskEvent?.Invoke(taskText);
                        break;
                    case Cases.Start:
                        if (dashboard.Speed > 0.5f)
                            TriggerTaskEvent?.Invoke(taskText);
                        break;
                    case Cases.StopStart:                        
                         if (dashboard.Speed < 0.1f && !onceStart)
                        {
                            onceStart = true;
                            TriggerTaskEvent?.Invoke(taskText);
                        }
                        if (dashboard.Speed > 0.5f && onceStart)
                        {
                            TriggerTaskEvent?.Invoke(secondTaskText);
                        }                        
                        break;
                    default:
                        break;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {

        switch (cases)
        {
            case Cases.Stop:                    
                //если выехал из зоны фиксации и не зафиксироавл машину на сколне
                if (!completed)                                            
                    transform.parent.GetComponent<Exercise>().AddMiddleMistakeInvoke();                                       
                break;
            default:
                break;
        }

        DestroyComponents();
    }

    public void InvokeTriggerTaskEvent()
    {
        TriggerTaskEvent?.Invoke(taskText);
    }

    private void DestroyComponents()
    {
        Destroy(GetComponent<BoxCollider>());
        Destroy(this);
    }
}
