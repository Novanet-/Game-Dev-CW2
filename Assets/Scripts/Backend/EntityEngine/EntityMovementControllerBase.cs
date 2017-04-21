using UnityEngine;

namespace Backend.EntityEngine
{
    public abstract class EntityMovementControllerBase : MonoBehaviour
    {
        #region Private Fields

        private Vector2 _jumpDirection;

        #endregion Private Fields

        #region Public Fields

        [HideInInspector] public bool IsActivePlayer = false;

        #endregion Public Fields

        #region Protected Fields

        protected Vector3 HorizontalClamp;
        protected bool IsJumpQueued;
        protected float JumpForce;
        protected float MaxHorizontalSpeed;
        protected float MaxVerticalSpeed;
        protected float MoveForce;

        #endregion Protected Fields

        #region Public Properties

        protected bool IsFlying { get; set; }
        protected bool IsWallClimbing { get; private set; }

        #endregion Public Properties

        #region Protected Methods

        protected void ApplyJumpForce(Rigidbody rigidBody)
        {
            if (!IsActivePlayer || !IsJumpQueued) return;

            rigidBody.AddForce(_jumpDirection * JumpForce);
            IsFlying = true;
            IsJumpQueued = false;
        }

        protected void CheckSpeeds(bool isAboveHorizontalSpeedLimit, Rigidbody rigidBody,
            bool isAboveVerticalSpeedLimit)
        {
            if (isAboveHorizontalSpeedLimit)
            {
                rigidBody.velocity = new Vector2(Mathf.Sign(rigidBody.velocity.x) * MaxHorizontalSpeed, rigidBody.velocity.y);
            }

            if (isAboveVerticalSpeedLimit)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Sign(rigidBody.velocity.y) * MaxVerticalSpeed);
            }
        }

        protected abstract void FixedUpdate();

        protected abstract void Start();

        protected abstract void Update();

        #endregion Protected Methods

        #region Private Methods

        private void UpdateMovementConstraints(Collision collision, float normalAngleFromUpVector, Vector2 newJumpDirection)
        {
            IsWallClimbing = !(normalAngleFromUpVector <= 45)
                             && collision.gameObject.tag.Equals("Ground");

            HorizontalClamp = IsWallClimbing ? (Vector3) (_jumpDirection * -1) : HorizontalClamp;
            IsFlying = false;
            _jumpDirection = newJumpDirection;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var newJumpDirection = new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.y);
            var upVector = Vector2.up;
            float angle = Vector2.Angle(upVector, newJumpDirection);

            UpdateMovementConstraints(collision, angle, newJumpDirection);
        }

        private void OnCollisionExit()
        {
            IsFlying = true;
            IsWallClimbing = false;
        }

        private void OnCollisionStay(Collision collision)
        {
            var newJumpDirection = new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.y);
            var upVector = Vector2.up;
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
            UpdateMovementConstraints(collision, angle, newJumpDirection);
        }

        #endregion Private Methods
    }
}