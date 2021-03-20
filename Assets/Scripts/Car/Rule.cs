using UnityEngine;

public class Rule : ScriptableObject, IRule
{
    private GameObject ruleGO;
    private Transform[] ruleComponents;

    public void SetRule(GameObject ruleGO)
    {
        this.ruleGO = ruleGO;
        GetChildFromRule(ruleGO);
    }

    private void GetChildFromRule(GameObject ruleGO)
    {
        if (ruleGO != null)
        {
            ruleComponents = new Transform[ruleGO.transform.childCount];

            for (int i = 0; i < ruleGO.transform.childCount; i++)
            {
                ruleComponents[i] = ruleGO.transform.GetChild(i);
            }
        }
    }

    public void HandleRule(float minAngle,float maxAngle, float angle)
    {
        if (angle > minAngle && angle < maxAngle)
        {
            for (int i = 0; i < ruleComponents.Length; i++)
            {
                ruleComponents[i].localRotation = Quaternion.Euler(0, angle, 0);

            }
        }
    }
}
