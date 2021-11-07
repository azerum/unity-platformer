using System.Collections;
using UnityEngine;

public class HealthAndDamage : MonoBehaviour
{
    [Header("Health")]
    public int maxHP;

    [Header("Damage")]
    public float knockbackForce;
    public float knockbackDuration;

    public float redFlashingDuration;

    private Rigidbody2D playerRigidbody;
    private Movement movement;

    private Animator animator;

    private int hp;
    private int activeKnockbacksCount;
    private Coroutine redFlashing;

    public void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        movement = gameObject.GetComponent<Movement>();

        animator = gameObject.GetComponent<Animator>();

        hp = maxHP;
        activeKnockbacksCount = 0;
        redFlashing = null;
    }

    public void GetDamageInDirection(int damage, in Vector2 direction)
    {
        hp -= damage;

        if (redFlashing != null)
        {
            StopCoroutine(redFlashing);
        }

        redFlashing = StartCoroutine(RedFlashing());

        StartCoroutine(KnockbackInDirection(direction));
    }

    private IEnumerator RedFlashing()
    {
        animator.SetBool("isRedFlashing", true);
        yield return new WaitForSeconds(redFlashingDuration);

        animator.SetBool("isRedFlashing", false);
    }

    private IEnumerator KnockbackInDirection(Vector2 direction)
    {
        OnKnockbackStart();

        float timeLeft = knockbackDuration;
        Vector2 forceVector = direction * knockbackForce;

        while (timeLeft > 0.0f)
        {
            playerRigidbody.AddForce(forceVector, ForceMode2D.Force);

            yield return new WaitForEndOfFrame();
            timeLeft -= Time.deltaTime;
        }

        OnKnockbackEnd();
    }

    private void OnKnockbackStart()
    {
        ++activeKnockbacksCount;

        movement.Freeze();
        playerRigidbody.velocity = Vector2.zero;
    }

    private void OnKnockbackEnd()
    {
        --activeKnockbacksCount;

        if (activeKnockbacksCount == 0)
        {
            playerRigidbody.velocity = Vector2.zero;
            movement.Unfreeze();
        }
    }
}
