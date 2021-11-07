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

        HealthAndDamage health = collision.gameObject.GetComponent<HealthAndDamage>();
        health.GetDamageInDirection(20, -hit.normal);
    }
}
