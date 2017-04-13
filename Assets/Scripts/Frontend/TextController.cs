using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace frontend
{
    public class TextController : MonoBehaviour
    {
        #region Private Fields

        [SerializeField] private Text _currentText;
        [SerializeField] private float _defaultFadeSpeed = 2f;
        [SerializeField] private float _defaultMaxAlpha = 0.8f;
        private CanvasGroup _parentCanvasGroup;
        private GameObject _parentPanel;
        [SerializeField] private string _targetTextString;

        #endregion Private Fields

        #region Private Methods

        public void SetText(string newText)
        {
            Debug.Log(string.Format("Setting {0} textbox to {1} at {2}", _parentPanel.name, newText, Mathf.Round(Time.time)));
            _targetTextString = newText;
        }

        // Use this for initialization
        private void Start()
        {
            _parentPanel = gameObject.transform.parent.gameObject;
            _parentCanvasGroup = _parentPanel.GetComponent<CanvasGroup>();
            _currentText = gameObject.GetComponent<Text>();
            _targetTextString = "";
        }

        // Update is called once per frame
        private void Update()
        {

            bool textNeedsUpdate = !_currentText.text.Equals(_targetTextString);
//        if (string.IsNullOrEmpty(_currentText.text) && string.IsNullOrEmpty(_targetTextString)) textNeedsUpdate = false;

            if (textNeedsUpdate)
            {
                StartCoroutine(HelperFunctions.FadeOut(_parentCanvasGroup, _defaultFadeSpeed));
            }
            else if (_parentCanvasGroup.alpha < _defaultMaxAlpha)
            {
                StartCoroutine(HelperFunctions.FadeIn(_parentCanvasGroup, _defaultFadeSpeed, _defaultMaxAlpha));
            }

            if (_parentCanvasGroup.alpha < 0.05 && textNeedsUpdate)
            {
                _currentText.text = _targetTextString;
                _parentCanvasGroup.alpha = 0;
            }


//        Debug.Log(string.Format("Current Text: {0}, Target Text: {1}", _currentText.text, _targetTextString));
        }

        #endregion Private Methods

    }
}