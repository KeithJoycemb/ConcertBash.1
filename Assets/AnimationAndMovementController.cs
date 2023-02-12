using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{

    PlayerInput playerInput;
    CharacterController characterController;

    // Start is called before the first frame update
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;

    public Vector2 CurrentMovementInput { get => currentMovementInput; set => currentMovementInput = value; }
    public Vector3 CurrentMovement { get => currentMovement; set => currentMovement = value; }

    void Awake()
    {
        // initially set reference variables
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
    }

    void onMovementInput (InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>(); // Holds the current input vectors
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move(CurrentMovement* Time.deltaTime);
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

