using UnityEngine;

public class DamagePlayerByTouch : MonoBehaviour
{
    public int minDamage;
    public int maxDamageExclusive;

    private readonly System.Random random = new System.Random();

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
        {
            return;
        }

        ContactPoint2D hit = collision.GetContact(0);

        Health health = collision.gameObject.GetComponent<Health>();
        health.GetDamage(20);

        Movement movement = collision.gameObject.GetComponent<Movement>();
        movement.Knockback(-hit.normal);
    }
}
