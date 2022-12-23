using UnityEngine;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    private ActorMovements _actorMovements;
    
    private void Awake()
    {
        _actorMovements = GetComponent<ActorMovements>();
    }


    private void FixedUpdate()
    {
        Run();
    }

    private void Update()
    {
        Attack(); 
    }


    private void Run()
    {
        float direction = Input.GetAxis("Horizontal");
        _actorMovements.Run(direction);
    }

    private void Attack()
    {
        if (!Input.GetButtonDown("Fire1"))
            return;

        _actorMovements.Attack();
    }

    private void CheckForJumpInput()
    {

    }

    private void Jump()
    {
        _actorMovements.Jump();
    }
}
