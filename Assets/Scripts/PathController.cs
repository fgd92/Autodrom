using UnityEngine;

public class PathController : MonoBehaviour
{
    public int MaxCountpathLines;
    public int CountPathLines;

    public void CountPathParts()
    {
        CountPathLines += 1;

        int residue = MaxCountpathLines - CountPathLines;
        transform.parent.GetComponent<Exercise>().CountPathLinesLeft = residue;
    }
}
