using UnityEngine;

public class RepeatNode : DecoratorNode
{
    public override void SetActor(ref Actor actor)
    {
    }

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        child.Update();
        return State.RUNNING;
    }
}

