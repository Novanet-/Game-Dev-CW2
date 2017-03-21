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

    private bool _grounded;
    private bool _jump;

    #endregion Private Fields

    #region Private Methods

    private static void AddGravity([NotNull] Rigidbody rigidBody)
    {
        rigidBody.AddForce(new Vector2(0f, -9.81f * 2));
    }

    private void AddVerticalJumpMovement([NotNull] Rigidbody rigidBody)
    {
        rigidBody.AddForce(new Vector2(0f, JumpForce));
        _jump = false;
        Debug.Log("Jump is false");
    }

    private void CheckForAndAddHorizontalMovement(float horizontalInput, [NotNull] Rigidbody rigidBody)
    {
        if (IsActivePlayer && horizontalInput * rigidBody.velocity.x < MaxHorizontalSpeed)
        {
            rigidBody.AddForce(Vector2.right * horizontalInput * MoveForce);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        var rigidBody = GetComponent<Rigidbody>();

        CheckForAndAddHorizontalMovement(horizontalInput, rigidBody);
        LimitHorizontalSpeed(rigidBody);
        LimitVerticalSpeed(rigidBody);

        if (IsActivePlayer && _jump)
        {
            AddVerticalJumpMovement(rigidBody);
        }

        if (!_grounded)
        {
            AddGravity(rigidBody);
        }
    }

    private void LimitHorizontalSpeed([NotNull] Rigidbody rigidBody)
    {
        if (Mathf.Abs(rigidBody.velocity.x) > MaxHorizontalSpeed)
        {
            rigidBody.velocity = new Vector2(Mathf.Sign(rigidBody.velocity.x) * MaxHorizontalSpeed, rigidBody.velocity.y);
        }
    }

    private void LimitVerticalSpeed([NotNull] Rigidbody rigidBody)
    {
        if (rigidBody.velocity.y > MaxVerticalSpeed)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Sign(rigidBody.velocity.y) * MaxVerticalSpeed);
        }
    }

    private void OnCollisionEnter([NotNull] Collision collision)
    {
        if (collision.transform.position.y < transform.position.y)
            _grounded = true;
    }

    private void OnCollisionExit([NotNull] Collision collision)
    {
        _grounded = false;
    }

    // Use this for initialization
    private void Start()
    {
    }

    private void Update()
    {
        if (!IsActivePlayer) return;
        if (!Input.GetKey(KeyCode.W)) return;
        if (!_grounded) return;

        _jump = true;
        Debug.Log("Jump is true");
    }

    #endregion Private Methods
}