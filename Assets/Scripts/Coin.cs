using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinManager _coinManager;

    private void Awake()
    {
        _coinManager = FindObjectOfType<CoinManager>();
    }

    void Update()
    {
        //transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(NameManager.Player))
        {
            _coinManager.AddCoin();
            Destroy(gameObject);
        }
    }
}
