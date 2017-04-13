namespace Backend.StoryEngine.Events
{
    public partial class EventController
    {
        #region Public Properties

        public GameEvents Game { get; private set; }
        public StoryEvents Story { get; private set; }

        #endregion Public Properties

        #region Private Properties

        private StoryController StoryController { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public EventController(StoryController storyController)
        {
            StoryController = storyController;
            Story = new StoryEvents(this);
            Game = new GameEvents(this);
        }

        #endregion Public Constructors
    }
}