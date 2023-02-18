using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController enemy;

    public void Instant(EnemyController enemy)
    {
        this.enemy = Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
