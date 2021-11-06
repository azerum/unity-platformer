using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP;
    private int hp;

    public float damageAnimationDuration = 1.0f;
    private float damageAnimationTimeLeft;

    private SpriteRenderer spriteRenderer;

    public void Start()
    {
        hp = maxHP;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        damageAnimationTimeLeft = 0.0f;
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
        damageAnimationTimeLeft += damageAnimationDuration;
    }
}
