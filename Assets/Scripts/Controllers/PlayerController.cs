using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ActorMovements _actorMovements;
    private ActorAttack _actorAttack;
    private float _direction;

    private void Awake()
    {
        _actorMovements = GetComponent<ActorMovements>();
        _actorAttack = GetComponent<ActorAttack>();
    }

    private void Update()
    {
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
        if (Input.GetButtonDown("Fire1"))
            _actorAttack.Attack(_direction);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
            _actorMovements.Jump();
    }
}