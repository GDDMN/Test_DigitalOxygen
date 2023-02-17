using UnityEngine;

public class FingtingNode : ActionNode
{
    public EnemyController enemy;
    private float attackTime = 0.0f;
    private float coolDownTme = 1.0f;
    private float attackSpeed = 2.0f;

    public override void SetActor(ref Actor actor)
    {
        enemy = actor as EnemyController;
    }

    protected override void OnStart()
    {
        attackTime = 0.0f;
    }

    protected override void OnStop()
    {
        attackTime = 0.0f;
    }

    protected override State OnUpdate()
    {
        if (enemy.player == null)
            return State.SUCCESS;

        float distantion = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);
        
        if (distantion > 2.0f)
            return State.FAILURE;

        attackTime += attackSpeed * Time.deltaTime;
        if(attackTime > coolDownTme)
        {
            enemy.Attack();
            attackTime = 0.0f;
        }

        return State.RUNNING;
    }
}
