using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject[] _enemySpawnPos;
    [SerializeField] private GameObject _collectables;
    [SerializeField] private GameObject[] _collectablesSpawnPos;
    [SerializeField] private GameObject _coins;
    [SerializeField] private GameObject[] _coinsSpawnPos;
    private bool _spawnedEnemy;
    private bool _spawnedCollectables;
    private bool _spawnedCoins;

    void Start()
    {
        SpawnEnemy();
        SpawnCollectables();
    }

    private void Update()
    {
        if (_enemySpawnPos[0] == null && _spawnedEnemy == false)
        {
            SpawnEnemy();
            _spawnedEnemy = true;
        }

        if(_collectablesSpawnPos[0] == null && _spawnedCollectables == false)
        {
            SpawnCollectables();
            _spawnedCollectables = true;
        }

        if (_coinsSpawnPos[0] == null && _spawnedCoins == false)
        {
            SpawnCoins();
            _spawnedCoins = true;
        }
    }

    private void SpawnEnemy()
    {
        _enemySpawnPos = GameObject.FindGameObjectsWithTag(NameManager.SpawnEnemy);

        foreach (GameObject spawn in _enemySpawnPos)
        {
            Instantiate(_enemy, spawn.transform.position, spawn.transform.rotation);
        }
    }

    private void SpawnCollectables()
    {
        _collectablesSpawnPos = GameObject.FindGameObjectsWithTag(NameManager.SpawnCollectables);

        foreach (GameObject spawn in _collectablesSpawnPos)
        {
            Instantiate(_collectables, spawn.transform.position, spawn.transform.rotation);
        }
    }

    private void SpawnCoins()
    {
        _coinsSpawnPos = GameObject.FindGameObjectsWithTag(NameManager.SpawnCoins);

        foreach (GameObject spawn in _coinsSpawnPos)
        {
            Instantiate(_coins, spawn.transform.position, spawn.transform.rotation);
        }
    }
}
