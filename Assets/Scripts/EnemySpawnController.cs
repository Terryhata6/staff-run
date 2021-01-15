using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject[] _spawnPos; 

    void Start()
    {
        SpawnEnemy();
    }

    private void Update()
    {
        if (_spawnPos[0] == null)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        _spawnPos = GameObject.FindGameObjectsWithTag(NameManager.SpawnEnemy);

        foreach (GameObject spawn in _spawnPos)
        {
            Instantiate(_enemy, spawn.transform.position, spawn.transform.rotation);
        }
    }
}
