using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float moveForce;
    public float maxHorizontalSpeed;
    public float maxVerticalSpeed;
    public float jumpForce;
    public Transform groundCheck;
    [HideInInspector] public bool isActivePlayer = false;
    [HideInInspector] public bool isFollowing = true;
    private Vector2 jumpDirection;
    private bool isJumpQueued;
    public bool IsWallClimbing { get; private set; }
    public bool IsFlying { get; private set; }
    private Vector3 _horizontalClamp;

    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {
        if (isActivePlayer && !IsFlying && Input.GetKey(KeyCode.W))
        {
            isJumpQueued = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        var newJumpDirection = new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.y);
        var upVector = Vector2.up;
        var angle = Vector2.Angle(upVector, newJumpDirection);


        if ((angle <= 45 || !collision.gameObject.tag.Equals("Ground")))
        {
            jumpDirection = newJumpDirection;
            IsWallClimbing = false;
            IsFlying = false;

        }
        else
        {
            jumpDirection = newJumpDirection * -1;
            IsWallClimbing = true;
            _horizontalClamp = jumpDirection;
            IsFlying = false;

        }


    }

    void OnCollisionStay(Collision collision)
    {
        var newJumpDirection = new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.y);
        var upVector = Vector2.up;

        if (newJumpDirection == Vector2.up)
        {
            jumpDirection = Vector2.up;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        IsFlying = true;

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        var rigidBody = GetComponent<Rigidbody>();

        bool isUnderHorizontalSpeedLimit = h * rigidBody.velocity.x < maxHorizontalSpeed;
        bool isAboveHorizontalSpeedLimit = Mathf.Abs(rigidBody.velocity.x) > maxHorizontalSpeed;
        bool isAboveVerticalSpeedLimit = rigidBody.velocity.y > maxVerticalSpeed;

        float clampAngle = Vector3.Angle(_horizontalClamp, h * Vector2.right);
        bool walkingIntoWall = (clampAngle < 90);

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