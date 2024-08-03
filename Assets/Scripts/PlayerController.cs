using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [Header("Parameters")]
    public float movementSpeed;
    public float jumpForce;

    [Header("Colliders")]
    public LayerMask ground;
    public Vector2 down;
    public float colliderRadius;
    
    [Header("Flags")]
    public bool canMove = true;
    public bool inGround = true;
    public bool isJumpPressed = false;

    private Rigidbody2D _rb;
    private Transform _transform;
    private Vector2 _movementDirection;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        Move();
        Grip();
    }

    private void Move()
    {
        Walk();
        ImproveJump();
        Jump();
    }

    private void Walk()
    {
        if (!canMove) return;
        
        RotatePlayer();
        _rb.velocity = new Vector2(_movementDirection.x * movementSpeed, _rb.velocity.y);
    }

    private void RotatePlayer()
    {
        if (_movementDirection == Vector2.zero) return;

        _transform.localScale = _movementDirection.x switch
        {
            < 0 when _transform.localScale.x > 0 => new Vector3(-_transform.localScale.x, _transform.localScale.y,
                _transform.localScale.z),
            > 0 when _transform.localScale.x < 0 => new Vector3(-_transform.localScale.x, _transform.localScale.y,
                _transform.localScale.z),
            _ => _transform.localScale
        };
    }

    private void ImproveJump()
    {
        switch (_rb.velocity.y)
        {
            case < 0:
                _rb.velocity += Vector2.up * (Physics2D.gravity.y * (2.5f - 1) * Time.deltaTime);
                break;
            case > 0 when !isJumpPressed:
                _rb.velocity += Vector2.up * (Physics2D.gravity.y * (2.0f - 1) * Time.deltaTime);
                break;
        }
    }
    
    private void Jump()
    {
        if (!inGround || !isJumpPressed) return;
        
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.velocity += Vector2.up * jumpForce;
        
        SetVerticalVelocity();
    }
}
