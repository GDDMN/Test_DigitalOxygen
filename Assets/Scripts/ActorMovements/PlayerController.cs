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
        Run();
        Jump();

        if (_actorMovements.IsJumping)
            _actorMovements.JumpAnimation();

        Attack();
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

    private void OnTriggerEnter(Collider other)
    {
        var interactableObject = other.GetComponent<IInteractable>();

        if (interactableObject != null)
            interactableObject.Interact(GetComponent<Collider>());
    }
}