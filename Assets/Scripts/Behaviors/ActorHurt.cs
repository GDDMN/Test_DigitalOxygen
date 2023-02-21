using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHurt : MonoBehaviour, IInteractable
{
    [SerializeField] private Actor _actor;
    [SerializeField] private Animator _animationController;

    [Space(1)]
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private ParticleSystem _punchEffect;
    [SerializeField] private Transform _amaturePosition;
    [SerializeField] private AudioSource _audioSource;
    private bool _IsDead = false;

    public List<AudioClip> punchClips = new List<AudioClip>();
    public List<AudioClip> deathClips = new List<AudioClip>();

    public void Interact(Collider other)
    {
        var actorAttack = other.GetComponent<ActorAttack>();
        Hurt(actorAttack.Damage);
    }

    private void SetHurtTrigger()
    {
        _actor.Hurt = true;
    }

    private void SetIdleTrigger()
    {
        _actor.Hurt = false;
    }

    private void Hurt(float damage)
    {
        _punchEffect.GetComponent<AudioSource>().clip = punchClips[Random.Range(0, punchClips.Count-1)];
        _punchEffect.Play();
        _punchEffect.GetComponent<AudioSource>().Play();
        TakeDamageCalculation(damage);
        
        if(_actor.getHurt != null)
            _actor.getHurt.Invoke();

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
        _audioSource.clip = deathClips[Random.Range(0, punchClips.Count-1)];
        _audioSource.Play();

        _IsDead = true;
        _actor.Hurt = _IsDead;
        _animationController.SetBool("Death", _IsDead);
    }

    public void Death()
    {
        Instantiate(_deathEffect.gameObject, _amaturePosition.position + (Vector3.up * 0.5f), Quaternion.identity);
        _actor.Death();
    }

}
