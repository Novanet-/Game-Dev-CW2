using System.Collections;
using Frontend;
using UnityEngine;

namespace Backend
{
    public class StoryController : MonoBehaviour
    {
        [SerializeField] private Canvas _uiCanvas;
        private UIController _uiController;

        // Use this for initialization
        private void Start()
        {
            _uiController = _uiCanvas.GetComponent<UIController>();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private IEnumerable DisplayStoryMessage(string message, float delay)
        {
            yield return new WaitForSeconds(delay);

            _uiController.DisplayMessage(message, MessageLocation.TopPanel);
        }
    }
}