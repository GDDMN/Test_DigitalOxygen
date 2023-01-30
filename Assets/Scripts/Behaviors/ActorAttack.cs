using UnityEngine;

public class ActorAttack : MonoBehaviour
{
    [SerializeField] private Animator _animationController;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _enemyLayer;

    [SerializeField] private float _damage;

    private int _punchType;
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
        int punch;

        do
        {
            punch = Random.Range(0, 3);
        }
        while (_punchType == punch);

        _punchType = punch;
        _animationController.SetInteger("PunchType", _punchType);
        _animationController.SetTrigger("Attack");
    }
}
