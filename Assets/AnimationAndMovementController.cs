using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{

    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;

    int _isWalkingHash;
    int _isRunningHash;

    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _appliedMovement;
    bool _isMovementPressed;
    bool _isRunPressed;

    //jump
    bool _isJumpPressed = false;
    float _initialJumpVelocity;
    float _maxJumpHeight=2.0f;
    float _maxJumpTime =1f;
    bool _isJumping = false;
    int _isJumpingHash;
    bool _isJumpAnimating = false;
    int _jumpCount = 0;
    int _jumpCountHash;
    float _playerDefaultSpeed = 4.6f;

    //players movement and speed 
    float _rotationFactorPerFrame = 15.0f;
    float _runMultiplier = 5.0f;
    

    //gravity
    float _gravity = -9.8f;
    float _groundedGravity = -0.5f;
    int zero = 0;




    void Awake()
    {
        // initially set reference variables
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _jumpCountHash = Animator.StringToHash("jumpCount");

        _playerInput.CharacterControls.Move.started += onMovementInput;
        _playerInput.CharacterControls.Move.canceled += onMovementInput;
        _playerInput.CharacterControls.Move.performed += onMovementInput;
        _playerInput.CharacterControls.Run.started += onRun;
        _playerInput.CharacterControls.Run.canceled += onRun;
        _playerInput.CharacterControls.Jump.started += onJump;
        _playerInput.CharacterControls.Jump.canceled += onJump;

        setupJumpVariables();
    }
    void onJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }

    void onRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    void handleJump()
    {
        if (!_isJumping && _characterController.isGrounded && _isJumpPressed)
        {
            _animator.SetBool(_isJumpingHash, true);
            _isJumpAnimating = true;
            _isJumping = true;
            _jumpCount += 1;
            _animator.SetInteger(_jumpCountHash, _jumpCount);
            _currentMovement.y = _initialJumpVelocity;
            _appliedMovement.y = _initialJumpVelocity;
        }else if(!_isJumpPressed && _isJumping && _characterController.isGrounded)
        {
            _isJumping = false;
        }
        if (_jumpCount == 3)
        {
            _jumpCount = 0;
        }
    }

    void setupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
    }

   
    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = _currentMovementInput.x;
        positionToLookAt.y = zero;
        positionToLookAt.z = _currentMovementInput.y;
        Quaternion currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }

    }

    void Start()
    {

    }

    void handleAnimation()
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isRunning = _animator.GetBool(_isRunningHash);

        if (_isMovementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        else if (!_isMovementPressed && isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }

        if ((_isMovementPressed && _isRunPressed) && !isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
        }
        else if ((!_isMovementPressed || !_isRunPressed) && isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
        }
    }

    void handleGravity()
    {
        bool isFalling = _currentMovement.y <= 0.0f||!_isJumpPressed;
        float fallMultiplier = 2.0f;
        if (_characterController.isGrounded)
        {
            if (_isJumpAnimating)
            {
                _animator.SetBool(_isJumpingHash, false);
                _isJumpAnimating = false;
            }
            _animator.SetBool("isJumping", false);
            _currentMovement.y = _groundedGravity;
            _appliedMovement.y = _groundedGravity;
        }
        else if (isFalling)
        {
            float previousYVelocity = _currentMovement.y;
            _currentMovement.y = _currentMovement.y + (_gravity * fallMultiplier * Time.deltaTime);
            _appliedMovement.y = Mathf.Max((previousYVelocity + _currentMovement.y) * .5f,-20.0f);
        }
        else
        {
            float previousYVelocity = _currentMovement.y;
            _currentMovement.y =  _currentMovement.y + (_gravity * Time.deltaTime);
            _appliedMovement.y = (previousYVelocity + _currentMovement.y) * .5f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        handleRotation();
        handleAnimation();

        _appliedMovement.x = _isRunPressed ?_currentMovementInput.x*_runMultiplier:_currentMovementInput.x;
        _appliedMovement.z = _isRunPressed ? _currentMovementInput.y * _runMultiplier : _currentMovementInput.y;
        _characterController.Move((_appliedMovement* _playerDefaultSpeed) * Time.deltaTime);
        handleGravity();
        handleJump();
    }
     void onMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>(); 
        _isMovementPressed = _currentMovementInput.x != zero || _currentMovementInput.y != zero;
    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }
    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }
}


