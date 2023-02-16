using UnityEngine;

public class DeadGround : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 9)
            return;

        other.GetComponent<Actor>().Death();
    }
}
