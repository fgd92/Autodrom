using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class GeneratePath : MonoBehaviour
{
    public Vector3 Orientation;
    public float Distance = 5;
    public int CountPoint = 100;
    public float height = 0.2f;
    public int Rate;
    public float Width;

    public Transform EndPosition;
    public Transform StartStartPosition;
    public Transform EndStartPositoin;

    LineRenderer lineRenderer;

    private void GenerateSnake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.positionCount = CountPoint + 1;

        lineRenderer.SetPosition(0, StartStartPosition.localPosition);
        lineRenderer.SetPosition(1, EndStartPositoin.localPosition);

        Vector3 startPos = EndStartPositoin.localPosition;
        startPos = new Vector3(startPos.x, 0, startPos.z);
        float step = Distance - 1 / CountPoint;
        float increment = 0;

        for (int i = 2; i < CountPoint; i++)
        {
            increment += step;
            float x = Mathf.Sin(startPos.x + increment * Width) * height;
            x = Mathf.Pow(x, Rate);
            lineRenderer.SetPosition(i, new Vector3(x, 0, startPos.z + increment));
        }

        lineRenderer.SetPosition(CountPoint, EndPosition.localPosition);
    }

    void Update()
    {
        if(Application.isPlaying == false)
            GenerateSnake();
    }
}
