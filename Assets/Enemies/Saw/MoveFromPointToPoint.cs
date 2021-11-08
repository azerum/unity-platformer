using UnityEngine;

public class MoveFromPointToPoint : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float speed;

    private Rigidbody2D sawRigidbody;
    private Collider2D collider;

    private bool isMovingTowardsA;

    public void Start()
    {
        sawRigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<CircleCollider2D>();

        transform.position = pointA.position;
        isMovingTowardsA = false;

        Vector2 direction = pointB.position - pointA.position;
        direction.Normalize();

        sawRigidbody.velocity = direction * speed;
    }

    public void FixedUpdate()
    {
        Vector2 point = isMovingTowardsA ? pointA.position : pointB.position;

        if (collider.bounds.Contains(point))
        {
            isMovingTowardsA = !isMovingTowardsA;
            sawRigidbody.velocity = -sawRigidbody.velocity;
        }
    }
}
