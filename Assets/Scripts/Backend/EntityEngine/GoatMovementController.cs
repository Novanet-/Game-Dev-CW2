using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using ProBuilder2.Common;
using UnityEngine;

namespace Backend.EntityEngine
{
    public class GoatMovementController : EntityMovementControllerBase
    {
        #region Public Fields

        [HideInInspector] public bool IsFollowing = true;

        #endregion Public Fields

        #region Private Fields

        private const float FollowDistance = 10;
        [SerializeField] private float _fadeTime = 10f;
        [SerializeField] private float _followForceMult = 1.0f;

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

        public void KillGoat()
        {
            Debug.Log("Kill Goat");
            StartCoroutine(DeathAnimation(_fadeTime));
        }

        #endregion Public Methods

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

        #endregion Protected Methods

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

        private IEnumerator DeathAnimation(float fadeTime)
        {
            Debug.Log("Death Animation");
            Vector3 goatScale = gameObject.transform.localScale;
            while (goatScale.x > 0.1 && goatScale.y > 0.1)
            {
                gameObject.transform.localScale = new Vector3(Mathf.Max(goatScale.x -= Time.deltaTime / fadeTime, 0), Mathf.Max(goatScale.y -= Time.deltaTime / fadeTime, 0),
                    goatScale.z);
                goatScale = gameObject.transform.localScale;
                yield return null;
            }

            gameObject.transform.position = new Vector3(9999f, 9999f, 9999f);
            GameController.Instance.GoatControllerArray = GameController.Instance.GoatControllerArray.Where(goat => goat != this).ToArray();
            GameController.Instance.ChangeCurrentTarget(true);
            Destroy(this);
        }

        private IEnumerator QueueJump()
        {
            IsJumpQueued = true;
            yield return new WaitForSeconds(0.2f);
            IsJumpQueued = false;
        }

        #endregion Private Methods
    }
}