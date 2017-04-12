using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class TextController : MonoBehaviour
{
    #region Private Fields

    private Text _currentText;
    [SerializeField] private float _defaultFadeSpeed = 2f;
    [SerializeField] private float _defaultMaxAlpha = 0.8f;
    private CanvasGroup _parentCanvasGroup;
    private GameObject _parentPanel;
    private string _targetTextString;

    #endregion Private Fields

    #region Private Methods

    public void SetText(string newText)
    {
        Debug.Log(string.Format("Setting {0} textbox to {1}", _parentPanel.name, newText));
        _targetTextString = newText;
    }

    // Use this for initialization
    private void Start()
    {
        _parentPanel = gameObject.transform.parent.gameObject;
        _parentCanvasGroup = _parentPanel.GetComponent<CanvasGroup>();
        _currentText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        float alpha = _parentCanvasGroup.alpha;

        bool textNeedsUpdate = (_currentText.text != _targetTextString);
        bool isTextTransitioning = (alpha < _defaultMaxAlpha);
//        bool fadingOut = isTextTransitioning && textNeedsUpdate;
//        bool fadingIn = isTextTransitioning && !textNeedsUpdate;

//        Debug.Log(string.Format("Text Needs Update: {0}, isTexttransitioning: {1}, fadingOut: {2}, fadingIn: {3}, Alpha: {4}", textNeedsUpdate,
//            isTextTransitioning,
//            fadingOut, fadingIn, alpha));


        if (textNeedsUpdate)
        {
            StartCoroutine(HelperFunctions.FadeOut(_parentCanvasGroup, _defaultFadeSpeed));
        }
        else if (alpha < _defaultMaxAlpha)
        {
            StartCoroutine(HelperFunctions.FadeIn(_parentCanvasGroup, _defaultFadeSpeed, _defaultMaxAlpha));
        }

        if (alpha < 0.01)
            _currentText.text = _targetTextString;


//        Debug.Log(string.Format("Current Text: {0}, Target Text: {1}", _currentText.text, _targetTextString));
    }

    #endregion Private Methods

}