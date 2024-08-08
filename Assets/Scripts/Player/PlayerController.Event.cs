using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerController
{
    public void OnMove(InputValue value)
    {
        var direction = value.Get<Vector2>();
        _movementDirection = direction;

        var isMoving = CheckMovement();

        isWalkPressed = isMoving;

        if (isMoving && inGround && canMove) ActivateWalkAnimation();
        else StopWalkAnimation();
    }

    public void OnRoll()
    {
        if (!inGround || !canDash) return;
        
        ActivateRollAnimation();
        Dash();
    }

    public void OnEndRoll()
    {
        EndDash();
    }

    public void OnJump()
    {
        StartJump();
        ActivateJumpAnimation();
    }

    public void OnJumpRelease()
    {
        isJumpPressed = false;
    }

    public void OnAttack()
    {
        StartAttack();
        ActivateAttackAnimation();
    }

    public void OnEndAttack()
    {
        EndAttack();
    }
}