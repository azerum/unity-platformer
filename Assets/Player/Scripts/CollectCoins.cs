using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    private int coinsCount;

    public void AddCoins(int count)
    {
        coinsCount += count;
        Debug.Log($"Coins: {coinsCount}");
    }
}
