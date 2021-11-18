using UnityEngine;

[ExecuteInEditMode]
public class JointChild : MonoBehaviour
{
    public HingeJoint2D joint;

    public void Awake()
    {
        joint.autoConfigureConnectedAnchor = false;
    }

    public void ConnectTo(JointParent jointParent)
    {
        joint.connectedBody = jointParent.body;
        joint.connectedAnchor = jointParent.anchor.localPosition;

        Vector3 connectedAnchorWorld = jointParent.anchor.position;
        Vector3 anchorWorld = joint.transform.TransformPoint(joint.anchor);

        Vector3 moveVector = connectedAnchorWorld - anchorWorld;
        transform.position += moveVector;
    }
}
