using System.Collections;
using UnityEngine;

public class ActorHurt : MonoBehaviour, IInteractable
{
    [SerializeField] private Actor _actor;
    [SerializeField] private Animator _animationController;
    [SerializeField] private float _cooldownSpeed;

    [Space(1)]
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private ParticleSystem _punchEffect;
    [SerializeField] private Transform _amaturePosition;
    private bool _IsDead = false;

    public void Interact(Collider other)
    {
        var actorAttack = other.GetComponent<ActorAttack>();
        Hurt(actorAttack.Damage);
    }

    private void Hurt(float damage)
    {
        _punchEffect.Play();
        TakeDamageCalculation(damage);

        if (_IsDead)
            return;

        _animationController.SetInteger("HurtType", Random.Range(0, 3));
        _animationController.SetTrigger("Hurt");
    }
   
    private void TakeDamageCalculation(float damage)
    {
        _actor.actorData.Health -= damage;

        if (_actor.actorData.Health <= 0)
            Die();
    }
    private void Die()
    {
        _IsDead = true;
        _animationController.SetBool("Death", _IsDead);
    }

    public void Death()
    {
        Instantiate(_deathEffect.gameObject, _amaturePosition.position, Quaternion.identity);
        _actor.Death();
    }

}
