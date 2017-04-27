using System;
using System.Collections;
using System.Collections.Generic;
using com.kleberswf.lib.core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Frontend.UIEngine
{
    public class UIController : Singleton<UIController>
    {
        #region Private Fields

        [SerializeField] private GameObject _pnlBottom;
        [SerializeField] private GameObject _pnlCentre;
        [SerializeField] private GameObject _pnlTop;
        [SerializeField] private GameObject _pnlAll;

        private CanvasGroup canvasGroup;

        #endregion Private Fields

        #region Private Properties

        private bool FadeinFinished { get; set; }
        private bool FadeoutFinished { get; set; }
        private Dictionary<MessageLocation, Text> TextBoxDictionary { get; set; }
        private Text TxtBottom { get; set; }
        private Text TxtCentre { get; set; }
        private Text TxtTop { get; set; }

        #endregion Private Properties

        #region Public Methods

        public void DisplayMessage(string message, MessageLocation location)
        {
            if (TextBoxDictionary == null)
                throw new GruffException("Textbox dictionary was not created");

            if (TextBoxDictionary.Count != Enum.GetNames(typeof(MessageLocation)).Length)
                throw new GruffException("Message Location enum and textbox dictionary do not match");

            Text targetTextBox;

            bool fetchTextboxSuccess = TextBoxDictionary.TryGetValue(location, out targetTextBox);

            if (!fetchTextboxSuccess)
                throw new GruffException("Message location not found in dictionary");

            var targetTextController = targetTextBox.GetComponent<TextController>();
            if (message != null) targetTextController.SetText(message);
        }

        public void FadeOutGame()
        {
            StartCoroutine(FadeOutCoroutine());
        }

        #endregion Public Methods

        #region Private Methods

        private IEnumerator FadeOutCoroutine()
        {
//            yield return new WaitForSeconds(4f);
//
//            _pnlTop.GetComponent<Image>().CrossFadeAlpha(0f, 0.01f, false);
//            _pnlCentre.GetComponent<Image>().CrossFadeAlpha(0f, 0.01f, false);
//            _pnlBottom.GetComponent<Image>().CrossFadeAlpha(0f, 0.01f, false);
//
//            _pnlTop.GetComponent<CanvasGroup>().alpha = 1f;
//            _pnlCentre.GetComponent<CanvasGroup>().alpha = 1f;
//            _pnlBottom.GetComponent<CanvasGroup>().alpha = 1f;
//
//            _pnlTop.GetComponent<Image>().enabled = true;
//            _pnlCentre.GetComponent<Image>().enabled = true;
//            _pnlBottom.GetComponent<Image>().enabled = true;
//
//            _pnlTop.GetComponent<Image>().color = Color.black;
//            _pnlCentre.GetComponent<Image>().color = Color.black;
//            _pnlBottom.GetComponent<Image>().color = Color.black;
//
//            _pnlTop.GetComponent<CanvasGroup>().alpha = 1f;
//            _pnlCentre.GetComponent<CanvasGroup>().alpha = 1f;
//            _pnlBottom.GetComponent<CanvasGroup>().alpha = 1f;
//
//            _pnlTop.GetComponent<Image>().CrossFadeAlpha(1f, 5f, false);
//            _pnlCentre.GetComponent<Image>().CrossFadeAlpha(1f, 5f, false);
//            _pnlBottom.GetComponent<Image>().CrossFadeAlpha(1f, 5f, false);
//
//            yield return new WaitForSeconds(5f);
//
//            _pnlTop.GetComponent<CanvasGroup>().alpha = 1f;
//            _pnlCentre.GetComponent<CanvasGroup>().alpha = 1f;
//            _pnlBottom.GetComponent<CanvasGroup>().alpha = 1f;

//            SceneManager.LoadSceneAsync("TitleScene");

            yield return new WaitForSeconds(2f);

            _pnlAll.GetComponent<Image>().CrossFadeAlpha(0f, 0.01f, false);
            _pnlAll.GetComponent<CanvasGroup>().alpha = 1f;
            _pnlAll.GetComponent<Image>().enabled = true;
            _pnlAll.GetComponent<Image>().color = Color.black;
            _pnlAll.GetComponent<Image>().CrossFadeAlpha(1f, 5f, false);

            yield return new WaitForSeconds(5f);

            SceneManager.LoadSceneAsync("TitleScene");
        }

        // Use this for initialization
        private void Start()
        {
            var topTextbox = HelperFunctions.GetChildUITextboxFromPanel(_pnlTop);
            var centreTextbox = HelperFunctions.GetChildUITextboxFromPanel(_pnlCentre);
            var bottomTextbox = HelperFunctions.GetChildUITextboxFromPanel(_pnlBottom);

            if (topTextbox != null) TxtTop = topTextbox.GetComponent<Text>();
            else throw new GruffException("Top textbox not found");

            if (centreTextbox != null) TxtCentre = centreTextbox.GetComponent<Text>();
            else throw new GruffException("Centre textbox not found");

            if (bottomTextbox != null) TxtBottom = bottomTextbox.GetComponent<Text>();
            else throw new GruffException("bottom textbox not found");

            TextBoxDictionary = new Dictionary<MessageLocation, Text>
            {
                {MessageLocation.TopPanel, TxtTop},
                {MessageLocation.CentrePanel, TxtCentre},
                {MessageLocation.BottomPanel, TxtBottom}
            };
        }

        // Update is called once per frame
        private void Update()
        {
        }

        #endregion Private Methods
    }
}