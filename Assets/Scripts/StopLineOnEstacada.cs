using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLineOnEstacada : MonoBehaviour
{
    private Dashboard dashboard;
    private bool performed;
    private void OnTriggerStay(Collider other)
    {
        //если зафиксировал, то говорим при выходе это
        if (other.CompareTag("Player"))
        {
            dashboard = other.gameObject.GetComponent<Dashboard>();
            if (Mathf.RoundToInt(dashboard.Speed) == 0)
            {
                performed = true;                
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //если выехал из зоны фиксации и не зафиксироавл машину на сколне, то + ошибка
            if (!performed)
            {
                transform.parent.GetComponent<Exercise>().AddMiddleMistakeInvoke();
            }
        }
    }
}
