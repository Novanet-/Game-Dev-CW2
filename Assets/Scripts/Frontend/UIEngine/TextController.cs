using Backend.StoryEngine;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Frontend.UIEngine
{
    public class TextController : MonoBehaviour
    {
        #region Private Fields

        [SerializeField] private Text _currentText;
        [SerializeField] private float _defaultFadeSpeed = 2f;
        [SerializeField] private float _defaultMaxAlpha = 0.8f;
        private CanvasGroup _parentCanvasGroup;
        private GameObject _parentPanel;
        private StoryController _storyController;
        [SerializeField] private string _targetTextString;

        private string _textboxName;

        #endregion Private Fields

        #region Public Methods

        public void SetText(string newText)
        {
            _targetTextString = newText;
            SetBusy(true);
        }

        #endregion Public Methods

        #region Private Methods

        private void SetBusy(bool isBusy)
        {
            if (_textboxName.Equals("txtTop"))
                _storyController.StoryBusy = isBusy;
            else
                _storyController.GameBusy = isBusy;
        }

        // Use this for initialization
        private void Start()
        {
            _parentPanel = gameObject.transform.parent.gameObject;
            _parentCanvasGroup = _parentPanel.GetComponent<CanvasGroup>();
            _currentText = gameObject.GetComponent<Text>();
            _targetTextString = "";
            _textboxName = gameObject.name;
            _storyController = StoryController.Instance;
        }

        // Update is called once per frame
        private void Update()
        {
            bool textNeedsUpdate = !_currentText.text.Equals(_targetTextString);
            //        if (string.IsNullOrEmpty(_currentText.text) && string.IsNullOrEmpty(_targetTextString)) textNeedsUpdate = false;

            if (textNeedsUpdate)
            {
                StartCoroutine(HelperFunctions.FadeOut(_parentCanvasGroup, _defaultFadeSpeed));
                SetBusy(true);
            }
            else if (_parentCanvasGroup.alpha < _defaultMaxAlpha)
            {
                StartCoroutine(HelperFunctions.FadeIn(_parentCanvasGroup, _defaultFadeSpeed, _defaultMaxAlpha));
                SetBusy(true);
            }

            if (_parentCanvasGroup.alpha > 0.79 && string.IsNullOrEmpty(_currentText.text) && string.IsNullOrEmpty(_targetTextString))
            {
                SetBusy(false);
            }

            if (_parentCanvasGroup.alpha < 0.05 && textNeedsUpdate)
            {
                _currentText.text = _targetTextString;
                _parentCanvasGroup.alpha = 0;
            }
        }

        #endregion Private Methods
    }
}