using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct ActorData 
{
    [Range(0f, 1f)]
    public float Health;
}

public abstract class Actor : MonoBehaviour
{
    public ActorData actorData;
    public Vertex vertex;
    public UnityAction groundedOnPlatform;
    public bool Dead;

    abstract public void Death();
}
