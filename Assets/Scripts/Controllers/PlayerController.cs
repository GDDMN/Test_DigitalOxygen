using UnityEngine;

public class PlayerController : Actor
{
    [SerializeField]private ActorMovements _actorMovements;
    [SerializeField]private ActorAttack _actorAttack;

    private float _direction;

    private void Start()
    {
        Dead = false;
    }


    private void Update()
    {
        if (Dead)
            return;

        Run();
        Jump();

        if(!_actorMovements.IsJumping && _actorMovements.OnGround && Mathf.Abs(_direction) < .01f)
            Attack();
    }

    private void FixedUpdate()
    {
        if (_actorMovements.IsJumping)
            _actorMovements.JumpAnimation();
    }


    private void Run()
    {
        _direction = (Input.GetAxis("Horizontal"));
        _actorMovements.Run(_direction);
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
            _actorAttack.StartAttack();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
            _actorMovements.Jump();
    }

    public override void Death()
    {
        if (vertex != null)
            vertex.RemoveActorAction(this);

        Destroy(gameObject);
    }
}