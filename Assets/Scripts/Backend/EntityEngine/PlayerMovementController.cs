using UnityEngine;

namespace Backend
{
    public class PlayerMovementController : EntityMovementControllerBase
    {
        #region Public Fields

        [HideInInspector] public bool IsFollowing = true;
        [SerializeField] private float _followForceMult = 1.0f;

        #endregion Public Fields

        #region Protected Methods

        protected override void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            var rigidBody = GetComponent<Rigidbody>();

            bool isUnderHorizontalSpeedLimit = h * rigidBody.velocity.x < MaxHorizontalSpeed;
            bool isAboveHorizontalSpeedLimit = Mathf.Abs(rigidBody.velocity.x) > MaxHorizontalSpeed;
            bool isAboveVerticalSpeedLimit = rigidBody.velocity.y > MaxVerticalSpeed;

            float clampAngle = Vector3.Angle(HorizontalClamp, h * Vector2.right);
            bool walkingIntoWall = clampAngle < 90;

            //        Debug.Log("IsWallClimbing = " + IsWallClimbing + ", ClampAngle = " + clampAngle);

            CheckPlayerSpecificMovement(walkingIntoWall, isUnderHorizontalSpeedLimit, rigidBody, h);
            CheckSpeeds(isAboveHorizontalSpeedLimit, rigidBody, isAboveVerticalSpeedLimit);
            ApplyJumpForce(rigidBody);
        }

        #endregion Protected Methods

        #region Private Fields

        private const float FollowDistance = 10;

        private PlayerMovementController _followTarget;

        private bool _isFollowing;

        #endregion Private Fields

        #region Public Methods

        public void DisableFollowing()
        {
            _isFollowing = false;
            _followTarget = null;
        }

        public void EnableFollowing(PlayerMovementController goatToFollow)
        {
            _isFollowing = true;
            _followTarget = goatToFollow;
        }

        #endregion Public Methods

        #region Private Methods

        private void ApplyActivePlayerMovementInput(bool walkingIntoWall, bool isUnderHorizontalSpeedLimit, Rigidbody rigidBody, float h)
        {
            if (!IsActivePlayer) return;

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

        private void ApplyFollowForce(bool walkingIntoWall, bool isUnderHorizontalSpeedLimit, Rigidbody rigidBody)
        {
            if (!_isFollowing || IsActivePlayer) return;

            float distance = Vector3.Distance(transform.position, _followTarget.transform.position);
            if (Mathf.Abs(distance) <= FollowDistance) return;

            if (IsWallClimbing && walkingIntoWall)
            {
                IsFlying = false;
            }
            else
            {
                if (!isUnderHorizontalSpeedLimit) return;

                if (transform.position.x < _followTarget.transform.position.x)
                {
                    rigidBody.AddForce(Vector2.right * MoveForce * _followForceMult);
                }
                else
                {
                    rigidBody.AddForce(Vector2.left * MoveForce * _followForceMult);
                }
            }
        }

        private void CheckPlayerSpecificMovement(bool walkingIntoWall, bool isUnderHorizontalSpeedLimit, Rigidbody rigidBody, float h)
        {
            ApplyActivePlayerMovementInput(walkingIntoWall, isUnderHorizontalSpeedLimit, rigidBody, h);
            ApplyFollowForce(walkingIntoWall, isUnderHorizontalSpeedLimit, rigidBody);
        }

        protected override void Start()
        {
            float mass = GetComponent<Rigidbody>().mass;
            MoveForce = mass * 400;
            JumpForce = mass * 1000;
            MaxHorizontalSpeed = 15 / mass;
            MaxVerticalSpeed = 50 / mass;
        }

        protected override void Update()
        {
            if (IsActivePlayer && !IsFlying && Input.GetKeyDown(KeyCode.W))
            {
                IsJumpQueued = true;
            }
        }

        #endregion Private Methods
    }
}