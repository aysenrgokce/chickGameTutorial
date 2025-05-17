using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerJumped;
    public event Action OnPlayerRolled;  // Yeni event

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

    [Header("Takla Ayarlari")]  // Yeni bölüm
    [SerializeField] private KeyCode _rollKey;
    [SerializeField] private float _rollDuration = 0.7f;
    private bool _isRolling = false;

    [Header("Zemin Kontrol Ayarları")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _graundLayer;
    [SerializeField] private float _groundDrag;

    private StateController _stateController;
    private Rigidbody _playerRigidbody;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;

    private void Awake()
    {
        _stateController = GetComponent<StateController>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
        _canJump = true;
    }

    private void Update()
    {
        SetInputs();
        SetStates();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }

    private void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        // Kayma
        if (Input.GetKeyDown(_slideKey))
        {
            _isRolling = false; // Takla iptal
            _stateController.ChangeState(PlayerState.Slide);
        }
        else if (Input.GetKeyUp(_slideKey) || Input.GetKeyUp(_momentKey))
        {
            if (!_isRolling) // Eğer takla değilse kaymayı kapat
                _stateController.ChangeState(PlayerState.Idle);
        }

        // Takla
        if (Input.GetKeyDown(_rollKey) && !_isRolling && IsGrounded())
        {
            StartCoroutine(StartRoll());
        }

        // Zıplama
        if (Input.GetKey(_jumKey) && _canJump && IsGrounded())
        {
            _canJump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), _jumpCooldown);
        }
    }

    private System.Collections.IEnumerator StartRoll()
    {
        _isRolling = true;
        _stateController.ChangeState(PlayerState.Roll);

        OnPlayerRolled?.Invoke();

        float startTime = Time.time;

        while (Time.time < startTime + _rollDuration)
        {
            // Takla hareketi - örnek basit hareket, ileri doğru hızlandırma:
            Vector3 rollDirection = _orientationTransform.forward;
            _playerRigidbody.velocity = new Vector3(
                rollDirection.x * _movementSpeed * 1.5f,
                _playerRigidbody.velocity.y,
                rollDirection.z * _movementSpeed * 1.5f);
            yield return null;
        }

        _isRolling = false;
        _stateController.ChangeState(PlayerState.Idle);
    }

    private void SetStates()
    {
        if (_isRolling) return; // Takla varken diğer durumlar değişmesin

        var movementDirection = GetMomentDirection();
        var isGrounded = IsGrounded();
        var isSliding = Input.GetKey(_slideKey);
        var currentState = _stateController.GetCurrentState();

        var newState = currentState switch
        {
            _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
            _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            _ when !_canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };

        if (newState != currentState)
        {
            _stateController.ChangeState(newState);
        }
    }

    private void SetPlayerMovement()
    {
        if (_isRolling) return; // Takla hareketi Coroutine içinde

        _movementDirection = _orientationTransform.forward * _verticalInput
                            + _orientationTransform.right * _horizontalInput;

        float forceMultipler = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
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

    private void SetPlayerDrag()
    {
        if (_isRolling)
        {
            _playerRigidbody.drag = 0f; // Takla sırasında sürtünme sıfır
            return;
        }

        _playerRigidbody.drag = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => _groundDrag,
            PlayerState.Slide => _slideDrag,
            PlayerState.Jump => _airDrag,
            _ => 0f
        };
    }

    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidbody.velocity.x, 0f, _playerRigidbody.velocity.z);
        if (flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            _playerRigidbody.velocity = new Vector3(limitedVelocity.x, _playerRigidbody.velocity.y, limitedVelocity.z);
        }
    }

    private void SetPlayerJumping()
    {
        _playerRigidbody.AddForce(Vector3.up * _jumForce, ForceMode.Impulse);
        OnPlayerJumped?.Invoke();
    }

    private void ResetJumping()
    {
        _canJump = true;
    }

    private Vector3 GetMomentDirection()
    {
        return new Vector3(_horizontalInput, 0f, _verticalInput);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _graundLayer);
    }
}
