using UnityEngine;

public partial class PlayerController
{
    private static readonly int WalkAnimationId = Animator.StringToHash("Walk");
    private static readonly int JumpAnimationId = Animator.StringToHash("Jump");
    private static readonly int FallAnimationId = Animator.StringToHash("Fall");
    private static readonly int RollAnimationId = Animator.StringToHash("Roll");
    private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");

    private void ActivateJumpAnimation()
    {
        _animator.SetBool(JumpAnimationId, true);
    }

    private void StartJump()
    {
        isJumpPressed = true;
        isJumping = true;
    }

    private void EndJump()
    {
        isJumpPressed = false;
        isJumping = false;
    }

    private void StopJumpAnimation()
    {
        _animator.SetBool(JumpAnimationId, false);
    }

    private void SetVerticalVelocity()
    {
        _animator.SetFloat(VerticalVelocity, _rb.velocity.y);
    }

    private void ActivateWalkAnimation()
    {
        _animator.SetBool(WalkAnimationId, true);
    }

    private void StopWalkAnimation()
    {
        _animator.SetBool(WalkAnimationId, false);
    }

    private void ActivateFallAnimation()
    {
        _animator.SetBool(FallAnimationId, true);
    }

    private void StopFallAnimation()
    {
        _animator.SetBool(FallAnimationId, false);
    }

    private void ActivateRollAnimation()
    {
        _animator.SetTrigger(RollAnimationId);
    }

    private void StartFall()
    {
        isFalling = true;
    }

    private void EndFall()
    {
        isFalling = false;
    }
}