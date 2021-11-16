using UnityEngine;

[ExecuteAlways]
public class ChainFragment : MonoBehaviour
{
    private HingeJoint2D firstPartHinge;
    public Rigidbody2D lastPartRigidbody { get; private set; }

    public void Awake()
    {
        firstPartHinge = transform.Find("FirstPart").GetComponent<HingeJoint2D>();
        lastPartRigidbody = transform.Find("LastPart").GetComponent<Rigidbody2D>();
    }

    public void SetPreviousChainPart(Rigidbody2D previousChainPart)
    {
        firstPartHinge.connectedBody = previousChainPart;
        transform.position = CalculatePositionToFitHinge(previousChainPart);
    }

    private Vector2 CalculatePositionToFitHinge(Rigidbody2D previousChainPart)
    {
        Transform partTransform = previousChainPart.gameObject.transform;
        return partTransform.TransformPoint(firstPartHinge.connectedAnchor);
    }
}
