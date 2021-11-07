using System.Collections;
using UnityEngine;

public class KnockbackOnDamage : MonoBehaviour
{
    public float force;
    public float duration;

    private Rigidbody2D playerRigidbody;
    private Movement playerMovement;

    private int activeKnockbacksCount;

    public void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerMovement = gameObject.GetComponent<Movement>();

        activeKnockbacksCount = 0;
    }

    public void KnockbackInDirection(in Vector2 direction)
    {
        StartCoroutine(DoKnockback(direction));
    }

    private IEnumerator DoKnockback(Vector2 direction)
    {
        OnKnockbackStart();

        float timeLeft = duration;
        Vector2 forceVector = direction * force;

        while (timeLeft > 0.0f)
        {
            playerRigidbody.AddForce(forceVector, ForceMode2D.Force);

            yield return new WaitForFixedUpdate();
            timeLeft -= Time.deltaTime;
        }

        OnKnockbackEnd();
    }

    private void OnKnockbackStart()
    {
        ++activeKnockbacksCount;

        playerMovement.Freeze();
        playerRigidbody.velocity = Vector2.zero;
    }

    private void OnKnockbackEnd()
    {
        --activeKnockbacksCount;

        if (activeKnockbacksCount == 0)
        {
            playerMovement.Unfreeze();
            playerRigidbody.velocity = Vector2.zero;
        }
    }
}
