using UnityEngine;

public class ActorMovements : MonoBehaviour
{
    [SerializeField] private Animator _animationController;

    [SerializeField] private AnimationCurve _jumpCurve;

    [SerializeField] private float _walkSpeed;

    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private Transform _groundCheckPoint;

    private bool _onGround;
    private Vector3 _startPosition;
    private float _progress = 0.0f;

    public bool IsJumping { get; private set; }


    private void Awake()
    {
        _animationController.SetBool("OnGround", _onGround);
    }

    private void Update()
    {
        OnGround();
    }

    public void Run(float direction)
    {
        Vector3 startPosition = transform.position;
        transform.position = startPosition + new Vector3(direction, 0.0f, 0.0f) * _walkSpeed * Time.deltaTime;
        Rotate(direction);
 
        _animationController.SetFloat("Run", Mathf.Abs(direction));
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

        IsJumping = true;

        
        _progress = 0.0f;
        _startPosition = transform.position;
    }

    public void JumpAnimation()
    {
        if (!IsJumping)
            return;

        _progress += _jumpSpeed * Time.deltaTime;
        float jumpEvaluation = _jumpCurve.Evaluate(_progress);
        float deltaYPos = _startPosition.y + (jumpEvaluation *_jumpForce);

        transform.position = new Vector3(transform.position.x,
                                        deltaYPos,
                                        transform.position.z);

        if (_progress >= 1.0f)
            IsJumping = false;
    }

    private void OnGround()
    {
        float distance = 1.5f;
        
        Ray ray = new Ray(_groundCheckPoint.position, Vector3.down);
        _onGround = Physics.Raycast(ray, distance);
        _animationController.SetBool("OnGround", _onGround);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsJumping = false;
    }
}