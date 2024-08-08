public partial class PlayerController
{
    private bool CheckMovement()
    {
        return _movementDirection.x != 0;
    }
}