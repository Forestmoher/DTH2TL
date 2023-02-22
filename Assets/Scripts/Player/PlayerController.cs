using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool _isSprinting;
    [SerializeField] private bool _isCrouching;
    private float _walkSpeed = 3;
    private float _sprintSpeed = 6;
    private float _currentMoveSpeed;
    private float _walkTurnSmoothTime = .1f;
    private float _sprintTurnSmoothTime = .05f;
    private float _currentTurnSmoothTime;
    private float _turnSmoothVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;
    public float Gravity = 15.0f;

    //private Rigidbody _rb;
    private CharacterController _controller;
    private Transform _camera;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _sprintAction;
    private InputAction _crouchAction;
    //[HideInInspector] public InputAction interactAction;
    private PlayerAnimator _animator;
    private Vector2 _input;
    public string currentRoom;

    void Awake()
    {
        _camera = Camera.main.transform;
        
        //_rb = GetComponent<Rigidbody>();
        _controller = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<PlayerAnimator>();
        _moveAction = _playerInput.actions["Move"];
        _sprintAction = _playerInput.actions["Sprint"];
        _crouchAction = _playerInput.actions["Crouch"];
        //interactAction = _playerInput.actions["Interact"];
        _isSprinting = false;
        _isCrouching = false;
    }

    private void Update()
    {
        GetInput();
        HandleSpeed();
    }

    private void GetInput()
    {
        _input = _moveAction.ReadValue<Vector2>();

        //if not currently crouching or sprinting, and crouch key is pressed, then crouch
        if(_crouchAction.triggered && !_isCrouching && !_isSprinting)
        {
            _isCrouching = true;
            return;
        }
        //if currently crouching and crouch key is pressed, stop crouching
        if(_crouchAction.triggered && _isCrouching)
        {
            _isCrouching = false;
        }
        //if currently crouching and sprint key is pressed, stop crouching and sprint
        if(_isCrouching && _sprintAction.IsPressed())
        {
            _isCrouching = false;
            _isSprinting = true;
        }
        //if not currently crouching and sprint key pressed, then sprint
        _isSprinting = _sprintAction.IsPressed() && !_isCrouching;
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        HandleMovementAndRotation();
        _animator.HandleMovementAnimation(_input.magnitude, _isSprinting, _isCrouching);
    }

    private void HandleSpeed()
    {
        _currentMoveSpeed = _isSprinting ? _sprintSpeed : _walkSpeed;
        _currentTurnSmoothTime = _isSprinting ? _sprintTurnSmoothTime : _walkTurnSmoothTime;
    }

    private void HandleMovementAndRotation()
    {
        Vector3 direction = new Vector3(_input.x, 0f, _input.y).normalized;
        if(direction.magnitude >= 0.1f)
        {
            float targeAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targeAngle, ref _turnSmoothVelocity, _currentTurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targeAngle, 0f) * Vector3.forward;
            //_rb.MovePosition(moveDirection.normalized * _currentMoveSpeed * Time.deltaTime);
            _controller.Move(moveDirection.normalized * (_currentMoveSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }

    }

    private void ApplyGravity()
    {
        // stop our velocity dropping infinitely when grounded
        if (_verticalVelocity < 0.0f)
        {
            _verticalVelocity = -2f;
        }
        // apply gravity over time 
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }

    }

}
