using UnityEngine;
using UnityEngine.InputSystem;

public enum MovementState
{
    Idle,
    Walking,
    Running,
    Jumping
}

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField] float walkingSpeed = 5f;
    [SerializeField] float runningSpeed = 10f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpForce = 5f;
    public MovementState currentMovementState = MovementState.Idle;

    CharacterController characterController;

    Vector3 moveDirection;
   public  Vector3 gravityVector;
    float currentSpeed;
    bool wantsToJump = false;

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

    void Jump()
    {
        if (wantsToJump)
        {
            gravityVector = transform.up * jumpForce;
            wantsToJump = false;
            currentMovementState = MovementState.Jumping; // set state to jumping once
        }
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

        ChangeMoveState();

        if (!characterController.isGrounded) // apply gravity
        {
            gravityVector.y += gravity * Time.deltaTime;
        }
        else if (characterController.isGrounded)
        {

            Jump();
        }

        characterController.Move((moveDirection * currentSpeed + gravityVector) * Time.deltaTime);
    }

    void ChangeMoveState() // can also be change in Jump()
    {
        if (moveDirection.magnitude == 0)
        {
            currentMovementState = MovementState.Idle;
        }
        else if (moveDirection.magnitude > 0 && currentSpeed == walkingSpeed)
        {
            currentMovementState = MovementState.Walking;
        }
        else if (moveDirection.magnitude > 0 && currentSpeed == runningSpeed)
        {
            currentMovementState = MovementState.Running;
        }
    }


    public void InputJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            wantsToJump = true;
        }
        else
        {
            wantsToJump = false;
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
