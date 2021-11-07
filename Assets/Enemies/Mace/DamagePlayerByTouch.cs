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

        int damage = random.Next(minDamage, maxDamageExclusive);

        Health health = collision.gameObject.GetComponent<Health>();
        health.GetDamage(damage);

        ContactPoint2D hit = collision.GetContact(0);

        KnockbackOnDamage knockback = collision.gameObject.GetComponent<KnockbackOnDamage>();
        knockback?.KnockbackInDirection(-hit.normal);
    }
}
