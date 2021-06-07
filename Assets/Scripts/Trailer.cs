using UnityEngine;

public class Trailer : MonoBehaviour
{
    [SerializeField]
    HingeJoint HingeJoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            HingeJoint.axis = Vector3.zero;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            HingeJoint.axis = Vector3.up;
        }
    }
}
