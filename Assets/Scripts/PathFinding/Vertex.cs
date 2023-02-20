using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vertex : MonoBehaviour
{
    [SerializeField] private List<Vertex> reachable = new List<Vertex>();
    [SerializeField] private List<Actor> actors = new List<Actor>();
    
    public UnityAction<Actor> AddActorAction;
    public UnityAction<Actor> RemoveActorAction;

    private void Start()
    {
        AddActorAction += AddActor;
        RemoveActorAction += RemoveActor;
    }

    private void OnDestroy()
    {
        AddActorAction -= AddActor;
        RemoveActorAction -= RemoveActor;
    }

    private void OnDisable()
    {
        AddActorAction -= AddActor;
        RemoveActorAction -= RemoveActor;
    }

    private void AddActor(Actor actor)
    {
        actor.vertex = this;
        actors.Add(actor);
    }
    
    private void RemoveActor(Actor actor)
    {
        actor.vertex = null;
        actors.Remove(actor);
    }

    public List<Vertex> GetReachable()
    {
        return reachable;
    }
}
