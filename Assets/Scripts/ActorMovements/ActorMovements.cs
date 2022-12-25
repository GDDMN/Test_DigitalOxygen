using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class ActorMovements : MonoBehaviour
{
    [SerializeField] private Animator _animationController;

    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private float _walkSpeed;

    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _jumpForce;

    private bool _onGround;

    private void Awake()
    {
        
    }

    public void Run(float direction)
    {

        Vector3 startPosition = transform.position;
        transform.position = startPosition + new Vector3(direction, 0.0f, 0.0f) * _walkSpeed * Time.deltaTime;
        Rotate(direction);
    }

    private void Rotate(float direction)
    {
        if((int)direction != 0)
            transform.rotation = Quaternion.Euler(0.0f, 90 * (int)direction, 0.0f);
    }

    public void Jump(float direction)
    {
        if (!_onGround)
            return;

        _onGround = false;
        StartCoroutine(JumpAnimation(direction));
    }

    private IEnumerator JumpAnimation(float direction)
    {
        float progress = 0.0f;
        float horizontalMove = 0.0f;
        Vector3 startPosition = transform.position;

        while (progress < 1)
        {
            progress += _jumpSpeed * Time.deltaTime;
            float jumpEvaluation = _jumpCurve.Evaluate(progress) * _jumpForce;
            

            float verticalMove = startPosition.y * jumpEvaluation;
            horizontalMove += (direction * _walkSpeed * Time.deltaTime);

            transform.position = startPosition + new Vector3(horizontalMove, verticalMove, 0.0f);
            yield return null;
        }
    }

    public void Hurt()
    {

    }

    public void Attack()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_onGround)
            _onGround = true;
    }
}
