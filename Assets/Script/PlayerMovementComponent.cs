using UnityEngine;
using UnityEngine.InputSystem;

public enum MovementState
{
    Idle,
    Walking,
    Running
}

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField] float walkingSpeed = 5f;
    [SerializeField] float runningSpeed = 10f;
    [SerializeField] float gravity = -9.81f;
    public MovementState currentMovementState = MovementState.Idle;

    CharacterController characterController;

    Vector3 moveDirection;
    Vector3 gravityVector;
    float currentSpeed;

    Vector2 moveInput;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        currentSpeed = walkingSpeed; // base speed
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if(moveInput.x < 0) // rotate left
        {
            transform.rotation = Quaternion.Euler(0, 360, 0);
        }
        else if (moveInput.x > 0) // rotate right
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (!characterController.isGrounded) // apply gravity
        {
            gravityVector.y += gravity * Time.deltaTime;
        }
        else
        {
            gravityVector.y = 0;
        }

        ChangeMoveState();

        characterController.Move((moveDirection * currentSpeed + gravityVector) * Time.deltaTime);
    }

    void ChangeMoveState()
   {
        if (moveDirection.magnitude == 0)
        {
            currentMovementState = MovementState.Idle;
        }
        else
        if (moveDirection.magnitude > 0 && currentSpeed == walkingSpeed)
        {
            currentMovementState = MovementState.Walking;
        }
        else if (moveDirection.magnitude > 0 && currentSpeed == runningSpeed)
        {
            currentMovementState = MovementState.Running;
        }
    }


    public void InputRun(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            currentSpeed = runningSpeed;
        }
        else if (ctx.canceled)
        {
            currentSpeed = walkingSpeed;
        }
    }

    public void InputMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }
}
