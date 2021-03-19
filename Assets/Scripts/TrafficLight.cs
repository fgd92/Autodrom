using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TrafficLight : MonoBehaviour
{
    public Material Green;
    public Material Red;
    public Material Yellow;
    public Material Gray;

    public MeshRenderer First;
    public MeshRenderer Second;
    public MeshRenderer Third;

    public float TimeForSwitch = 4f;
    private int increment = 1;
    private float currentTime = 0;
    private int currentIndex = 0;
    private List<Material> colors = new List<Material>();
    private List<MeshRenderer> lights = new List<MeshRenderer>();


    void Start()
    {
        colors.Add(Red);
        colors.Add(Yellow);
        colors.Add(Green);

        lights.Add(First);
        lights.Add(Second);
        lights.Add(Third);
    }


    void Update()
    {
        currentTime += Time.deltaTime;
        
        if(currentTime >= TimeForSwitch)
        {
            currentTime = 0;
            
            currentIndex+= increment;
        
            if (currentIndex == lights.Count - 1)
            {
                increment = -1;
            }
            else if(currentIndex == 0)
            {
                increment = 1;
            }
        
            SwitchLight(currentIndex);
        }
    }
    private void SwitchLight(int index)
    {
        lights[index].material = colors[index];
        lights[index].GetComponent<Light>().enabled = true;

        for (int i = 0; i < lights.Count; i++)
        {
            if (i != index)
            {
                lights[i].GetComponent<Light>().enabled = false;
                lights[i].material = Gray;
            }
        }
    }
}
