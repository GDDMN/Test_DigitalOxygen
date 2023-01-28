using System;
using UnityEngine;

[Serializable]
public struct ActorData 
{
    [Range(0f, 1f)]
    public float Health;
}

public abstract class Actor : MonoBehaviour
{
    public ActorData actorData;

    abstract public void Death();
}
