using System.Collections;
using UnityEngine;

public class SawMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed;

    private Rigidbody2D sawRigidbody;
    private Coroutine movement;

    private Bounds aBounds;
    private Bounds bBounds;

    private Vector2 velocityFromAToB;
    private bool isMovingTowardsB;

    public void Start()
    {
        sawRigidbody = GetComponent<Rigidbody2D>();
        sawRigidbody.isKinematic = true;

        movement = null;

        aBounds = new Bounds(pointA.position, Vector3.one);
        bBounds = new Bounds(pointB.position, Vector3.one);

        Vector2 directionFromAToB = pointB.position - pointA.position;
        directionFromAToB.Normalize();

        velocityFromAToB = directionFromAToB * speed;

        transform.position = pointA.position;
        isMovingTowardsB = true;
    }

    public void StartMovement()
    {
        if (movement != null)
        {
            return;
        }

        movement = StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        sawRigidbody.velocity = velocityFromAToB;
        yield return new WaitForFixedUpdate();

        while (true)
        {
            Bounds pointBounds = isMovingTowardsB ? bBounds : aBounds;

            if (pointBounds.Contains(transform.position))
            {
                isMovingTowardsB = !isMovingTowardsB;
                sawRigidbody.velocity = -sawRigidbody.velocity;
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
