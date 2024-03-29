using UnityEngine;

public abstract class Node : ScriptableObject
{
    public enum State
    {
        RUNNING,
        FAILURE,
        SUCCESS
    }

    public State state = State.RUNNING;
    public bool started = false;
    public string guid;
    public Vector2 position;

    public State Update()
    {
        if (!started)
        {
            OnStart();
            started = true;
        }

        state = OnUpdate();

        if (state == State.FAILURE || state == State.SUCCESS)
        {
            OnStop();
            started = false;
        }

        return state;
    }

    public virtual Node Clone()
    {
        return Instantiate(this);
    }

    public abstract void SetActor(ref Actor actor);
    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract State OnUpdate();
}
