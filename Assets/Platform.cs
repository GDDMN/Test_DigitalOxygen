using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Vertex vertex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 9)
            return;
        
        vertex.AddActorAction.Invoke(other.GetComponent<Actor>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 9)
            return;

        vertex.RemoveActorAction.Invoke(other.GetComponent<Actor>());
    }
}
