using UnityEngine;
using UnityEngine.UI;

public class CollectCoins : MonoBehaviour
{
    public Text coinsCountText;
    public int coinsCount { get; private set; }

    public void Start()
    {
        coinsCount = 0;
        UpdateCoinsCountText();
    }

    private void UpdateCoinsCountText()
    {
        coinsCountText.text = coinsCount.ToString();
    }

    public void AddCoins(int count)
    {
        coinsCount += count;
        UpdateCoinsCountText();
    }
}
