using System.Collections;
using Backend.StoryEngine.Events;
using com.kleberswf.lib.core;
using Frontend.UIEngine;
using JetBrains.Annotations;
using UnityEngine;

namespace Backend.StoryEngine
{
    public class StoryController : Singleton<StoryController>
    {
        #region Private Fields

        private float _gameMessageTimestamp;
        private float _startTime;
        private float _storyMessageTimestamp;
        [SerializeField] private Canvas _uiCanvas;
        private UIController _uiController;

        #endregion Private Fields

        #region Public Properties

        public EventController Events { get; private set; }
        public GameController Game { get; private set; }

        public bool GameBusy { get; set; }
        public bool StoryBusy { get; set; }

        #endregion Public Properties

        #region Public Methods

        public float DisplayCentreMessage([NotNull] string message, float displayStartTime, float displayEndTime)
        {
            StartCoroutine(DisplayCentreMessageCoroutine(message, displayStartTime));
            StartCoroutine(DisplayCentreMessageCoroutine("", displayEndTime));
            return displayEndTime;
        }

        public float DisplayGameMessage([NotNull] string message, float displayStartTime, float displayEndTime)
        {
            StartCoroutine(DisplayGameMessageCoroutine(message, displayStartTime));
            StartCoroutine(DisplayGameMessageCoroutine("", displayEndTime));
            return displayEndTime;
        }

        public float DisplayStoryMessage([NotNull] string message, float displayStartTime, float displayEndTime)
        {
            StartCoroutine(DisplayStoryMessageCoroutine(message, displayStartTime));
            StartCoroutine(DisplayStoryMessageCoroutine("", displayEndTime));
            return displayEndTime;
        }

        public void QuitGame()
        {
            GameController.QuitGame();
        }

        public void StartTimer()
        {
            Game.StartGameTimer();
        }

        #endregion Public Methods

        #region Private Methods

        private void Awake()
        {
        }

        private IEnumerator DisplayCentreMessageCoroutine([NotNull] string message, float displayStartTime)
        {
            yield return new WaitUntil(() => !GameBusy);
            yield return new WaitForSeconds(displayStartTime);

            _uiController.DisplayMessage(message, MessageLocation.CentrePanel);
        }

        private IEnumerator DisplayGameMessageCoroutine([NotNull] string message, float displayStartTime)
        {
            yield return new WaitUntil(() => !GameBusy);
            yield return new WaitForSeconds(displayStartTime);

            _uiController.DisplayMessage(message, MessageLocation.BottomPanel);
        }

        private IEnumerator DisplayStoryMessageCoroutine([NotNull] string message, float displayStartTime)
        {
            yield return new WaitUntil(() => !StoryBusy);
            yield return new WaitForSeconds(displayStartTime);

            _uiController.DisplayMessage(message, MessageLocation.TopPanel);
        }

        // Use this for initialization
        private void Start()
        {
            _startTime = Time.time;
            _uiController = _uiCanvas.GetComponent<UIController>();
            Events = new EventController(this);
            StoryBusy = false;
            GameBusy = false;
            //            StoryTest();
        }

        private void StoryTest()
        {
            //            Events.Story.Example1();
            //            Events.Story.Example2();
            //            Events.Story.Example3();
            //            Events.Story.Example4();
            //            Events.Game.QuitGame();
        }

        // Update is called once per frame
        private void Update()
        {
            _storyMessageTimestamp = Time.time;
            _gameMessageTimestamp = Time.time;
            //            Debug.Log(Time.time);
        }

        #endregion Private Methods
    }
}