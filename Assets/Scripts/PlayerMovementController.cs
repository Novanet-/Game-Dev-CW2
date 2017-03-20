using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    //    public float HorizontalForceMagnitude;
    //    public float VerticalForceMagnitude;
    //    public float maxHorizontalSpeed;
    //    public float maxVerticalSpeed;
    //    private bool isGrounded;

    private bool jump;
    public float moveForce;
    public float maxHorizontalSpeed;
    public float maxVerticalSpeed;
    public float jumpForce;
    public Transform groundCheck;
	[HideInInspector] public bool isActivePlayer = false;
	[HideInInspector] public bool isFollowing = true;
    private bool grounded = false;

    // Use this for initialization
    void Start()
    {
		
    }

    void Update()
    {
        if (isActivePlayer && Input.GetKey(KeyCode.W) && grounded)
        {
            jump = true;
            Debug.Log("Jump is true");
        }
    }

	void OnCollisionEnter(Collision collision) {
		if (collision.transform.position.y < transform.position.y) {
			grounded = true;
		}
	}

	void OnCollisionExit(Collision collision) {
		grounded = false;
	}

    // Update is called once per frame
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
		if (isActivePlayer && jump)
		{
			rigidBody.AddForce(new Vector2(0f, jumpForce));
			jump = false;
			Debug.Log("Jump is false");

		}
		if (!grounded)
		{
			rigidBody.AddForce(new Vector2(0f, -9.81f * 2));
		}
    }
}