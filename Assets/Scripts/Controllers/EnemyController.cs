using UnityEngine;

public class EnemyController : Actor
{
    public override void Death()
    {
        Destroy(gameObject);
    }
}
