using UnityEngine;

public class EnemyController : Actor
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private ActorMovements _actorMovements;
    private Transform _playerPosition;

    private void Awake()
    {
        _playerPosition = _player.gameObject.transform;
    }

    private void Update()
    {
        //MoveToPlayer();
    }

    private void FixedUpdate()
    {
        if (_actorMovements.IsJumping)
            _actorMovements.JumpAnimation();
    }
    private void MoveToPlayer()
    {
        Vector3 direction = _player.transform.position - transform.position;
        
        Debug.Log(direction);

        Ray ray = new Ray(transform.position, direction);
        float distantion = Vector3.Distance(transform.position, _player.gameObject.transform.position);

        Debug.DrawRay(transform.position, direction, Color.red, 10f);

        float directionForMoving = direction.x / Mathf.Abs(direction.x);

        if (Mathf.Abs(distantion) < 2.5f)
            directionForMoving = 0.0f;

        Run(directionForMoving);

        if (direction.y > 0.5f && Mathf.Abs(distantion) < 5f)
            Jump();
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
