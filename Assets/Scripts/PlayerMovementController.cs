using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovementController : MonoBehaviour
{
    [FormerlySerializedAs("moveForce")] public float moveForce;
    [FormerlySerializedAs("maxHorizontalSpeed")] public float maxHorizontalSpeed;
    [FormerlySerializedAs("maxVerticalSpeed")] public float maxVerticalSpeed;
    [FormerlySerializedAs("jumpForce")] public float jumpForce;
    [FormerlySerializedAs("groundCheck")] public Transform groundCheck;
    [HideInInspector] public bool isActivePlayer = false;
    [HideInInspector] public bool isFollowing = true;
    private Vector2 jumpDirection;
    private bool isJumpQueued;
    public bool IsWallClimbing { get; private set; }
    public bool IsFlying { get; private set; }
    private Vector3 _horizontalClamp;

    // Use this for initialization
    private void Start()
    {
    }

    private void Update()
    {
        if (isActivePlayer && !IsFlying && Input.GetKey(KeyCode.W))
        {
            isJumpQueued = true;
        }
    }

    private void OnCollisionEnter([NotNull] Collision collision)
    {
        var newJumpDirection = new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.y);
        Vector2 upVector = Vector2.up;
        float angle = Vector2.Angle(upVector, newJumpDirection);


        CheckMovementState(collision, angle, newJumpDirection);
    }

    private void CheckMovementState([NotNull] Collision collision, float normalAngleFromUpVector, Vector2 newJumpDirection)
    {
        IsWallClimbing = !(normalAngleFromUpVector <= 45)
                         && collision.gameObject.tag.Equals("Ground");

        _horizontalClamp = IsWallClimbing ? (Vector3) (jumpDirection * -1) : _horizontalClamp;
        IsFlying = false;
        jumpDirection = newJumpDirection;
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
                jumpDirection = Vector2.up;
            }

            CheckMovementState(collision, angle, newJumpDirection);
        }
        else
        {
            IsFlying = false;
        }
    }

    private void OnCollisionExit([NotNull] Collision collision)
    {
        IsFlying = true;
        IsWallClimbing = false;
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        var rigidBody = GetComponent<Rigidbody>();

        bool isUnderHorizontalSpeedLimit = h * rigidBody.velocity.x < maxHorizontalSpeed;
        bool isAboveHorizontalSpeedLimit = Mathf.Abs(rigidBody.velocity.x) > maxHorizontalSpeed;
        bool isAboveVerticalSpeedLimit = rigidBody.velocity.y > maxVerticalSpeed;

        float clampAngle = Vector3.Angle(_horizontalClamp, h * Vector2.right);
        bool walkingIntoWall = clampAngle < 90;

//        Debug.Log("IsWallClimbing = " + IsWallClimbing + ", ClampAngle = " + clampAngle);


        if (isActivePlayer)
        {
            if (IsWallClimbing && walkingIntoWall)
            {
                IsFlying = false;
            }
            else
            {
                if (isUnderHorizontalSpeedLimit)
                {
                    rigidBody.AddForce(Vector2.right * h * moveForce);
                }
            }
        }


        if (isAboveHorizontalSpeedLimit)
        {
            rigidBody.velocity = new Vector2(Mathf.Sign(rigidBody.velocity.x) * maxHorizontalSpeed, rigidBody.velocity.y);
        }

        if (isAboveVerticalSpeedLimit)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Sign(rigidBody.velocity.y) * maxVerticalSpeed);
        }

        if (isActivePlayer && isJumpQueued)
        {
            rigidBody.AddForce(jumpDirection * jumpForce);
            IsFlying = true;
            isJumpQueued = false;
        }
    }
}