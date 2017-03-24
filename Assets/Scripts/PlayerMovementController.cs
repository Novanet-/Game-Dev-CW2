using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Public Fields

    public Transform GroundCheck;
    [HideInInspector] public bool IsActivePlayer = false;
    [HideInInspector] public bool IsFollowing = true;

    #endregion Public Fields

    #region Private Fields

    private Vector3 _horizontalClamp;
    private bool _isJumpQueued;
    private Vector2 _jumpDirection;
	private bool _isFollowing;
	private PlayerMovementController _followTarget;
	private float _jumpForce;
	private float _maxHorizontalSpeed;
	private float _maxVerticalSpeed;
	private float _moveForce;
	private float _followDistance = 10;

    #endregion Private Fields

    #region Public Properties

    public bool IsFlying { get; private set; }
    public bool IsWallClimbing { get; private set; }

    #endregion Public Properties

    #region Private Methods

	private void Start()
	{
		float mass = GetComponent<Rigidbody> ().mass;
		_moveForce = mass * 400;
		_jumpForce = mass * 1000;
		_maxHorizontalSpeed = 15 / mass;
		_maxVerticalSpeed = 50 / mass;
	}

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

        bool isUnderHorizontalSpeedLimit = h * rigidBody.velocity.x < _maxHorizontalSpeed;
        bool isAboveHorizontalSpeedLimit = Mathf.Abs(rigidBody.velocity.x) > _maxHorizontalSpeed;
        bool isAboveVerticalSpeedLimit = rigidBody.velocity.y > _maxVerticalSpeed;

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
                    rigidBody.AddForce(Vector2.right * h * _moveForce);
                }
            }
		}
		else if (_isFollowing)
		{
			float distance = Vector3.Distance(transform.position, _followTarget.transform.position);
			if (Mathf.Abs(distance) > _followDistance) {
				if (IsWallClimbing && walkingIntoWall) {
					IsFlying = false;
				} else {
					if (isUnderHorizontalSpeedLimit) {
						if (transform.position.x < _followTarget.transform.position.x) {
							rigidBody.AddForce(Vector2.right * _moveForce);
						} else {
							rigidBody.AddForce(Vector2.left * _moveForce);
						}
					}
				}
			}
		}

        if (isAboveHorizontalSpeedLimit)
        {
            rigidBody.velocity = new Vector2(Mathf.Sign(rigidBody.velocity.x) * _maxHorizontalSpeed, rigidBody.velocity.y);
        }

        if (isAboveVerticalSpeedLimit)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Sign(rigidBody.velocity.y) * _maxVerticalSpeed);
        }

        if (IsActivePlayer && _isJumpQueued)
        {
            rigidBody.AddForce(_jumpDirection * _jumpForce);
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

        }
        else
        {
            IsFlying = false;
        }
        CheckMovementState(collision, angle, newJumpDirection);
    }

    private void Update()
    {
        if (IsActivePlayer && !IsFlying && Input.GetKeyDown(KeyCode.W))
        {
            _isJumpQueued = true;
        }
    }

    #endregion Private Methods

	#region Public Methods

	public void EnableFollowing(PlayerMovementController goatToFollow) {
		_isFollowing = true;
		_followTarget = goatToFollow;
	}

	public void DisableFollowing() {
		_isFollowing = false;
		_followTarget = null;
	}

	#endregion
}