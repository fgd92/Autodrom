using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public int MaxCountpathLines;
    public int CountPathLines;
    public int IndexCurrentPart;
    public PathLine[] PartsPath;
    public PathLine CurrentPathLine;

    private void Start()
    {
        var tempList = new List<PathLine>();

        for (int i = 0; i < transform.childCount-1; i++)
        {
            tempList.Add(transform.GetChild(i).GetComponent<PathLine>());
            tempList[i].Index = i;
            tempList[i].OnEnterEvent += PathController_OnEnterEvent;
            tempList[i].ChekExitEvent += PathController_ChekExitEvent;
        }

        PartsPath = tempList.ToArray();

    }

    private void PathController_ChekExitEvent(PathLine pathLine)
    {
        if (CurrentPathLine == pathLine)
        {
            transform.parent.GetComponent<Exercise>().AddMiddleMistakeInvoke();
        }
    }

    private void PathController_OnEnterEvent(PathLine pathLine)
    {
        PathLine prev = PartsPath[Mathf.Clamp(pathLine.Index - 1, 0, PartsPath.Length)];
        CurrentPathLine = PartsPath[pathLine.Index];

        if (prev.IsPassed)
        {
            CountPathLines += 1;

        }
    }

    public void CountPathParts()
    {
        CountPathLines += 1;

        int residue = MaxCountpathLines - CountPathLines;
        transform.parent.GetComponent<Exercise>().CountPathLinesLeft = residue;
    }


}
