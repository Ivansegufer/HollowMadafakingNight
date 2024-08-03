using UnityEngine;

public partial class PlayerController
{
    private void Grip()
    {
        var previousValue = inGround;
        inGround = Physics2D.OverlapCircle((Vector2)_transform.position + down, colliderRadius, ground);

        if (previousValue || !inGround) return;
        
        EndJump();
        StopJumpAnimation();
    }
}