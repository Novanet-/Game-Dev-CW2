using System.Collections;
using Frontend;
using UnityEngine;

namespace Backend
{
    public class StoryController : MonoBehaviour
    {
        #region Private Fields

        private float _globalMessageTimestamp = 0f;
        private float _startTime;
        [SerializeField] private Canvas _uiCanvas;
        private UIController _uiController;

        #endregion Private Fields

        #region Public Properties

        public StoryEventController StoryEvents { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void StoryTest()
        {
            DisplayStoryMessage("1. This is the beginning", 2f);
            DisplayStoryMessage("2. This is your life", 4f, 5f);
            DisplayStoryMessage("3. This is your ending", 8f, 10f);
            DisplayStoryMessage("4. The cressing of christ", 16f, 15f);
        }

        #endregion Public Methods

        #region Internal Methods

        internal void DisplayStoryMessage(string message, float displayTime)
        {
            StartCoroutine(DisplayStoryMessageCoroutine(message, _globalMessageTimestamp));
            StartCoroutine(DisplayStoryMessageCoroutine("", _globalMessageTimestamp += displayTime));
        }

        #endregion Internal Methods

        #region Private Methods

        private void DisplayStoryMessage(string message, float displayTime, float delayTime)
        {
            StartCoroutine(DisplayStoryMessageCoroutine(message, _globalMessageTimestamp += delayTime));
            StartCoroutine(DisplayStoryMessageCoroutine("", _globalMessageTimestamp += displayTime));
        }

        private IEnumerator DisplayStoryMessageCoroutine(string message, float displayTime)
        {
            yield return new WaitForSeconds(displayTime);
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
            _globalMessageTimestamp = Time.time;
            Debug.Log(Time.time);
        }

        #endregion Private Methods
    }
}