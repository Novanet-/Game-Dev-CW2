using System.Collections;
using Frontend;
using UnityEngine;

namespace Backend
{
    public class StoryController : MonoBehaviour
    {
        #region Private Fields

        [SerializeField] private Canvas _uiCanvas;
        private UIController _uiController;
        private float _globalMessageTimestamp = 0f;
        private float _startTime;

        #endregion Private Fields

        #region Private Methods

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
            StartCoroutine(Delay(5f));
            StoryTest();
        }

        private void StoryTest()
        {
            DisplayStoryMessage("1. This is the beginning", 2f);
            DisplayStoryMessage("2. This is your life", 4f, 5f);
            DisplayStoryMessage("3. This is your ending", 8f, 10f);
            DisplayStoryMessage("4. The cressing of christ", 16f, 15f);
        }

        private void DisplayStoryMessage(string message, float displayTime)
        {
            StartCoroutine(DisplayStoryMessageCoroutine(message, _globalMessageTimestamp));
            StartCoroutine(DisplayStoryMessageCoroutine("", _globalMessageTimestamp += displayTime));
        }

        private void DisplayStoryMessage(string message, float displayTime, float delayTime)
        {
            StartCoroutine(DisplayStoryMessageCoroutine(message, _globalMessageTimestamp += delayTime));
            StartCoroutine(DisplayStoryMessageCoroutine("", _globalMessageTimestamp += displayTime));
        }

        // Update is called once per frame
        private void Update()
        {
//            _globalMessageTimestamp = _startTime + Time.deltaTime;
            _globalMessageTimestamp = Time.time;
            Debug.Log(Time.time);
        }

        #endregion Private Methods

        private IEnumerator Delay(float delay)
        {
            yield return new WaitForSeconds(delay);
        }
    }
}