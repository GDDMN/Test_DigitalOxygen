using System.Collections;
using UnityEngine;

public class ActorHurt : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator _animationController;
    [SerializeField] private float _cooldownSpeed;

    public void Interact(Collider other)
    {
        Hurt(other);
    }

    private void Hurt(Collider other)
    {
        var actorAttack = other.GetComponent<ActorAttack>();

        if (actorAttack == null)
            return;

        if (!actorAttack.IsAttacking)
            return;

        float damage = other.GetComponent<ActorAttack>().Damage;
        TakeDamageCalculation(damage);
        TakeDamageAnimation();
    }
   
    private void TakeDamageCalculation(float damage)
    {
        GetComponent<Actor>().actorData.Health -= damage;

        if (GetComponent<Actor>().actorData.Health <= 0)
            Die();
    }
    private void Die()
    {
        Destroy(gameObject);
    }

    private void TakeDamageAnimation()
    {
        StartCoroutine(HurtAnimation());
    }

    private IEnumerator HurtAnimation()
    {
        float progress = 0.0f;

        while (progress < 1)
        {
            progress += _cooldownSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
