using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TrafficLight : MonoBehaviour
{
    public Material Green;
    public Material Red;
    public Material Orange;
    public Material Gray;

    public GameObject First;
    public GameObject Second;
    public GameObject Third;

    public float TimeForSwitch = 4f;
    private float currentTime = 0;
    private int currentIndex = 0;
    private Dictionary<int, Material> Colors = new Dictionary<int, Material>();
    private List<GameObject> LightsValue = new List<GameObject>();


    void Start()
    {
        Colors.Add(0, Red);
        Colors.Add(1, Orange);
        Colors.Add(2, Green);

        LightsValue.Add(First);
        LightsValue.Add(Second);
        LightsValue.Add(Third);

    }


    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= TimeForSwitch)
        {
            currentTime = 0;
            currentIndex++;

            if (currentIndex > 2)
            {
                currentIndex = 0;
            }


        }
    }
}
