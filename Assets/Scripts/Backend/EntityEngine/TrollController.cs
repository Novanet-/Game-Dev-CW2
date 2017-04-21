using UnityEngine;

namespace Backend.EntityEngine
{
    public class TrollMovementController : EntityMovementControllerBase {


        protected override void FixedUpdate()
        {
                    float h = Input.GetAxis("Horizontal");
//            var rigidBody = GetComponent<Rigidbody>();
//
//            bool isUnderHorizontalSpeedLimit = h * rigidBody.velocity.x < MaxHorizontalSpeed;
//            bool isAboveHorizontalSpeedLimit = Mathf.Abs(rigidBody.velocity.x) > MaxHorizontalSpeed;
//            bool isAboveVerticalSpeedLimit = rigidBody.velocity.y > MaxVerticalSpeed;
//
//            float clampAngle = Vector3.Angle(HorizontalClamp, h * Vector2.right);
//            bool walkingIntoWall = clampAngle < 90;
//
//
//            CheckSpeeds(isAboveHorizontalSpeedLimit, rigidBody, isAboveVerticalSpeedLimit);
//            ApplyJumpForce(rigidBody);
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
//            if (IsActivePlayer && !IsFlying && Input.GetKeyDown(KeyCode.W))
//            {
//                IsJumpQueued = true;
//            }
        }
    }
}
