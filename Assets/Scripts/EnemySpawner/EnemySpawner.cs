using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController enemy;
    public ParticleSystem spawnEffect;

    private void Start()
    {
        spawnEffect.Stop();
    }

    public void Instant(EnemyController enemy)
    {
        spawnEffect.Play();
        this.enemy = Instantiate(enemy, transform.position, Quaternion.identity);
        this.enemy.OnDeath += ClearEnemyField;
    }

    private void ClearEnemyField()
    {
        enemy = null;
    }
}
