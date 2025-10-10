using UnityEngine;

public class PlayerAnimationComponent : MonoBehaviour
{
    PlayerMovementComponent playerMovementComponent;
    Animator animator;
    MovementState currentMovementState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovementComponent = GetComponent<PlayerMovementComponent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentMovementState = playerMovementComponent.currentMovementState;
        ChangeMoveState();
    }

    void ChangeMoveState()
    {
        switch (currentMovementState)
        {
            case MovementState.Idle:
                DeactivateWalking();
                DeactivateRunning();
                break;
            case MovementState.Walking:
                ActivateWalking();
                DeactivateRunning();
                break;
            case MovementState.Running:
                ActivateRunning();
                DeactivateWalking();
                break;
            default:
                DeactivateWalking();
                DeactivateRunning();
                break;
        }
    }

    void ActivateRunning()
    {
        animator.SetBool("isRunning", true);
    }

    void DeactivateRunning()
    {
        animator.SetBool("isRunning", false);
    }

    void ActivateWalking()
    {
        animator.SetBool("isWalking", true);
    }

    void DeactivateWalking()
    {
        animator.SetBool("isWalking", false);
    }

    void ActivateDeath()
    {
        animator.SetTrigger("isDead");
    }
}
