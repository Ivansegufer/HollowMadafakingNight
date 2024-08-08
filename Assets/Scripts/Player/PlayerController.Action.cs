using UnityEngine;

public partial class PlayerController
{
    private static readonly int AttackAnimationId = Animator.StringToHash("Attack");
    private static readonly int WalkAnimationId = Animator.StringToHash("Walk");
    private static readonly int JumpAnimationId = Animator.StringToHash("Jump");
    private static readonly int FallAnimationId = Animator.StringToHash("Fall");
    private static readonly int RollAnimationId = Animator.StringToHash("Roll");
    
    private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");
    private static readonly int AttackX = Animator.StringToHash("AttackX");
    private static readonly int AttackY = Animator.StringToHash("AttackY");

    private void StartAttack()
    {
        isAttacking = true;
        canMove = false;
        canDash = false;
        
        _animator.SetFloat(AttackX, _movementDirection.x);
        _animator.SetFloat(AttackY, _movementDirection.y);
        
        StopWalkAnimation();
        StopJumpAnimation();
        StopFallAnimation();

        if (inGround)
        {
            ResetHorizontalMovement();
        }
    }

    private void ActivateAttackAnimation()
    {
        _animator.SetBool(AttackAnimationId, true);
    }

    private void StopAttackAnimation()
    {
        _animator.SetBool(AttackAnimationId, false);
    }

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