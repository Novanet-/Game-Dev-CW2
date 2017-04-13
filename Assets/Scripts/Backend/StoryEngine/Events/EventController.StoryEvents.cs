using Constants;

namespace Backend.StoryEngine.Events
{
    public partial class EventController
    {
        #region Public Classes

        public class StoryEvents
        {
            #region Private Fields

            private readonly EventController _eventController;

            #endregion Private Fields

            #region Public Constructors

            public StoryEvents(EventController eventController)
            {
                _eventController = eventController;
            }

            #endregion Public Constructors

            #region Public Methods

            public void Example1()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.Example1, 0f, 4f);
            }

            public void Example2()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.Example2, 6f, 10f);
            }

            public void Example3()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.Example3, 12f, 16f);
            }

            public void Example4()
            {
                _eventController.StoryController.DisplayStoryMessage(StoryMessage.Example4, 18f, 22f);
            }

            #endregion Public Methods
        }

        #endregion Public Classes
    }
}