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
        joint.connectedAnchor = jointParent.anchor;

        MoveToFitAnchor();
    }

    private void MoveToFitAnchor()
    {
        Vector3 connectedAnchorWorld =
            joint.connectedBody.transform.TransformPoint(joint.connectedAnchor);

        Vector3 anchorWorld = transform.TransformPoint(joint.anchor);

        Vector3 movementVector = connectedAnchorWorld - anchorWorld;

        transform.position += movementVector;
    }
}
