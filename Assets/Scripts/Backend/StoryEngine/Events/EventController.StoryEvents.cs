using Constants;
using Backend.EntityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Backend.StoryEngine.Events
{
    public partial class EventController
    {
        #region Public Classes

        public class StoryEvents
        {
            #region Private Fields

            private GameController _gameController;

            private readonly EventController _eventController;
            private TrollMovementController _trollMovementController;

            #endregion Private Fields

            #region Public Constructors

            public StoryEvents([NotNull] EventController eventController)
            {
                _eventController = eventController;
                _gameController = GameController.Instance;
            }

            #endregion Public Constructors

            #region Public Methods

            public void Intro()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.Example1, 0f, 4f);
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.Example2, 6f, 10f);
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.Example3, 12f, 16f);
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.Example4, 18f, 22f);
                _eventController.Game.Tutorial();
            }

            public void Example1()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.Example1, 0f, 4f);
            }

            public void Example2()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.Example2, 0f, 4f);
            }

            public void Example3()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.Example3, 0f, 4f);
            }

            public void Example4()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.Example4, 0f, 4f);
            }

            #endregion Public Methods

            public void YouAreTheBrothersGruff()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.YouAreTheBrothersGruff, 0f, 4f);
            }

            public void YourLandIsDying()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.YourLandIsDying, 0f, 4f);
            }

            public void YouMustFindANewHome()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.YouMustFindANewHome, 0f, 4f);
            }

            public void YouDoNotHaveMuchTime()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.YouDoNotHaveMuchTime, 0f, 4f);
            }

            public void YouHaveHeardOfALushHillside()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.YouHaveHeardOfALushHillside, 0f, 4f);
            }

            public void FarToTheEastOfHere()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.FarToTheEastOfHere, 0f, 4f);
            }

            public void TheJourneyIsNotEasy()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.TheJourneyIsNotEasy, 0f, 4f);
            }

            public void ItRequiresAgilityAndSkill()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.ItRequiresAgilityAndSkill, 0f, 4f);
            }

            public void YouAndYourBrothersMustWorkTogether()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.YouAndYourBrothersMustWorkTogether, 0f, 4f);
            }


            public void ForInTheShadows()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.ForInTheShadows, 0f, 4f);
            }

            public void AreCreaturesWhoWishYouHarm()
            {
                _gameController = GameController.Instance;

                var goatTriggerDict = new Dictionary<GoatMovementController, string>();

                goatTriggerDict.Add(_gameController.GoatControllerArray[0], "Are creatures you must run from");

                if (_gameController.GoatControllerArray.Length > 1)
                    goatTriggerDict.Add(_gameController.GoatControllerArray[1], "Are creatures who you must face");

                if (_gameController.GoatControllerArray.Length > 2)
                    goatTriggerDict.Add(_gameController.GoatControllerArray[2], "Are creatures who you must fight");

                _trollMovementController = _gameController.Hooks.Troll.GetComponent<TrollMovementController>();

                if (_trollMovementController.TrollAI != null)
                {
                    var triggeringGoat = _trollMovementController.TrollAI.GetClosestGoat();
                    var trollMessage = goatTriggerDict[triggeringGoat];

                    _eventController.StoryController.DisplayStoryMessage(trollMessage, 0f, 4f);
                }
            }

            public void RunFromThemMyChildren()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.RunFromThemMyChildren, 0f, 4f);
            }

            public void AndLeaveTheDarkBehind()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.AndLeaveTheDarkBehind, 0f, 4f);
            }

            public void ANewDawnAwaitsYou()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.ANewDawnAwaitsYou, 0f, 4f);
            }

            public void RiseUpAndShine()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.RiseUpAndShine, 0f, 4f);
            }

            public void AllIsLost()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.AllIsLost, 0f, 4f);
            }
        }

        #endregion Public Classes
    }
}