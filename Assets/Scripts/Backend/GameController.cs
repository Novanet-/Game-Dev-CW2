using System;
using Backend.EntityEngine;
using Backend.StoryEngine;
using com.kleberswf.lib.core;
using Frontend.SoundEngine;
using Frontend;
using UnityEngine;

namespace Backend
{
	public class GameController : Singleton<GameController>
	{
		#region Public Fields

		public float GravityStrength;
		public Hooks Hooks = new Hooks();

		#endregion Public Fields

		#region Private Fields

		[SerializeField] private GameObject _storyControllerObject;
		[SerializeField] private float _timeLeft;

		#endregion Private Fields

		#region Public Properties

		private bool TimerStarted { get; set; }

		#endregion Public Properties

		#region Private Properties

		private GoatMovementController CurrentGoat
		{
			get { return GoatControllerArray[CurrentGoatIndex]; }
		}

		private int CurrentGoatIndex { get; set; }
		private bool GlobalFollowingEnabled { get; set; }
		internal GoatMovementController[] GoatControllerArray { get; set; }
		private StoryController StoryController { get; set; }

		#endregion Private Properties

		#region Public Methods

		public static void EndGame()
		{
			Application.Quit();
		}

		public void StartGameTimer()
		{
			TimerStarted = true;
		}

		#endregion Public Methods

		#region Private Methods

		public void ChangeCurrentTarget(bool isRight)
		{
		    int newIndexUnbound = isRight ? CurrentGoatIndex + 1 : CurrentGoatIndex - 1;
		    int newIndex = Utils.HelperFunctions.Mod(newIndexUnbound, GoatControllerArray.Length);
			SetCurrentTarget(newIndex);
		}

		private void DisableFollowing()
		{
			foreach (var goatController in GoatControllerArray)
			{
				if (goatController != CurrentGoat) goatController.DisableFollowing();
			}
		}

		private void EnableFollowing()
		{
			foreach (var goatController in GoatControllerArray)
			{
				if (goatController != CurrentGoat) goatController.EnableFollowing(CurrentGoat);
			}
		}

		private void SetCurrentGoatAsActivePlayer()
		{
			foreach (var goatController in GoatControllerArray)
			{
				goatController.IsActivePlayer = goatController == CurrentGoat;
			}
		}

		private void SetCurrentTarget(int newIndex)
		{
			DisableFollowing();

			CurrentGoatIndex = newIndex;
			SetCurrentGoatAsActivePlayer();

			Hooks.Camera.GetComponent<CameraController>().CurrentTarget = CurrentGoat.transform;
			SoundController.Instance.PlaySingle(Sounds.Instance.GoatSwitchSwooshClip, 0.2f);


			if (GlobalFollowingEnabled) EnableFollowing();
		}

		private void Start()
		{
			Physics.gravity = new Vector3(0f, -GravityStrength, 0f);
			GoatControllerArray = new[]
			{
				Hooks.GoatSmall.GetComponent<GoatMovementController>(),
				Hooks.GoatMed.GetComponent<GoatMovementController>(),
				Hooks.GoatLarge.GetComponent<GoatMovementController>()
			};
			StoryController = _storyControllerObject.GetComponent<StoryController>();
			GlobalFollowingEnabled = true;
			CurrentGoatIndex = 0;

			SetCurrentGoatAsActivePlayer();
			EnableFollowing();
			SoundController.Instance.PlayMusic(Music.Instance.ExampleMusicClip);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				ChangeCurrentTarget(true);
			}
			else if (Input.GetKeyDown(KeyCode.Q))
			{
				ChangeCurrentTarget(false);
			}

			if (Input.GetKeyDown(KeyCode.F))
			{
				SoundController.Instance.PlaySingle(Sounds.Instance.FollowingDrumHitClip, 0.6f);
				if (GlobalFollowingEnabled)
				{
					DisableFollowing();
					GlobalFollowingEnabled = false;
				}
				else
				{
					EnableFollowing();
					GlobalFollowingEnabled = true;
				}
			}

			if (TimerStarted)
			{
				_timeLeft -= Time.deltaTime;
				if (_timeLeft < 0)
				{
					StoryController.Events.Game.EndGame();
				}
			}
		}

		#endregion Private Methods
	}

	[Serializable]
	public class Hooks
	{
		#region Public Fields

		public GameObject Camera;
		public GameObject GoatLarge;
		public GameObject GoatMed;
		public GameObject GoatSmall;
		public GameObject Troll;
		public GameObject LeftBound;
		public GameObject RightBound;

		#endregion Public Fields
	}
}