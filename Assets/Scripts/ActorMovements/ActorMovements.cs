using UnityEngine;

public class ActorMovements : MonoBehaviour
{
    [SerializeField] private Rigidbody _actorBody;
    [SerializeField] private Animator _animationController;
    
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private float _walkSpeed;
    private bool _onGround;

    public void Run()
    {

    }

    public void Jump()
    {
        if (!_onGround)
            return;


    }

    public void Hurt()
    {

    }

    public void Attack()
    {

    }

}
