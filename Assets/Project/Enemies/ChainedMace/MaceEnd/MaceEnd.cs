using UnityEngine;

[ExecuteAlways]
public class MaceEnd : MonoBehaviour
{
    private HingeJoint2D actualMaceHinge;

    public void Awake()
    {
        actualMaceHinge = transform.Find("ActualMace").GetComponent<HingeJoint2D>();
    }

    public void ConnectTo(Rigidbody2D chainPart)
    {
        actualMaceHinge.connectedBody = chainPart;

        Transform t = chainPart.transform;
        Debug.Log(t.TransformPoint(Vector3.zero));
    }
}
