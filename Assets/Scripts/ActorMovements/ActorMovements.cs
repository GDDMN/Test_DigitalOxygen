using UnityEngine;
using System.Collections.Generic;
using System;

public class ActorMovements : MonoBehaviour
{
    [SerializeField] private Rigidbody _actorBody;
    [SerializeField] private Animator _animationController;

    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private float _walkSpeed;

    [HideInInspector] public bool OnGround { get; private set; }

    private void Awake()
    {
        
    }

    public void Run(float direction)
    {
        if (!OnGround)
            return;

        _actorBody.velocity = new Vector3(direction, 0.0f, 0.0f) * _walkSpeed * Time.deltaTime;
        Rotate(direction);

        if (direction != 0)
            _animationController.SetBool("Run", true);
        else
            _animationController.SetBool("Run", false);
    }

    private void Rotate(float direction)
    {
        if((int)direction != 0)
            _actorBody.transform.rotation = Quaternion.Euler(0.0f, 90 * (int)direction, 0.0f);
    }

    public void Jump()
    {
        if (!OnGround)
            return;

        OnGround = false;

        Debug.Log("Jump");
    }

    public void Hurt()
    {

    }

    public void Attack()
    {
        Debug.Log("Attack");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!OnGround)
            OnGround = true;
    }
}
