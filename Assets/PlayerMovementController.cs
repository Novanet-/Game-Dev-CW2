using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float HorizontalForceMagnitude;
    public float VerticalForceMagnitude;
    public float maxSpeed;
    private bool isGrounded;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Input.GetAxis("Horizontal") * transform.right * HorizontalForceMagnitude, ForceMode.VelocityChange);
            GetComponent<Rigidbody>().AddForce(Input.GetAxis("Vertical") * transform.up * VerticalForceMagnitude, ForceMode.Impulse);
        }

        if (GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
        }
    }

    void OnCollisionEnter([NotNull] Collision collision)
    {
        if (collision.gameObject.tag.Contains("Ground"))
        {
            isGrounded = true;
        }
    }

    //consider when character is jumping .. it will exit collision.
    void OnCollisionExit([NotNull] Collision collision)
    {
        if (collision.gameObject.tag.Contains("Ground"))
        {
            isGrounded = false;
        }
    }
}