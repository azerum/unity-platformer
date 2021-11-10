using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHP;
    public float redFlashingFromDamageDuration;
    public Text hpText;

    private int hp;
    private Coroutine redFlashing;

    private Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();

        hp = maxHP;
        redFlashing = null;

        UpdateHpText();
    }

    private void UpdateHpText()
    {
        hpText.text = $"{hp}/{maxHP}";
    }

    public void Heal(int heal)
    {
        hp += heal;
        hp = Math.Min(maxHP, hp);

        UpdateHpText();
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
        hp = Math.Max(0, hp);

        UpdateHpText();

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
