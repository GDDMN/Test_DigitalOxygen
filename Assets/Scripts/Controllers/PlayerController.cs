using UnityEngine;

public class PlayerController : Actor
{
    [SerializeField]private ActorMovements _actorMovements;
    public ActorAttack actorAttack;
    public ActorHurt actorHurt;

    private float _direction;
    [HideInInspector]public bool GameStarted = false;


    private void Start()
    {
        Hurt = false;
    }


    private void Update()
    {
        if (!GameStarted)
            return;

        if (Hurt)
            return;

        Run();
        Jump();
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
        if (!_actorMovements.IsJumping && _actorMovements.OnGround && Mathf.Abs(_direction) < .01f)
        {
            if (Input.GetButtonDown("Fire1"))
                actorAttack.StartAttack();
        }
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