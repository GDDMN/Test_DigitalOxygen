public class SequencerNode : CompositeNode
{
    private int _current;
    protected override void OnStart()
    {
        _current = 0;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        var child = children[_current];
        switch (child.Update())
        {
            case State.RUNNING:
                return State.RUNNING;
                break;
            case State.FAILURE:
                return State.FAILURE;
                break;
            case State.SUCCESS:
                _current++;
                break;
        }

        return _current == children.Count ? State.SUCCESS : State.RUNNING;
    }
}
