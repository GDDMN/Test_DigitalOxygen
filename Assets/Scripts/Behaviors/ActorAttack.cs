using UnityEngine;

public class ActorAttack : MonoBehaviour
{
    [SerializeField] private Animator _animationController;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _enemyLayer;

    [SerializeField] private float _damage;


    public bool IsAttacking { get; private set; }
    public float Damage => _damage;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    public void Attack()
    {
        Collider[] hit = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayer);

        foreach (Collider hitted in hit)
            hitted.GetComponent<IInteractable>().Interact(GetComponent<Collider>());
    }

    public void StartAttack()
    {
        if (IsAttacking)
            return;

        _animationController.SetInteger("PunchType", Random.Range(0, 3));
        _animationController.SetTrigger("Attack");
        IsAttacking = true;
    }

    public void EndAttack()
    {
        IsAttacking = false;
    }


}
