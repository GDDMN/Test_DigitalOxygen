using UnityEngine;
using System.Collections;

public class ActorMovements : MonoBehaviour
{
    [SerializeField] private Animator _animationController;

    [SerializeField] private AnimationCurve _jumpCurve;

    [SerializeField] private float _walkSpeed;

    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private float _attackDistantion;

    private bool _onGround;
    Vector3 startPosition;
    private float _progress = 0.0f;
    
    public bool IsJumping { get; private set; }

    private void Awake()
    {
        _animationController.SetBool("OnGround", _onGround);
    }

    public void Run(float direction)
    {
        Vector3 startPosition = transform.position;
        transform.position = startPosition + new Vector3(direction, 0.0f, 0.0f) * _walkSpeed * Time.deltaTime;
        Rotate(direction);

        _animationController.SetInteger("Run", (int)direction);
        _animationController.SetBool("OnGround", _onGround);
    }

    private void Rotate(float direction)
    {
        if((int)direction != 0)
            transform.rotation = Quaternion.Euler(0.0f, 90 * direction, 0.0f);
    }

    public void Jump()
    {
        if (!_onGround)
            return;

        _onGround = false;
        IsJumping = true;

        _animationController.SetBool("OnGround", _onGround);
        _progress = 0.0f;
        startPosition = transform.position;
    }

    public void JumpAnimation()
    {
        if (!IsJumping)
            return;

        _progress += _jumpSpeed * Time.deltaTime;
        float jumpEvaluation = _jumpCurve.Evaluate(_progress);
        float deltaYPos = (startPosition.y * jumpEvaluation) * _jumpForce;

        transform.position = new Vector3(transform.position.x,
                                        startPosition.y + deltaYPos,
                                        transform.position.z);

        if (_progress >= 1.0f)
            IsJumping = false;
    }

    public void Hurt()
    {
        StartCoroutine(HurtAnimation());
    }

    private IEnumerator HurtAnimation()
    {
        float progress = 0.0f;
        Vector3 startPosition = transform.position;

        while (progress < 1)
        {
            progress += _jumpSpeed * Time.deltaTime;
            float jumpEvaluation = _jumpCurve.Evaluate(progress) * _jumpForce / 2;
            float verticalMove = startPosition.y * jumpEvaluation;
            
            //transform.position = startPosition + new Vector3(horizontalMove, verticalMove, 0.0f);
            yield return null;
        }
    }

    public void Attack(float direction)
    {
        _animationController.SetTrigger("Attack");
        StartCoroutine(AttackAnimation(direction));
    }

    private IEnumerator AttackAnimation(float direction)
    {
        float progress = 0.0f;

        while(progress <= _attackDistantion)
        {
            progress += 2.0f * Time.deltaTime;
            transform.position += Vector3.right * direction;
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_onGround)
        {
            _onGround = true;
            IsJumping = false;
            _animationController.SetBool("OnGround", _onGround);
        }
    }
}

