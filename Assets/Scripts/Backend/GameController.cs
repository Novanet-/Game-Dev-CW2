using System;
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

        private PlayerMovementController CurrentGoat
        {
            get { return GoatControllerArray[CurrentGoatIndex]; }
        }

        private int CurrentGoatIndex { get; set; }
        private bool GlobalFollowingEnabled { get; set; }
        private PlayerMovementController[] GoatControllerArray { get; set; }
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

        private void ChangeCurrentTarget(bool isRight)
        {
            int newIndex = (isRight ? CurrentGoatIndex + 1 : CurrentGoatIndex + 2) % GoatControllerArray.Length;
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

            if (GlobalFollowingEnabled) EnableFollowing();
        }

        private void Start()
        {
            Physics.gravity = new Vector3(0f, -GravityStrength, 0f);
            GoatControllerArray = new[]
            {
                Hooks.GoatSmall.GetComponent<PlayerMovementController>(),
                Hooks.GoatMed.GetComponent<PlayerMovementController>(),
                Hooks.GoatLarge.GetComponent<PlayerMovementController>()
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
                SoundController.Instance.PlaySingle(Sounds.Instance.ExampleSoundClip);
                if (GlobalFollowingEnabled)
                {
                    DisableFollowing();
                    GlobalFollowingEnabled = false;
                    Debug.Log(string.Format("Global following: {0}", GlobalFollowingEnabled));
                    StoryController.Events.Game.FollowingDisabled();
                }
                else
                {
                    EnableFollowing();
                    GlobalFollowingEnabled = true;
                    Debug.Log(string.Format("Global following: {0}", GlobalFollowingEnabled));
                    StoryController.Events.Game.FollowingEnabled();
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

        #endregion Public Fields
    }
}