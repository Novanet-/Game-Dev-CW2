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
    private bool grounded = true;
    private float distToGround;

    // Use this for initialization
    void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        //        Debug.Log(grounded);

        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            jump = true;
            Debug.Log("Jump is true");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        var rb2d = GetComponent<Rigidbody>();


        if (h * rb2d.velocity.x < maxHorizontalSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxHorizontalSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxHorizontalSpeed, rb2d.velocity.y);

        if (rb2d.velocity.y > maxVerticalSpeed)
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Sign(rb2d.velocity.y) * maxVerticalSpeed);



        if (jump)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
            Debug.Log("Jump is false");

        }

        if (!grounded)
        {
            rb2d.AddForce(new Vector2(0f, -9.81f * 2));
        }
    }
}