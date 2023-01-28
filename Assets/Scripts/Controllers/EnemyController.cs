using UnityEngine;

public class EnemyController : Actor
{
    [SerializeField] private PlayerController _player;
    private Transform _playerPosition;

    private void Awake()
    {
        _playerPosition = _player.gameObject.transform;
    }

    public override void Death()
    {
        Destroy(gameObject);
    }
}
