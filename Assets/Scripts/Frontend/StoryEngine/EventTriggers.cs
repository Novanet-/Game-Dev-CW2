using System;
using System.Collections.Generic;
using Backend.StoryEngine;
using Backend.StoryEngine.Events;
using com.kleberswf.lib.core;
using UnityEngine;

namespace Frontend.StoryEngine
{
    internal class EventTriggers : Singleton<EventTriggers>
    {
        private EventController.StoryEvents _storyEvents;
        private EventController.GameEvents _gameEvents;

        [SerializeField] private GameObject _storyControllerObject;

        public Dictionary<string, Action> EventTriggerDictionary { get; set; }

        private void Update()
        {
            if (EventTriggerDictionary == null) InitializeDictionary();
        }
        private void InitializeDictionary()
        {
            var storyController = _storyControllerObject.GetComponent<StoryController>();
            var storyControllerEvents = storyController.Events;
            _storyEvents = storyControllerEvents.Story;
            _gameEvents = storyControllerEvents.Game;
            EventTriggerDictionary = new Dictionary<string, Action>()
            {
                {"YouAreTheBrothersGruff", _storyEvents.YouAreTheBrothersGruff},
                {"YourLandIsDying", _storyEvents.YourLandIsDying},
                {"YouMustFindANewHome", _storyEvents.YouMustFindANewHome},
                {"YouDoNotHaveMuchTime", _storyEvents.YouDoNotHaveMuchTime},
                {"YouHaveHeardOfALushHillside", _storyEvents.YouHaveHeardOfALushHillside},
                {"FarToTheEastOfHere", _storyEvents.FarToTheEastOfHere},
                {"TheJourneyIsNotEasy", _storyEvents.TheJourneyIsNotEasy},
                {"ItRequiresAgilityAndSkill", _storyEvents.ItRequiresAgilityAndSkill},
                {"YouAndYourBrothersMustWorkTogether", _storyEvents.YouAndYourBrothersMustWorkTogether},
                {"ForInTheShadows", _storyEvents.ForInTheShadows},
                {"AreCreaturesWhoWishYouHarm", _storyEvents.AreCreaturesWhoWishYouHarm},
                {"RunFromThemMyChildren", _storyEvents.RunFromThemMyChildren},
                {"AndLeaveTheDarkBehind", _storyEvents.AndLeaveTheDarkBehind},
                {"ANewDawnAwaitsYou", _storyEvents.ANewDawnAwaitsYou},
                {"RiseUpAndShine", _storyEvents.RiseUpAndShine},
				{"JourneysEnd", _storyEvents.JourneysEnd},


                {"WASDToMove", _gameEvents.WASDToMove},
                {"GoatSwitch", _gameEvents.GoatSwitch},
                {"FollowToggle", _gameEvents.FollowToggle}
            };
        }
    }
}