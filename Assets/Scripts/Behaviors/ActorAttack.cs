using System.Collections;
using UnityEngine;

public class ActorAttack : MonoBehaviour
{
    [SerializeField] private Animator _animationController;
    [SerializeField] private float _attackDistantion;

    [SerializeField] public bool IsAttacking { get; private set; }

    public void Attack(float direction)
    {
        _animationController.SetTrigger("Attack");
        StartCoroutine(AttackAnimation(direction));
    }

    private IEnumerator AttackAnimation(float direction)
    {
        float progress = 0.0f;
        IsAttacking = true;

        while (progress <= _attackDistantion)
        {
            progress += 2.0f * Time.deltaTime;
            transform.position += Vector3.right * direction;
            yield return null;
        }
        IsAttacking = false;
    }
}
