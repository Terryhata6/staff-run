using System.Collections.Generic;
using UnityEngine;

public class FinalController : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyList = new List<Enemy>();

    private int _countEnemyKilled;


    public int CountEnemy()
    {
        return _enemyList.Count;
    }
    public int CountEnemyKilled()
    {
        _countEnemyKilled = 0;

        for (int i = 0; i < _enemyList.Count; i++)
        {
            if (_enemyList[i].IsDead()) _countEnemyKilled++;
        }

        return _countEnemyKilled;
    }
}
