using UnityEngine;
using System.Collections;

public class BeCollectedByPlayerAfterSomeTime : MonoBehaviour
{
    public float uncollectableDuration = 1.0f;
    private bool canBeCollected;

    public void Start()
    {
        canBeCollected = false;
        StartCoroutine(BecomeCollectableAfterDuration());
    }

    private IEnumerator BecomeCollectableAfterDuration()
    {
        yield return new WaitForSeconds(uncollectableDuration);
        canBeCollected = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnterOrStay(other);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerEnterOrStay(other);
    }

    private void OnTriggerEnterOrStay(Collider2D other)
    {
        if (!canBeCollected)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            CollectCoins collectCoins = other.GetComponentInParent<CollectCoins>();
            collectCoins.AddCoins(1);

            Destroy(gameObject);
        }
    }
}
