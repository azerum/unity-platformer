using UnityEngine;

public class MoveFromPointToPoint : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed;

    private Bounds aBounds;
    private Bounds bBounds;

    private Rigidbody2D sawRigidbody;
    private bool isMovingTowardsB;

    public void Start()
    {
        sawRigidbody = gameObject.GetComponent<Rigidbody2D>();

        aBounds = new Bounds(pointA.position, Vector3.one);
        bBounds = new Bounds(pointB.position, Vector3.one);

        transform.position = pointA.position;
        isMovingTowardsB = true;

        Vector2 directionFromAToB = pointB.position - pointA.position;
        directionFromAToB.Normalize();

        sawRigidbody.velocity = directionFromAToB * speed;
    }

    public void FixedUpdate()
    {
        Bounds pointBounds = isMovingTowardsB ? bBounds : aBounds;

        if (pointBounds.Contains(transform.position))
        {
            isMovingTowardsB = !isMovingTowardsB;
            sawRigidbody.velocity = -sawRigidbody.velocity;
        }
    }
}
