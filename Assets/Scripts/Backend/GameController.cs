using System;
using System.Linq;
using Frontend;
using UnityEngine;

namespace Backend
{
    public class GameController : MonoBehaviour
    {
        #region Public Fields

        public float GravityStrength;
        public Hooks Hooks = new Hooks();

        #endregion Public Fields

        #region Private Fields

        private int _currentGoatIndex;
        private bool _globalFollowingEnabled;
        private PlayerMovementController[] _goatControllerArray;
        private StoryController _storyController;
        [SerializeField] private GameObject _storyControllerObject;
        private float _timeStart;

        #endregion Private Fields

        #region Private Properties

        private PlayerMovementController CurrentGoat
        {
            get { return _goatControllerArray[_currentGoatIndex]; }
        }

        #endregion Private Properties

        #region Private Methods

        private void ChangeCurrentTarget(bool isRight)
        {
            int newIndex = (isRight ? _currentGoatIndex + 1 : _currentGoatIndex + 2) % _goatControllerArray.Length;
            SetCurrentTarget(newIndex);
        }

        private void DisableFollowing()
        {
            foreach (var goatController in _goatControllerArray)
            {
                if (goatController != CurrentGoat) goatController.DisableFollowing();
            }
        }

        private void EnableFollowing()
        {
            foreach (var goatController in _goatControllerArray)
            {
                if (goatController != CurrentGoat) goatController.EnableFollowing(CurrentGoat);
            }
        }

        private void SetCurrentGoatAsActivePlayer()
        {
            foreach (var goatController in _goatControllerArray)
            {
                goatController.IsActivePlayer = goatController == CurrentGoat;
            }
        }

        private void SetCurrentTarget(int newIndex)
        {
            DisableFollowing();

            _currentGoatIndex = newIndex;
            SetCurrentGoatAsActivePlayer();

            Hooks.Camera.GetComponent<CameraController>().CurrentTarget = CurrentGoat.transform;

            if (_globalFollowingEnabled) EnableFollowing();
        }

        private void Start()
        {
            Physics.gravity = new Vector3(0f, -GravityStrength, 0f);
            _goatControllerArray = new[]
            {
                Hooks.GoatSmall.GetComponent<PlayerMovementController>(),
                Hooks.GoatMed.GetComponent<PlayerMovementController>(),
                Hooks.GoatLarge.GetComponent<PlayerMovementController>()
            };
            _storyController = _storyControllerObject.GetComponent<StoryController>();
            _globalFollowingEnabled = true;
            _currentGoatIndex = 0;

            SetCurrentGoatAsActivePlayer();
            EnableFollowing();

            _timeStart = Time.time;
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
                if (_globalFollowingEnabled) DisableFollowing(); else EnableFollowing();
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