using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Frontend
{
    public class UIController : MonoBehaviour
    {
        #region Private Fields

        [SerializeField] private GameObject _pnlBottom;
        [SerializeField] private GameObject _pnlCentre;
        [SerializeField] private GameObject _pnlTop;

        [SerializeField] private float _defaultFadeSpeed = 2f; //500ms
        [SerializeField] private float _defaultMaxAlpha = 0.8f;

        #endregion Private Fields

        #region Private Properties

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
            bool success = TextBoxDictionary.TryGetValue(location, out targetTextBox);

            if (!success)
                throw new GruffException("Message location not found in dictionary");

            targetTextBox.GetComponent<TextController>().SetText(message);
//            CrossfadeTextChangeDefault(targetTextBox, message);
        }

        #endregion Public Methods

        #region Private Methods

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

//        private void CrossfadeTextChangeDefault(Text textComponent, string newMessage)
//        {
//            
//            CrossfadeTextChange(textComponent, newMessage, _defaultFadeSpeed, _defaultMaxAlpha);
//        }
//
//        private void CrossfadeTextChange(Text textComponent, string newMessage, float fadeSpeed, float maxAlpha)
//        {
//            var canvasGroupComponent = (CanvasGroup)textComponent.gameObject.GetComponentInParent(typeof(CanvasGroup));
//
//            StartCoroutine(HelperFunctions.FadeOut(canvasGroupComponent, fadeSpeed));
//            textComponent.text = newMessage;
//            StartCoroutine(HelperFunctions.FadeIn(canvasGroupComponent, fadeSpeed, maxAlpha));
//        }
    }
}