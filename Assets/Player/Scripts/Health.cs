using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP;
    private int hp;

    public float redFlashingFromDamageDuration;
    private Coroutine redFlashing;

    private Animator animator;

    public void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        hp = maxHP;
        redFlashing = null;
    }

    public void GetDamage(int damage)
    {
        hp -= damage;

        if (redFlashing != null)
        {
            StopCoroutine(redFlashing);
        }

        redFlashing = StartCoroutine(RedFlashing());
    }

    private IEnumerator RedFlashing()
    {
        animator.SetBool("isRedFlashing", true);
        yield return new WaitForSeconds(redFlashingFromDamageDuration);

        animator.SetBool("isRedFlashing", false);
    }
}
