using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject[] _enemySpawnPos;
    [SerializeField] private GameObject _collectables;
    [SerializeField] private GameObject[] _collectablesSpawnPos;

    void Start()
    {
        SpawnEnemy();
        SpawnCollectables();
    }

    private void Update()
    {
        if (_enemySpawnPos[0] == null)
        {
            SpawnEnemy();
        }

        if(_collectablesSpawnPos[0] == null)
        {
            SpawnCollectables();
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
}
