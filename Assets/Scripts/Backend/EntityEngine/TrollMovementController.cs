using JetBrains.Annotations;
using UnityEngine;
using System.Collections;
using Backend.StoryEngine;

namespace Backend.EntityEngine
{
	public class TrollMovementController : EntityMovementControllerBase
	{
		[SerializeField] private float _distanceThreshold;

		#region Public Properties

		public TrollAIController TrollAI { get; set; }

		private bool _isDying;
		[SerializeField] private float _fadeTime = 10f;

		#endregion Public Properties

		#region Protected Methods

		protected override void FixedUpdate()
		{
			if (TrollAI == null) TrollAI = new TrollAIController(this, GameController.Instance.GoatControllerArray, _distanceThreshold);

			float h = _isDying ? 0 :TrollAI.NextMove();
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

		void KillTroll() {
			Debug.Log("Kill Goat");
			//            StartCoroutine(DeathAnimation(_fadeTime));
			StartCoroutine(DeathAnimation(_fadeTime));
		}

		private IEnumerator DeathAnimation(float fadeTime)
		{
			Debug.Log("Death Animation");
			Vector3 trollScale = gameObject.transform.localScale;
			float originalMass = gameObject.GetComponent<Rigidbody>().mass;
			float trollMass = gameObject.GetComponent<Rigidbody>().mass;
			while (trollScale.x > 0.1f && trollScale.y > 0.1f)
			{
				if (_isDying)
				{
					float inc = Time.deltaTime / fadeTime;
					gameObject.transform.localScale = new Vector3(Mathf.Max(trollScale.x -= inc, 0f), Mathf.Max(trollScale.y -= inc, 0f),
						trollScale.z);
					gameObject.GetComponent<Rigidbody>().mass = Mathf.Max(trollMass -= inc, 0f);
					trollScale = gameObject.transform.localScale;
					trollMass = gameObject.GetComponent<Rigidbody>().mass;

					float mass = GetComponent<Rigidbody>().mass;
				}
				yield return null;
			}
				
			StoryController.Instance.Events.Story.TrollDied();
			gameObject.transform.position = new Vector3(9999f, 9999f, 9999f);
			Destroy(this);
		}

		protected override void Start()
		{
			float mass = GetComponent<Rigidbody>().mass;
			MoveForce = mass * 800;
			JumpForce = mass * 1000;
			MaxHorizontalSpeed = 15 / mass;
			MaxVerticalSpeed = 50 / mass;
		}

		protected override void Update()
		{
//			if (TrollAI == null) TrollAI = new TrollAIController(this, GameController.Instance.GoatControllerArray, _distanceThreshold);
		}

		protected override void OnCollisionEnter([NotNull] Collision collision) {
			if (collision.gameObject.tag.Equals("Player") && collision.gameObject.GetComponent<Rigidbody>().mass >= 2.5f)
				_isDying = true;
		}

		protected override void OnCollisionStay([NotNull] Collision collision)
		{
			base.OnCollisionStay(collision);
			if (collision.gameObject.tag.Equals("Player") && collision.gameObject.GetComponent<Rigidbody>().mass >= 2.5f)
			{
				gameObject.GetComponent<TrollMovementController>().KillTroll();
			}
		}

		protected override void OnCollisionExit([NotNull] Collision collision)
		{
			if (collision.gameObject.tag.Equals("Player") && collision.gameObject.GetComponent<Rigidbody>().mass >= 2.5f)
				_isDying = false;
		}

		#endregion Protected Methods
	}
}