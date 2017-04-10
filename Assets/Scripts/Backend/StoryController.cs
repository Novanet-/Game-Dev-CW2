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

        private IEnumerable DisplayStoryMessage(string message, float delay)
        {
            yield return new WaitForSeconds(delay);

            _uiController.DisplayMessage(message, MessageLocation.TopPanel);
        }

        // Use this for initialization
        private void Start()
        {
            _uiController = _uiCanvas.GetComponent<UIController>();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        #endregion Private Methods
    }
}