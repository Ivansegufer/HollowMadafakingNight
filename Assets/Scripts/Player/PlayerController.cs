using System.Collections;
using Cinemachine;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [Header("Parameters")] public float movementSpeed;
    public float dashNoMovementSpeed;
    public float dashSpeed;
    public float jumpForce;

    [Header("Colliders")] public LayerMask ground;
    public Vector2 down;
    public float colliderRadius;

    [Header("Flags")] public bool canDash = true;
    public bool canMove = true;
    public bool inGround = true;
    public bool isJumping = false;
    public bool isFalling = false;
    public bool isAttacking = false;
    public bool isWalkPressed = false;
    public bool isJumpPressed = false;
    public bool isCameraShaking = false;

    private Rigidbody2D _rb;
    private Animator _animator;
    private Transform _transform;
    private Vector2 _movementDirection;
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        _virtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        Move();
        Grip();

        if (Input.GetKeyDown(KeyCode.Escape) && !isCameraShaking)
        {
            StartCoroutine(ShakeCamera());
        }
    }

    private void Move()
    {
        Walk();
        ImproveJump();
        Jump();
        Fall();

        if (!inGround)
        {
            SetVerticalVelocity();
        }
    }

    private void Walk()
    {
        if (!canMove) return;

        RotatePlayer();
        _rb.velocity = new Vector2(_movementDirection.x * movementSpeed, _rb.velocity.y);
    }

    private void Dash()
    {
        if (!canDash) return;

        canDash = false;
        canMove = false;

        ResetMovement();

        var isMoving = CheckMovement();

        var speed = isMoving ? dashSpeed : dashNoMovementSpeed;

        if (transform.localScale.x > 0)
        {
            _rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        }
        else
        {
            _rb.AddForce(-Vector2.right * speed, ForceMode2D.Impulse);
        }
    }

    private IEnumerator ShakeCamera()
    {
        var cinemachineBasicMultiChannelPerlin =
            _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        isCameraShaking = true;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 3;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 3;
        yield return new WaitForSeconds(1f);
        isCameraShaking = false;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
    }

    private void EndDash()
    {
        canDash = true;
        canMove = true;
    }

    private void EndAttack()
    {
        isAttacking = false;
        canMove = true;
        canDash = true;
        StopAttackAnimation();

        if (isWalkPressed)
        {
            ActivateWalkAnimation();
        }
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
    }

    private void Fall()
    {
        if (inGround || isJumping || isFalling) return;

        StartFall();
        ActivateFallAnimation();
    }

    private void ResetMovement()
    {
        _rb.velocity = Vector2.zero;
    }

    private void ResetHorizontalMovement()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }
}
