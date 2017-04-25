using JetBrains.Annotations;
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
        protected float MaxHorizontalSpeed;
		protected float JumpForce;
        protected float MaxVerticalSpeed;
		protected float MoveForce;
		protected bool IsJumpQueued;

        #endregion Protected Fields

        #region Public Properties

        #endregion Public Properties

        #region Protected Methods

        protected void ClampSpeeds(bool isAboveHorizontalSpeedLimit, [NotNull] Rigidbody rigidBody,
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

        private void OnCollisionEnter([NotNull] Collision collision)
        {
			
        }

        private void OnCollisionExit()
        {

        }

        private void OnCollisionStay([NotNull] Collision collision)
        {
			if (IsJumpQueued)
			{
				IsJumpQueued = false;
				var rigidBody = GetComponent<Rigidbody>();
				_jumpDirection = new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.y);
				rigidBody.AddForce(_jumpDirection * JumpForce);
			}
        }

        #endregion Private Methods
    }
}