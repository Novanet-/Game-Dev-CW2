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

    [HideInInspector] public bool jump;
    public float moveForce = 365f;
    public float maxHorizontalSpeed = 5f;
    public float maxVerticalSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
    private bool grounded = true;

    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Foreground");
//        layerMask = ~layerMask;

        RaycastHit hit;

        grounded = Physics.Linecast(transform.position, groundCheck.position, out hit, layerMask);
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
        //        var rigidbody = GetComponent<Rigidbody>();
        //
        ////        rigidbody.AddForce(Vector3.down * 10f * Time.fixedDeltaTime);
        //        float horizontalSpeed = Vector3.Magnitude(new Vector3(rigidbody.velocity.x, 0, 0));
        //        float verticalSpeed = Vector3.Magnitude(new Vector3(rigidbody.velocity.y, 0, 0));
        //
        //        isGrounded = !(Mathf.Abs(verticalSpeed) > 0);
        //
        //
        //        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        //        rigidbody.AddForce(horizontalMovement * transform.right * HorizontalForceMagnitude, ForceMode.Impulse);
        ////        if (horizontalMovement != 0)
        ////        {
        ////            rigidbody.AddForce(transform.up * -1f, ForceMode.Impulse);
        ////        }
        //
        ////        isGrounded = true;
        //        if (isGrounded)
        //        {
        //            rigidbody.AddForce(Input.GetAxisRaw("Vertical") * transform.up * VerticalForceMagnitude, ForceMode.Impulse);
        //        }
        //
        //        rigidbody.AddForce(transform.up * -1f, ForceMode.Impulse);
        //
        //        if (horizontalSpeed > maxHorizontalSpeed)
        //        {
        //            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxHorizontalSpeed);
        //
        //            float brakeSpeed = horizontalSpeed - maxHorizontalSpeed; // calculate the speed decrease
        //            Vector3 normalisedVelocity = rigidbody.velocity.normalized;
        //            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed; // make the brake Vector3 value
        //
        //            brakeVelocity = new Vector3(brakeVelocity.x, 0, 0);
        //            rigidbody.AddForce(-brakeVelocity); // apply opposing brake force
        //        }
        //        if (verticalSpeed > maxVerticalSpeed)
        //        {
        //            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVerticalSpeed);
        //
        //            float brakeSpeed = verticalSpeed - maxVerticalSpeed; // calculate the speed decrease
        //            Vector3 normalisedVelocity = rigidbody.velocity.normalized;
        //            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed; // make the brake Vector3 value
        //
        //            brakeVelocity = new Vector3(brakeVelocity.y, 0, 0);
        //            rigidbody.AddForce(-brakeVelocity); // apply opposing brake force
        //        }

        float h = Input.GetAxis("Horizontal");
        var rb2d = GetComponent<Rigidbody>();


        if (h * rb2d.velocity.x < maxHorizontalSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxHorizontalSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxHorizontalSpeed, rb2d.velocity.y);

        if ((rb2d.velocity.y) > maxHorizontalSpeed)
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Sign(rb2d.velocity.y) * maxHorizontalSpeed);



        if (jump)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
            Debug.Log("Jump is false");

        }
    }


//    void OnCollisionEnter([NotNull] Collision collision)
//    {
//        if (collision.gameObject.tag.Contains("Ground"))
//        {
//            isGrounded = true;
//        }
//    }
//
//    //consider when character is jumping .. it will exit collision.
//    void OnCollisionExit([NotNull] Collision collision)
//    {
//        if (collision.gameObject.tag.Contains("Ground"))
//        {
//            isGrounded = false;
//        }
//    }
}