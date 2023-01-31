using UnityEngine;
public class WaitNode : ActionNode
{
    public float duration = 1;
    private float _startTime;
    protected override void OnStart()
    {
        _startTime = Time.time;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(Time.time - _startTime > duration)
            return State.SUCCESS;

        return State.RUNNING;
    }
}
