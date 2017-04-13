using Constants;

namespace Backend
{
    public class StoryEventController
    {
        #region Public Properties

        private StoryController StoryController { get; set; }

        #endregion Public Properties

        #region Public Constructors

        public StoryEventController(StoryController storyController)
        {
            StoryController = storyController;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Example1()
        {
            StoryController.DisplayStoryMessage(StoryMessage.Example1, 0f, 4f);
        }
        public void Example2()
        {
            StoryController.DisplayStoryMessage(StoryMessage.Example2, 6f, 10f);
        }
        public void Example3()
        {
            StoryController.DisplayStoryMessage(StoryMessage.Example3, 12f, 16f);
        }
        public void Example4()
        {
            StoryController.DisplayStoryMessage(StoryMessage.Example4 , 18f, 22f);
        }

        public void FollowingEnabled()
        {
            StoryController.DisplayGameMessage(GruffMessage.FollowingEnabled, 0f, 4f);
        }

        public void FollowingDisabled()
        {
            StoryController.DisplayGameMessage(GruffMessage.FollowingDisabled, 0f, 4f);
        }

        #endregion Public Methods
    }
}