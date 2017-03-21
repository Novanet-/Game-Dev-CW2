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
	private bool isFlying = false;

    // Use this for initialization
    void Start()
    {
		
    }

    void Update()
    {
		if (isActivePlayer && !isFlying && Input.GetKey(KeyCode.W))
        {
            isJumpQueued = true;
        }
    }

	void OnCollisionEnter(Collision collision) {
		jumpDirection = new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.y);
		isFlying = false;
	}

	void OnCollisionExit(Collision collision) {
		isFlying = true;
	}
		
    void FixedUpdate()
    {
		float h = Input.GetAxis("Horizontal");
		var rigidBody = GetComponent<Rigidbody>();

		if (isActivePlayer && h * rigidBody.velocity.x < maxHorizontalSpeed)
			rigidBody.AddForce(Vector2.right * h * moveForce);

		if (Mathf.Abs(rigidBody.velocity.x) > maxHorizontalSpeed)
			rigidBody.velocity = new Vector2(Mathf.Sign(rigidBody.velocity.x) * maxHorizontalSpeed, rigidBody.velocity.y);

		if (rigidBody.velocity.y > maxVerticalSpeed)
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Sign(rigidBody.velocity.y) * maxVerticalSpeed);
		
		if (isActivePlayer && isJumpQueued)
		{
			rigidBody.AddForce(jumpDirection * jumpForce);
			isJumpQueued = false;
		}

    }
}