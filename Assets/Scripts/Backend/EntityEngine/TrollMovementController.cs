using JetBrains.Annotations;
using UnityEngine;

namespace Backend.EntityEngine
{
    public class TrollMovementController : EntityMovementControllerBase
    {
        [SerializeField] private float _distanceThreshold;

        #region Public Properties

        private TrollAIController TrollAI { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override void FixedUpdate()
        {
            if (TrollAI == null) TrollAI = new TrollAIController(this, GameController.Instance.GoatControllerArray, _distanceThreshold);

            float h = TrollAI.NextMove();
            var rigidBody = GetComponent<Rigidbody>();

            bool isUnderHorizontalSpeedLimit = h * rigidBody.velocity.x < MaxHorizontalSpeed;
            bool isAboveHorizontalSpeedLimit = Mathf.Abs(rigidBody.velocity.x) > MaxHorizontalSpeed;
            bool isAboveVerticalSpeedLimit = rigidBody.velocity.y > MaxVerticalSpeed;

            float clampAngle = Vector3.Angle(HorizontalClamp, h * Vector2.right);
            bool walkingIntoWall = clampAngle < 90;

            ApplyMovement(walkingIntoWall, isUnderHorizontalSpeedLimit, rigidBody, h);
            ClampSpeeds(isAboveHorizontalSpeedLimit, rigidBody, isAboveVerticalSpeedLimit);
        }

        private void ApplyMovement(bool walkingIntoWall, bool isUnderHorizontalSpeedLimit, [NotNull] Rigidbody rigidBody, float h)
        {
            if (isUnderHorizontalSpeedLimit)
            {
                rigidBody.AddForce(Vector2.right * h * MoveForce);
            }
        }

        protected override void Start()
        {
            float mass = GetComponent<Rigidbody>().mass;
            MoveForce = mass * 400;
            JumpForce = mass * 1000;
            MaxHorizontalSpeed = 15 / mass;
            MaxVerticalSpeed = 50 / mass;
//            TrollAI = new TrollAIController(this, GameController.Instance.GoatControllerArray, _distanceThreshold);
        }

        protected override void Update()
        {
        }

        #endregion Protected Methods
    }
}