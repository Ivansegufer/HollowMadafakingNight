using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerController
{
    public void OnMove(InputValue value)
    {
        var direction = value.Get<Vector2>();
        _movementDirection = direction;

        var isMoving = CheckMovement();

        if (isMoving && inGround) ActivateWalkAnimation();
        else StopWalkAnimation();
    }

    public void OnJump()
    {
        StartJump();
        ActivateJumpAnimation();
    }

    public void OnJumpRelease()
    {
        EndJump();
    }
}