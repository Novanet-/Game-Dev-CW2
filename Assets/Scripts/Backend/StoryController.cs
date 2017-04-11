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

        #endregion Private Fields

        #region Private Methods

        private IEnumerator DisplayStoryMessage(string message, float delay)
        {
            yield return new WaitForSeconds(delay);

            _uiController.DisplayMessage(message, MessageLocation.TopPanel);
        }

        // Use this for initialization
        private void Start()
        {
            _uiController = _uiCanvas.GetComponent<UIController>();
            StartCoroutine(Delay(5f));
            StoryTest();
        }

        private void StoryTest()
        {
            var delay = 0f;
            StartCoroutine(DisplayStoryMessage("1. This is the beginning", delay += 1f));
            StartCoroutine(DisplayStoryMessage("2. This is your life", delay += 5f));
            StartCoroutine(DisplayStoryMessage("3. This is your ending", delay += 5f));
            StartCoroutine(DisplayStoryMessage("4. The cressing of christ", delay += 5f));
        }

        // Update is called once per frame
        private void Update()
        {
        }

        #endregion Private Methods

        private IEnumerator Delay(float delay)
        {
            yield return new WaitForSeconds(delay);
        }
    }
}