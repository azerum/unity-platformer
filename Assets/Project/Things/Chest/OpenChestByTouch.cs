using System.Collections;
using UnityEngine;

public class OpenChestByTouch : MonoBehaviour
{
    public GameObject coinAsset;
    public int cointsCount;

    [Header("Coins throwing settings")]
    public float minThrowDelay;
    public float maxThrowDelay;

    public float minThrowAngle;
    public float maxThrowAngle;

    public float minThrowImpulse;
    public float maxThrowImpulse;

    private Animator animator;
   
    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        animator.SetBool("isOpened", true);
    }

    public void OnOpeningAnimationEnd()
    {
        StartCoroutine(ThrowingCoinsCoroutine());
    }

    private IEnumerator ThrowingCoinsCoroutine()
    {
        for (int i = 0; i < cointsCount; ++i)
        {
            ThrowCoin();

            float delay = Random.Range(minThrowDelay, maxThrowDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    private void ThrowCoin()
    {
        GameObject coin = Instantiate(coinAsset, transform.position, Quaternion.identity);
        Rigidbody2D rigidbody = coin.GetComponent<Rigidbody2D>();

        bool left = Random.Range(0, 2) == 1;
        float angle = Random.Range(minThrowAngle, maxThrowAngle);

        float angleRelativeToChest = left ? 90 + angle : 90 - angle;

        float imulse = Random.Range(minThrowImpulse, maxThrowImpulse);

        Vector3 direction = Quaternion.AngleAxis(angleRelativeToChest, Vector3.forward) * Vector3.right;
        rigidbody.AddForce(direction * imulse, ForceMode2D.Impulse);
    }
}
