using System;
using UnityEngine;

public class Rule : CarComponent
{
    public GameObject RuleGO;
    public float MaxSteeringRule = 60;
    private Transform[] ruleComponents;

    public void SetRule(GameObject ruleGO)
    {
        this.RuleGO = ruleGO;
        GetChildFromRule(ruleGO);
    }
    protected override void StartCall()
    {
        GetChildFromRule(RuleGO);
    }
    private void FixedUpdate()
    {
        HandleRule(playerInput.Horizontal);
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

    public void HandleRule(float delta)
    {
        float angle = delta * MaxSteeringRule;
        for (int i = 0; i < ruleComponents.Length; i++)
        {
            ruleComponents[i].localRotation = Quaternion.Euler(0, angle, 0);

        }
    }
}
