using UnityEngine;

public class ActorAttack : MonoBehaviour
{
    [SerializeField] private Animator _animationController;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _enemyLayer;

    [SerializeField] private float _damage;

    [SerializeField, Range(0f, 1.5f)] private float _stamina;
    [SerializeField] private float _punchFatigue;
    [SerializeField] private float _punchRecovery;

    private int _punchType;
    public float Damage => _damage;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    private void Update()
    {
        if (_stamina > 0.0f)
            _stamina -= _punchRecovery * Time.deltaTime;
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

        if (_stamina > 1.0f)
            return;

        if (_stamina < 1.5f)
            _stamina += _punchFatigue;

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
