using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

[RequireComponent(typeof(PlanarReflectionProbe))]
public class Mirror : MonoBehaviour
{
    private PlanarReflectionProbe planarReflectionProbe;

    void Start()
    {
        planarReflectionProbe = GetComponent<PlanarReflectionProbe>();
    }
    private void Update()
    {
        
    }
    //Работает даже при просмотре на объект из viewport'а
    private void OnBecameInvisible()
    {
        planarReflectionProbe.enabled = false;
    }

    private void OnBecameVisible()
    {
        planarReflectionProbe.enabled = true;
    }

}
