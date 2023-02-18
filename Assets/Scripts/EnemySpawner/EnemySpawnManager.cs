using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : MonoBehaviour
{
    public int EnemyCount;
    public DeadEnemyCountUI deadEnemyCount;
    public UnityAction OnEveryEnemyDeth;

    [SerializeField] private List<EnemyController> _enemys = new List<EnemyController>();
    [SerializeField] private List<EnemySpawner> _spawners = new List<EnemySpawner>();

    
    public void StartInitialization()
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
        _spawners[spawnerNum].enemy.OnDeath += deadEnemyCount.Increase;
        _spawners[spawnerNum].enemy.OnDeath += CheckEnemyDeath;
    }

    private void CheckEnemyDeath()
    {
        foreach (var spawn in _spawners)
            if (spawn.enemy != null)
                return;

        if (EnemyCount > 0)
            return;

        OnEveryEnemyDeth.Invoke();
    }

}
