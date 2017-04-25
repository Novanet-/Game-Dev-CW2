using JetBrains.Annotations;
using UnityEngine;
using System.Collections;

namespace Backend.EntityEngine
{
    public class GoatMovementController : EntityMovementControllerBase
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
			ApplyActivePlayerMovementInput(isUnderHorizontalSpeedLimit, rigidBody, h);
			ApplyFollowForce(isUnderHorizontalSpeedLimit, rigidBody);
            ClampSpeeds(isAboveHorizontalSpeedLimit, rigidBody, isAboveVerticalSpeedLimit);
        }

		private IEnumerator QueueJump()
		{
			IsJumpQueued = true;
			yield return new WaitForSeconds(0.2f);
			IsJumpQueued = false;
		}

        #endregion Protected Methods

        #region Private Fields

        private const float FollowDistance = 10;

        private GoatMovementController _followTarget;

        private bool _isFollowing;

        #endregion Private Fields

        #region Public Methods

        public void DisableFollowing()
        {
            _isFollowing = false;
            _followTarget = null;
        }

        public void EnableFollowing([NotNull] GoatMovementController goatToFollow)
        {
            _isFollowing = true;
            _followTarget = goatToFollow;
        }

        #endregion Public Methods

        #region Private Methods

        private void ApplyActivePlayerMovementInput(bool isUnderHorizontalSpeedLimit, [NotNull] Rigidbody rigidBody, float h)
        {
            if (!IsActivePlayer) return;
            if (isUnderHorizontalSpeedLimit)
            {
                rigidBody.AddForce(Vector2.right * h * MoveForce);
            }
			if (Input.GetKeyDown(KeyCode.W) && !IsJumpQueued)
			{
				StartCoroutine(QueueJump());
			}
        }

        private void ApplyFollowForce(bool isUnderHorizontalSpeedLimit, [NotNull] Rigidbody rigidBody)
        {
            if (!_isFollowing || IsActivePlayer) return;

            float distance = Vector3.Distance(transform.position, _followTarget.transform.position);
            if (Mathf.Abs(distance) <= FollowDistance) return;
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

        }

        #endregion Private Methods
    }
}