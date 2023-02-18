using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController enemy;
    public ParticleSystem spawnEffect;

    public void Instant(EnemyController enemy)
    {
        spawnEffect.Play();
        this.enemy = Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
