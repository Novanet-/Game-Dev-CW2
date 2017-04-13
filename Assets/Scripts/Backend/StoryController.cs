using System;
using System.Collections;
using Constants;
using frontend;
using JetBrains.Annotations;
using UnityEngine;

namespace Backend
{
    public class StoryController : MonoBehaviour
    {
        #region Private Fields

        private float _storyMessageTimestamp = 0f;
        private float _gameMessageTimestamp = 0f;
        private float _startTime;
        [SerializeField] private Canvas _uiCanvas;
        private UIController _uiController;

        #endregion Private Fields

        #region Public Properties

        public StoryEventController StoryEvents { get; private set; }

        #endregion Public Properties

        #region Public Methods

        private void StoryTest()
        {
            StoryEvents.Example1();
            StoryEvents.Example2();
            StoryEvents.Example3();
            StoryEvents.Example4();
//            DisplayStoryMessage(StoryMessage.Example1, 0f, 4f);
//            DisplayStoryMessage(StoryMessage.Example2, 6f, 10f);
//            DisplayStoryMessage(StoryMessage.Example3, 12f, 16f);
//            DisplayStoryMessage(StoryMessage.Example4, 18f, 22f);
        }

        #endregion Public Methods

        #region Internal Methods


        #endregion Internal Methods

        #region Private Methods

        public float DisplayGameMessage(string message, float displayStartTime, float displayEndTime)
        {
            Debug.Log(string.Format("{0}, DisplayStart = {1}, DisplayEnd = {2}", message, displayStartTime, displayEndTime));

            StartCoroutine(DisplayGameMessageCoroutine(message, displayStartTime));
            StartCoroutine(DisplayGameMessageCoroutine("", displayEndTime));
            return displayEndTime;
        }

        private IEnumerator DisplayGameMessageCoroutine(string message, float displayStartTime)
        {
            yield return new WaitForSeconds(displayStartTime);
            _uiController.DisplayMessage(message, MessageLocation.BottomPanel);
        }

        public float DisplayStoryMessage(string message, float displayStartTime, float displayEndTime)
        {
            Debug.Log(string.Format("{0}, DisplayStart = {1}, DisplayEnd = {2}", message, displayStartTime, displayEndTime));

            StartCoroutine(DisplayStoryMessageCoroutine(message, displayStartTime));
            StartCoroutine(DisplayStoryMessageCoroutine("", displayEndTime));
            return displayEndTime;
        }

        private IEnumerator DisplayStoryMessageCoroutine(string message, float displayStartTime)
        {
            yield return new WaitForSeconds(displayStartTime);
            _uiController.DisplayMessage(message, MessageLocation.TopPanel);
        }

        // Use this for initialization
        private void Start()
        {
            _startTime = Time.time;
            _uiController = _uiCanvas.GetComponent<UIController>();
            StoryEvents = new StoryEventController(this);
            StoryTest();
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