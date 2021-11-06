using UnityEngine;

public class BeCollectedByPlayer : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoins collectCoins = other.GetComponentInParent<CollectCoins>();
            collectCoins.AddCoins(1);

            Destroy(gameObject);
        }
    }
}
