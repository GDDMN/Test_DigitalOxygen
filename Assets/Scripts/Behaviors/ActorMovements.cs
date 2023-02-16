using UnityEngine;

public class ActorMovements : MonoBehaviour
{
    [SerializeField] private Transform _actorObject;

    [SerializeField] private Animator _animationController;

    [SerializeField] private AnimationCurve _jumpCurve;

    [SerializeField] private float _walkSpeed;

    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _jumpForce;

    public Transform _groundCheckPoint;

    [SerializeField] private ParticleSystem _landingParticle;

    private bool _onGround = false;
    private Vector3 _startPosition;
    private float _progress = 0.0f;

    public bool OnGround => _onGround;
    public bool IsJumping { get; private set; }


    private void Awake()
    {
        _animationController.SetBool("OnGround", _onGround);
    }

    private void Update()
    {
        OnGroundCheck();
    }

    public void Run(float direction)
    {
        Vector3 startPosition = _actorObject.position;
        _actorObject.position = startPosition + new Vector3(direction, 0.0f, 0.0f) * _walkSpeed * Time.deltaTime;
        Rotate(direction);
 
        _animationController.SetFloat("Run", Mathf.Abs(direction));
        _animationController.SetBool("OnGround", _onGround);
    }

    private void Rotate(float direction)
    {
        if((int)direction != 0)
            _actorObject.rotation = Quaternion.Euler(0.0f, 90 * direction, 0.0f);
    }

    public void Jump()
    {
        if (!_onGround)
            return;

        IsJumping = true;

        _progress = 0.0f;
        _startPosition = _actorObject.position;
    }

    public void JumpAnimation()
    {
        if (!IsJumping)
            return;

        _progress += _jumpSpeed * Time.deltaTime;
        float jumpEvaluation = _jumpCurve.Evaluate(_progress);
        float deltaYPos = _startPosition.y + (jumpEvaluation *_jumpForce);

        _actorObject.position = new Vector3(_actorObject.position.x,
                                            deltaYPos,
                                            _actorObject.position.z);

        if (_progress >= 1.0f)
            IsJumping = false;
    }

    public void PlayLandingEffect()
    {
        Instantiate(_landingParticle, new Vector3(_groundCheckPoint.position.x, 
                                                  _groundCheckPoint.position.y-1,
                                                  _groundCheckPoint.position.z), Quaternion.identity);
    }

    private void OnGroundCheck()
    {
        float distance = 1.5f;

        Ray ray = new Ray(_groundCheckPoint.position, Vector3.down);
        _onGround = Physics.Raycast(ray,distance);

        _animationController.SetBool("OnGround", _onGround);
    }

    private void OnTriggerEnter(Collider other)
    {
        IsJumping = false;
    }
}