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
        //Debug.Log("Hurt " + other.GetComponent<ActorAttack>().Damage);

        _animationController.SetInteger("HurtType", Random.Range(0, 3));
        _animationController.SetTrigger("Hurt");
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
