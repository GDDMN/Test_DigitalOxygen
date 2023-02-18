using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public bool Started;
    public int EnemyCount;

    [SerializeField] private List<EnemyController> _enemys = new List<EnemyController>();
    [SerializeField] private List<EnemySpawner> _spawners = new List<EnemySpawner>();

    private void Start()
    {
        foreach (var spawner in _spawners)
            SpawnEnemy(_spawners.IndexOf(spawner));
    }

    private void FindEmptySpawner()
    {
        if (EnemyCount <= 0)
            return;

        int spawnerNum = Random.Range(0, _spawners.Count);
        SpawnEnemy(spawnerNum);    
    }

    private void SpawnEnemy(int spawnerNum)
    {
        EnemyCount--;
        int enemyType = Random.Range(0, _enemys.Count);

        _spawners[spawnerNum].Instant(_enemys[enemyType]);
        _spawners[spawnerNum].enemy.OnDeath += FindEmptySpawner;
    }

}
