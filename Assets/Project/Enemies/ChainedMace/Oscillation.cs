using System.Collections;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    public float period;
    public float impulse;

    private Rigidbody2D myRigidbody;

    public void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(OscillationCoroutine());
    }

    private IEnumerator OscillationCoroutine()
    {
        bool isMovingRight = true;
        Vector2 direction = Vector2.right;

        while (true)
        {
            myRigidbody.AddForce(direction * impulse, ForceMode2D.Impulse);

            isMovingRight = !isMovingRight;
            direction = (isMovingRight) ? Vector2.right : Vector2.left;

            yield return new WaitForSeconds(period);
        }
    }
}
