using UnityEngine;

public class DamagePlayerByTouch : MonoBehaviour
{
    public int minDamage;
    public int maxDamageExclusive;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
        {
            return;
        }

        int damage = Random.Range(minDamage, maxDamageExclusive);

        Health health = collision.gameObject.GetComponentInParent<Health>();
        health.GetDamage(damage);

        ContactPoint2D hit = collision.GetContact(0);

        KnockbackOnDamage knockback =
            collision.gameObject.GetComponentInParent<KnockbackOnDamage>();

        knockback.KnockbackInDirection(-hit.normal);
    }
}
