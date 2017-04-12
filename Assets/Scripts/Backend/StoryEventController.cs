namespace Backend
{
    public class StoryEventController
    {
        #region Public Properties

        public StoryController StoryController { get; private set; }

        #endregion Public Properties

        #region Public Constructors

        public StoryEventController(StoryController storyController)
        {
            StoryController = storyController;
        }

        #endregion Public Constructors

        #region Public Methods

        public void ExampleEvent()
        {
            StoryController.DisplayStoryMessage("E. This is an example message", 5f);
        }

        public void FollowingEnabled()
        {
            StoryController.DisplayStoryMessage("FOLLOWING HAS BEEN ENABLED", 2f);
        }

        public void FollowingDisabled()
        {
            StoryController.DisplayStoryMessage("FOLLOWING HAS BEEN DISABLED", 2f);
        }

        #endregion Public Methods
    }
}