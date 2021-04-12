using System;
using UnityEngine;

public class TriggerTaskEvents : MonoBehaviour
{
    enum Cases
    {
        Stop,
        Start,
        StopStart,
        GreenStart
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
    [SerializeField]
    private GameObject invisibleBorder;

    private TrafficLight trafficLight;
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
                        TriggerTaskEvent?.Invoke(taskText);
                        break;
                    case Cases.Start:
                        if (Mathf.RoundToInt(dashboard.Speed) > 0.5f)
                            TriggerTaskEvent?.Invoke(taskText);
                        break;
                }
            }
        }
        else if (other.CompareTag("TrafficLight"))
        {
            if (cases == Cases.GreenStart)
                trafficLight = other.GetComponent<TrafficLight>();            
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
                        print("Целочисленная скорость - "+Mathf.RoundToInt(dashboard.Speed));
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
                            invisibleBorder.SetActive(true);
                        }
                        if (dashboard.Speed > 0.5f && onceStart)
                        {
                            TriggerTaskEvent?.Invoke(secondTaskText);
                        }                        
                        break;
                    case Cases.GreenStart:
                        if (dashboard.Speed > 0.5f)
                        {
                            if (trafficLight.CurrentStateTrafficeLight == StateTrafficLight.Green)
                                TriggerTaskEvent?.Invoke(taskText);
                            else
                            {
                                transform.parent.GetComponent<Exercise>().AddMistakeInvoke(5);                                
                            }
                            DestroyComponents();
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
                    transform.parent.GetComponent<Exercise>().AddMistakeInvoke(3);                                       
                break;
            default:
                DestroyComponents();
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
        if (cases == Cases.GreenStart)
            Destroy(GetComponent<BoxCollider>());

        Destroy(this);
    }
}
