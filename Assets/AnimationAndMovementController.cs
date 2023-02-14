using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{

    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    // Start is called before the first frame update
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;


    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight;
    float maxJumpTime;
    bool isJumping = false;


    //players movement and speed 
    float speed = 4.5f;
    float rotationFactorPerFrame = 15.0f;
    float runMultiplier = 4.0f;

    float gravity = -2f;
    float groundedGravity = -0.5f;


    public Vector2 CurrentMovementInput { get => currentMovementInput; set => currentMovementInput = value; }
    public Vector3 CurrentMovement { get => currentMovement; set => currentMovement = value; }
    public Animator Animator { get => animator; set => animator = value; }


    void Awake()
    {
        // initially set reference variables
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");


        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
        playerInput.CharacterControls.Run.started += onJump;
        playerInput.CharacterControls.Run.canceled += onJump;
    }


    void handleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            isJumping = true;
            currentMovement.y = initialJumpVelocity;
            currentRunMovement.y = initialJumpVelocity;
        }else if(!isJumping && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }




    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        Debug.Log(isJumpPressed);
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }

    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>(); // Holds the current input vectors
        currentMovement.x = currentMovementInput.x * speed;
        currentMovement.z = currentMovementInput.y * speed;
        currentRunMovement.x = currentMovementInput.x * speed * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * speed * runMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void Start()
    {

    }

    void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else
        {
            currentMovement.y += gravity * Time.deltaTime;
            currentRunMovement.y += gravity * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {

        handleRotation();
        handleAnimation();
        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }
        handleGravity();
        handleJump();
    }


    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }
    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}


