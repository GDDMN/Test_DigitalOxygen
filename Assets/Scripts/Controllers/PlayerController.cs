using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private ActorMovements _actorMovements;
    [SerializeField]private ActorAttack _actorAttack;

    private float _direction;


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
            _actorAttack.StartAttack();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
            _actorMovements.Jump();
    }
}