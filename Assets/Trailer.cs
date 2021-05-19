using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer : MonoBehaviour
{
    [SerializeField]
    HingeJoint HingeJoint;

    JointLimits defaultHingeJoint;
    JointLimits stopHingeJoint;
    Transform StopHingeJointTransform;

    void Start()
    {
        defaultHingeJoint = HingeJoint.limits;

        JointLimits limits = new JointLimits()
        {
            max = 0,
            min = 0
        };

        stopHingeJoint = limits;
        StopHingeJointTransform = HingeJoint.transform;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            JointLimits limits = new JointLimits()
            {
                max = StopHingeJointTransform.eulerAngles.y,
                min = StopHingeJointTransform.eulerAngles.y
            };

            HingeJoint.limits = limits;
        }
        else
        {
            HingeJoint.limits = defaultHingeJoint;
        }
    }
}
