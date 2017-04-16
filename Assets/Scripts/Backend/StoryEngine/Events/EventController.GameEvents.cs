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
                _eventController.StoryController.DisplayGameMessage(GruffMessage.FollowingDisabled, 0f, 4f);
            }

            public void FollowingEnabled()
            {
                _eventController.StoryController.DisplayGameMessage(GruffMessage.FollowingEnabled, 0f, 4f);
            }

            public void StartGameTimer()
            {
                _eventController.StoryController.StartTimer();
            }

            public void EndGame()
            {
                _eventController.StoryController.EndGame();

            }

            #endregion Public Methods
        }

        #endregion Public Classes
    }
}