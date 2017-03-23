using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Public Fields

    public Transform GroundCheck;
    [HideInInspector] public bool IsActivePlayer = false;
    [HideInInspector] public bool IsFollowing = true;
    public float JumpForce;
    public float MaxHorizontalSpeed;
    public float MaxVerticalSpeed;
    public float MoveForce;

    #endregion Public Fields

    #region Private Fields

    private Vector3 _horizontalClamp;
    private bool _isJumpQueued;
    private Vector2 _jumpDirection;

    #endregion Private Fields

    #region Public Properties

    public bool IsFlying { get; private set; }
    public bool IsWallClimbing { get; private set; }

    #endregion Public Properties

    #region Private Methods

    private void CheckMovementState([NotNull] Collision collision, float normalAngleFromUpVector, Vector2 newJumpDirection)
    {
        IsWallClimbing = !(normalAngleFromUpVector <= 45)
                         && collision.gameObject.tag.Equals("Ground");

        _horizontalClamp = IsWallClimbing ? (Vector3) (_jumpDirection * -1) : _horizontalClamp;
        IsFlying = false;
        _jumpDirection = newJumpDirection;
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        var rigidBody = GetComponent<Rigidbody>();

        bool isUnderHorizontalSpeedLimit = h * rigidBody.velocity.x < MaxHorizontalSpeed;
        bool isAboveHorizontalSpeedLimit = Mathf.Abs(rigidBody.velocity.x) > MaxHorizontalSpeed;
        bool isAboveVerticalSpeedLimit = rigidBody.velocity.y > MaxVerticalSpeed;

        float clampAngle = Vector3.Angle(_horizontalClamp, h * Vector2.right);
        bool walkingIntoWall = clampAngle < 90;

        //        Debug.Log("IsWallClimbing = " + IsWallClimbing + ", ClampAngle = " + clampAngle);

        if (IsActivePlayer)
        {
            if (IsWallClimbing && walkingIntoWall)
            {
                IsFlying = false;
            }
            else
            {
                if (isUnderHorizontalSpeedLimit)
                {
                    rigidBody.AddForce(Vector2.right * h * MoveForce);
                }
            }
        }

        if (isAboveHorizontalSpeedLimit)
        {
            rigidBody.velocity = new Vector2(Mathf.Sign(rigidBody.velocity.x) * MaxHorizontalSpeed, rigidBody.velocity.y);
        }

        if (isAboveVerticalSpeedLimit)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Sign(rigidBody.velocity.y) * MaxVerticalSpeed);
        }

        if (IsActivePlayer && _isJumpQueued)
        {
            rigidBody.AddForce(_jumpDirection * JumpForce);
            IsFlying = true;
            _isJumpQueued = false;
        }
    }

    private void OnCollisionEnter([NotNull] Collision collision)
    {
        var newJumpDirection = new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.y);
        Vector2 upVector = Vector2.up;
        float angle = Vector2.Angle(upVector, newJumpDirection);

        CheckMovementState(collision, angle, newJumpDirection);
    }

    private void OnCollisionExit([NotNull] Collision collision)
    {
        IsFlying = true;
        IsWallClimbing = false;
    }

    private void OnCollisionStay([NotNull] Collision collision)
    {
        var newJumpDirection = new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.y);
        Vector2 upVector = Vector2.up;
        float angle = Vector2.Angle(upVector, newJumpDirection);

        if (!IsWallClimbing)
        {
            if (newJumpDirection == Vector2.up)
            {
                _jumpDirection = Vector2.up;
            }

            CheckMovementState(collision, angle, newJumpDirection);
        }
        else
        {
            IsFlying = false;
        }
    }

    // Use this for initialization
    private void Start()
    {
    }

    private void Update()
    {
        if (IsActivePlayer && !IsFlying && Input.GetKey(KeyCode.W))
        {
            _isJumpQueued = true;
        }
    }

    #endregion Private Methods
}