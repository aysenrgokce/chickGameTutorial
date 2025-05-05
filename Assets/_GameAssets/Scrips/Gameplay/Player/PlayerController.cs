using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    [Header("References")]
    [SerializeField] private Transform _orientationTransform;

    [Header("Hareket Ayarlari")]
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private KeyCode _momentKey;

    [Header("Atlama Ayarlari")]
    [SerializeField] private KeyCode _jumKey;
    [SerializeField] private float _jumForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private bool _canJump;
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _airDrag;
    
    [Header("Kayma Ayarlari")]
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _sliderMultiplier;
    [SerializeField] private float _slideDrag;

    [Header("Zemin Kontrol Ayarları")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _graundLayer;
    [SerializeField] private float _groundDrag;

    private StateController _stateController;
    private Rigidbody _playerRigidbody;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;
    private bool _isSliding;

 private void Awake()
    {
        _stateController = GetComponent<StateController>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
        _canJump = true;
    }

    [System.Obsolete]
    private void Update()
    {
        SetInputs();
        SetStates();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        SetPlayerMovement();
    }

    [System.Obsolete]
    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(_slideKey))
        {
            _isSliding = true;
        }
        else if(Input.GetKeyUp(_slideKey) || Input.GetKeyUp(_momentKey))
        {
            _isSliding = false;
        }

        if(Input.GetKey(_jumKey) && _canJump && IsGrounded())
        {
            _canJump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), _jumpCooldown);
        }
    }

    private void SetStates()
    {
        var movementDirection = GetMomentDirection();
        var isGrounded = IsGrounded();
        var isSliding = IsSliding();
        var currentState = _stateController.GetCurrentState();

        var newState = currentState switch 
        {
            _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Mode,
            _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            _ when !_canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };

        if(newState != currentState)
        {
            _stateController.ChangeState(newState);
        }
    }

    [System.Obsolete]
    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput
                            + _orientationTransform.right * _horizontalInput;

        float forceMultipler = _stateController.GetCurrentState() switch{
            PlayerState.Mode => 1f,
            PlayerState.Slide => _sliderMultiplier,
            PlayerState.Jump => _airMultiplier,
            _ => 1f      
        };
        
        Vector3 normalizedMovement = _movementDirection.normalized;
        _playerRigidbody.velocity = new Vector3(
            normalizedMovement.x * _movementSpeed * forceMultipler,
            _playerRigidbody.velocity.y,
            normalizedMovement.z * _movementSpeed * forceMultipler);
    }

    [System.Obsolete]
    private void SetPlayerDrag()
    {
        _playerRigidbody.drag = _stateController.GetCurrentState() switch
        {
            PlayerState.Mode => _groundDrag,
            PlayerState.Slide => _slideDrag,
            PlayerState.Jump => _airDrag,
            _ => 0f   
        };
    }

    [System.Obsolete]
    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidbody.velocity.x, 0f, _playerRigidbody.velocity.z);
        if(flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            _playerRigidbody.velocity = new Vector3(limitedVelocity.x, _playerRigidbody.velocity.y, limitedVelocity.z);
        }
    }

    [System.Obsolete]
    private void SetPlayerJumping()
    {
        _playerRigidbody.velocity = new Vector3(_playerRigidbody.velocity.x, 0f, _playerRigidbody.velocity.z);
        _playerRigidbody.AddForce(transform.up * _jumForce, ForceMode.Impulse);
    }

    private void ResetJumping()
    {
        _canJump = true;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _graundLayer);
    }

    private Vector3 GetMomentDirection()
    {
        return _movementDirection.normalized;
    }

    private bool IsSliding()
    {
        return _isSliding;
    }
}
