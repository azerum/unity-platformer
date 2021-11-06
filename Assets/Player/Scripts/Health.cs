using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP;
    private int hp;

    public float damageAnimationDuration = 1.0f;

    private Animator animator;
    private Coroutine redFlashing;

    public void Start()
    {
        hp = maxHP;

        animator = gameObject.GetComponent<Animator>();
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
        yield return new WaitForSeconds(damageAnimationDuration);

        animator.SetBool("isRedFlashing", false);
    }
}
