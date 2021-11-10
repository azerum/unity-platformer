using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    public int minHeal;
    public int maxHealExclusive;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health health = other.GetComponentInParent<Health>();
            health.Heal(Random.Range(minHeal, maxHealExclusive));

            Destroy(gameObject);
        }
    }
}
