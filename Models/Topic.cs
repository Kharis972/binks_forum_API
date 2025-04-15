namespace binks_forum_API.Models
{
    public class Topic
    {
        //Variables
        //private used only inside constructors
        private int _topicId;
        private string _description = null!;
        private string _userId = null!;
        private string _name = null!;
        private string _subjects = null!;
        private int _views;
        private int _participants;
        private string? _topicIcon;
        private DateTime _creationDate;

        public User User { get; private set; } = null!;

        public List<TopicMessages>? TopicMessages {get; set; }

        //default contructor by FluentAPI
        public Topic() {}

        //Constructor with parameters
        public Topic
        (
            
            string description,
            string userId,
            string name,
            string subjects,
            int views,
            int participants,
            string topicIcon,
            DateTime creationDate
        )
        {
            _description = description;
            _userId = userId;
            _name = name;
            _subjects = subjects;
            _views = views;
            _participants = participants;
            _topicIcon = topicIcon;
            _creationDate = creationDate;
            
        }

        //Access property
        public int TopicId
        {
            get => _topicId;
        }
        public string Description
        {
            get => _description;
            set => _description = value;
        }
        public string UserId
        {
            get => _userId;
        }
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public string Subjects
        {
            get => _subjects;
            set => _subjects = value;
        }
        public int Views
        {
            get => _views;
            set => _views = value;
        }
        public int Participants
        {
            get => _participants;
            set => _participants = value;
        }
        public string? TopicIcon
        {
            get => _topicIcon;
            set => _topicIcon = value;
        }
        public DateTime CreationDate
        {
            get => _creationDate;
        }

    }
}