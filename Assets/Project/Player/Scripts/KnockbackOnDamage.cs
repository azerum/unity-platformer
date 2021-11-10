using System.Collections;
using UnityEngine;

public class KnockbackOnDamage : MonoBehaviour
{
    public int durationInTimesteps;
    public float distanceFromHitPoint;

    private Rigidbody2D playerRigidbody;
    private Movement playerMovement;

    private int activeKnockbacksCount;

    public void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<Movement>();

        activeKnockbacksCount = 0;
    }

    public void KnockbackInDirection(in Vector2 direction)
    {
        StartCoroutine(DoKnockback(direction));
    }

    private IEnumerator DoKnockback(Vector2 direction)
    {
        OnKnockbackStart();

        //v0 (initial velocity) is 0
        playerRigidbody.velocity = Vector2.zero;

        //S = v0t + 1/2at^2
        //S = 1/2at^2
        //a = 2S/t^2

        float duration = durationInTimesteps * Time.fixedDeltaTime;
        float acceleration = 2 * distanceFromHitPoint / (duration * duration);

        float force = acceleration * playerRigidbody.mass;
        Vector2 forceVector = direction * force;

        for (int i = 0; i < durationInTimesteps; ++i)
        {
            playerRigidbody.AddForce(forceVector, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();
        }

        OnKnockbackEnd();
    }

    private void OnKnockbackStart()
    {
        ++activeKnockbacksCount;
        playerMovement.Freeze();
    }

    private void OnKnockbackEnd()
    {
        --activeKnockbacksCount;

        if (activeKnockbacksCount == 0)
        {
            playerRigidbody.velocity = Vector2.zero;
            playerMovement.Unfreeze();
        }
    }
}
