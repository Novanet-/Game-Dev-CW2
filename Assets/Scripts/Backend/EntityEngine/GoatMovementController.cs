using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Backend.StoryEngine;

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
		private bool _isDying;

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
//            StartCoroutine(DeathAnimation(_fadeTime));
			StartCoroutine(DeathAnimation(_fadeTime));
		}

		#endregion Public Methods

		#region Protected Methods

		protected override void FixedUpdate()
		{
			float h = GameController.Instance.GameEnded ? 0 : Input.GetAxis("Horizontal");
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
			if (!_isFollowing || IsActivePlayer || _followTarget == null) return;

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
			float originalMass = gameObject.GetComponent<Rigidbody>().mass;
			float goatMass = gameObject.GetComponent<Rigidbody>().mass;
			while (goatScale.x > 0.1f && goatScale.y > 0.1f)
			{
				if (_isDying)
				{
					float inc = Time.deltaTime / fadeTime;
					gameObject.transform.localScale = new Vector3(Mathf.Max(goatScale.x -= inc, 0f), Mathf.Max(goatScale.y -= inc, 0f),
						goatScale.z);
					gameObject.GetComponent<Rigidbody>().mass = Mathf.Max(goatMass -= inc, 0f);
					goatScale = gameObject.transform.localScale;
					goatMass = gameObject.GetComponent<Rigidbody>().mass;

					float mass = GetComponent<Rigidbody>().mass;
//					float massDiff = originalMass - mass;
//					MoveForce -= massDiff * 400;
//					JumpForce -= massDiff * 1000;
				}
				yield return null;
			}

			var deadMass = gameObject.GetComponent<Rigidbody>().mass;
			StoryController.Instance.Events.Story.GoatDied(deadMass);
			gameObject.transform.position = new Vector3(9999f, 9999f, 9999f);
			GameController.Instance.GoatControllerArray = GameController.Instance.GoatControllerArray.Where(goat => goat != this).ToArray();
			GameController.Instance.ChangeCurrentTarget(true);
			GameController.Instance.ChangeCurrentTarget(true);
			Destroy(this);
			if (GameController.Instance.GoatControllerArray.Length <= 0)
			{
				GameController.Instance.EndGame();
			}
		}

		private IEnumerator QueueJump()
		{
			IsJumpQueued = true;
			yield return new WaitForSeconds(0.2f);

			IsJumpQueued = false;
		}

		protected override void OnCollisionEnter([NotNull] Collision collision) {
			if (collision.gameObject.tag.Equals("Troll") && gameObject.GetComponent<Rigidbody>().mass < 3f)
				_isDying = true;
		}

		protected override void OnCollisionStay([NotNull] Collision collision)
		{
			base.OnCollisionStay(collision);
			if (collision.gameObject.tag.Equals("Troll") && gameObject.GetComponent<Rigidbody>().mass < 3f)
			{
				gameObject.GetComponent<GoatMovementController>().KillGoat();
			}
		}

		protected override void OnCollisionExit([NotNull] Collision collision)
		{
			if (collision.gameObject.tag.Equals("Troll") && gameObject.GetComponent<Rigidbody>().mass < 3f)
				_isDying = false;
		}
		#endregion Private Methods
	}
}