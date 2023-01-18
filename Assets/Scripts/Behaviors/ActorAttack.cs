using System.Collections;
using UnityEngine;

public class ActorAttack: MonoBehaviour, IInteractable
{
    [SerializeField] private Animator _animationController;
    [SerializeField] private float _attackDistantion;

    private bool _isAttacking;

    public void Attack(float direction)
    {
        _animationController.SetTrigger("Attack");
        StartCoroutine(AttackAnimation(direction));
    }

    private IEnumerator AttackAnimation(float direction)
    {
        float progress = 0.0f;

        while (progress <= _attackDistantion)
        {
            progress += 2.0f * Time.deltaTime;
            transform.position += Vector3.right * direction;
            yield return null;
        }
    }

    public void Interact(Collider other)
    {
        if (_isAttacking)
            return;

        var actorMovements = other.GetComponent<ActorMovements>();

        if (actorMovements == null)
            return;

        actorMovements.Hurt();
    }

    private void OnTriggerEnter(Collider other)
    {
        Interact(other);
    }
}
