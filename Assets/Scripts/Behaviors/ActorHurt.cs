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

        TakeDamageCalculation();
        TakeDamageAnimation();
    }
   
    private void TakeDamageCalculation()
    {
        Debug.Log("Hurt");
    }
    private void Die()
    {
        Debug.Log("Die");
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

        _animationController.SetBool("Hurt", false);
    }
}
