using Constants;

namespace Backend.StoryEngine.Events
{
    public partial class EventController
    {
        #region Public Classes

        public class GameEvents
        {
            #region Private Fields

            private readonly EventController _eventController;

            #endregion Private Fields

            #region Public Constructors

            public GameEvents(EventController eventController)
            {
                _eventController = eventController;
            }

            #endregion Public Constructors

            #region Public Methods

            public void FollowingDisabled()
            {
                _eventController.StoryController.DisplayGameMessage(GameMessage.FollowingDisabled, 0f, 4f);
            }

            public void FollowingEnabled()
            {
                _eventController.StoryController.DisplayGameMessage(GameMessage.FollowingEnabled, 0f, 4f);
            }

            public void StartGameTimer()
            {
                _eventController.StoryController.StartTimer();
            }

            public void EndGame()
            {
                _eventController.StoryController.EndGame();

            }

            public void Tutorial()
            {
                _eventController.StoryController.DisplayGameMessage(GameMessage.WASDToMove, 4f, 8f);
                _eventController.StoryController.DisplayGameMessage(GameMessage.GoatSwitch, 10f, 14f);
                _eventController.StoryController.DisplayGameMessage(GameMessage.FollowToggle, 16f, 20f);
            }

            #endregion Public Methods

            public void WASDToMove()
            {
                _eventController.StoryController.DisplayGameMessage(GameMessage.WASDToMove, 0f, 4f);
            }

            public void GoatSwitch()
            {
                _eventController.StoryController.DisplayGameMessage(GameMessage.GoatSwitch, 0f, 4f);
            }

            public void FollowToggle()
            {
                _eventController.StoryController.DisplayGameMessage(GameMessage.FollowToggle, 0f, 4f);
            }
        }

        #endregion Public Classes
    }
}