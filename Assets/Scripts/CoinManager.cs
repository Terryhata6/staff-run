using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private int _currentCoin = 0;

    public void AddCoin(int count)
    {
        _currentCoin += count;
    }

    public void ResetCoin()
    {
        _currentCoin = 0;
    }

    public int GetCurrentCoin()
    {
        return _currentCoin;
    }
    public void SetCurrentCoins(int count)
    {
        _currentCoin = count;
    }
}