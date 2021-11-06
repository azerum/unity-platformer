using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP;
    private int hp;

    public void Start()
    {
        hp = maxHP;
    }

    public void ChangeHealth(int change)
    {
        hp += change;

        if (hp <= 0)
        {
            Debug.Log("Dead");
        }
        else if (hp > maxHP)
        {
            hp = maxHP;
        }

        Debug.Log($"HP: {hp}");
    }
}
