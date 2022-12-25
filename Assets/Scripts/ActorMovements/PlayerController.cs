using UnityEngine;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    private ActorMovements _actorMovements;
    private int _direction;

    private void Awake()
    {
        _actorMovements = GetComponent<ActorMovements>();
    }

    private void Update()
    {
        Attack();
        Run();
        Jump();
    }


    private void Run()
    {
        _direction = ((int)Input.GetAxis("Horizontal"));
        _actorMovements.Run(_direction);
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
            _actorMovements.Attack();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
            _actorMovements.Jump(_direction);
    }
}
