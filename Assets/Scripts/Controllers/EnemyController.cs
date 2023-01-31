using UnityEngine;

public class EnemyController : Actor
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private ActorMovements _actorMovements;
    [SerializeField] private BehaviourTree _behaviourTree;
    private Transform _playerPosition;

    private void Awake()
    {
        _playerPosition = _player.gameObject.transform;
    }

    private void Update()
    {
        _behaviourTree.Update();
    }

    private void FixedUpdate()
    {
        if (_actorMovements.IsJumping)
            _actorMovements.JumpAnimation();
    }

    private void Run(float direction)
    {
        _actorMovements.Run(direction);
    }

    private void Jump()
    {
        _actorMovements.Jump();
    }
    public override void Death()
    {
        onDeath.Invoke(this);
        Destroy(gameObject);
    }
}
