using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{
    public List<Vertex> reachable = new List<Vertex>();
    public List<Actor> actors = new List<Actor>();

    private void OnTriggerEnter(Collider other)
    {
        var layer = other.gameObject.layer;

        if (layer == 9)
        {
            actors.Add(other.GetComponent<Actor>());
            other.GetComponent<Actor>().onDeath += RemoveDeadActor;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        var layer = other.gameObject.layer;

        if (layer == 9)
        {
            actors.Remove(other.GetComponent<Actor>());
            other.GetComponent<Actor>().onDeath -= RemoveDeadActor;
        }
    }

    private void RemoveDeadActor(Actor actor)
    {
        actors.Remove(actor);
    }
}
