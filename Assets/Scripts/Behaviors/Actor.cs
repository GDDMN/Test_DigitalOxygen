using System;
using UnityEngine;

[Serializable]
public struct ActorData 
{
    [Range(0f, 1f)]
    public float Health;
}

public class Actor : MonoBehaviour
{
    public ActorData actorData;
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<IInteractable>().Interact(other.GetComponent<Collider>());
    }
}
