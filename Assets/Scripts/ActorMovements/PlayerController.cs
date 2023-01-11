using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private ActorMovements _actorMovements;
    private float _direction;

    private void Awake()
    {
        _actorMovements = GetComponent<ActorMovements>();
    }

    private void Update()
    {
        Attack();
        Run();
        Jump();

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
            _actorMovements.Attack(_direction);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
            _actorMovements.Jump();
    }
}
