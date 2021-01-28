using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private int _currentCoin = 0;

    public void AddCoin()
    {
        _currentCoin ++;
    }

    public void ResetCoin()
    {
        _currentCoin = 0;
    }

    public int GetCurrentCoin()
    {
        return _currentCoin;
    }
}
