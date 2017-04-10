using System;
using System.Collections.Generic;
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
            if (TextBoxDictionary.Count != Enum.GetNames(typeof(MessageLocation)).Length)
                throw new GruffException("Message Location enum and textbox dictionary do not match");

            Text targetTextBox;
            bool success = TextBoxDictionary.TryGetValue(location, out targetTextBox);

            if (!success)
                throw new GruffException("Message location not found in dictionary");

            targetTextBox.text = message;
        }

        #endregion Public Methods

        #region Private Methods

        // Use this for initialization
        private void Start()
        {
            TxtTop = _pnlTop.GetComponent<Text>();
            TxtCentre = _pnlCentre.GetComponent<Text>();
            TxtBottom = _pnlBottom.GetComponent<Text>();

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