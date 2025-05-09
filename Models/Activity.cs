namespace binks_forum_API.Models
{
    public class Activity
    {
        private int _id;
        private string _name = null!;
        private string _description = null!;
        private DateTime _creationDate;
        private DateTime _scheduledDate;
        private DateTime _endingDate;
        private string _activity_type = null!;
        private bool _is_featured;
        private string? _userId;

        public Activity() {}
        public User? User { get; set; }
        // public List<Participation>? Participations { get; set; } = new List<Participation>();

        public Activity
        (
            string name,
            string description,
            DateTime creationDate,
            DateTime scheduledDate,
            DateTime endingDate,
            string activity_type,
            bool is_featured,
            string userId
        )
        {
            _name = name;
            _description = description;
            _creationDate = creationDate;
            _scheduledDate = scheduledDate;
            _endingDate = endingDate;
            _activity_type = activity_type;
            _is_featured = is_featured;
            _userId = userId;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public string Description
        {
            get => _description;
            set => _description = value;
        }
        public DateTime CreationDate
        {
            get => _creationDate;
            init => _creationDate = value;
        }
        public DateTime ScheduledDate
        {
            get => _scheduledDate;
            set => _scheduledDate = value;
        }
        public DateTime EndingDate
        {
            get => _endingDate;
            set => _endingDate = value;
        }
        public string Activity_type
        {
            get => _activity_type;
            set => _activity_type = value;
        }
        public bool Is_featured
        {
            get => _is_featured;
            set => _is_featured = value;
        }
        public string? UserId => _userId;
    }
}