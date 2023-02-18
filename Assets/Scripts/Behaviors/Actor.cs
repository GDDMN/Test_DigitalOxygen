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
    [Header("Actor data")]
    public ActorData actorData;

    [HideInInspector] public Vertex vertex;
    [HideInInspector] public UnityAction groundedOnPlatform;
    [HideInInspector] public UnityAction getHurt;
    [HideInInspector] public UnityAction OnDeath;
    [HideInInspector] public bool Hurt;

    abstract public void Death();
}
