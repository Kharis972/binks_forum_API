namespace binks_forum_API.Models
{
    public class Activity
    {
        private int _id;
        private string _name;
        private string _description;
        private DateTime _creationDate;
        private DateTime _scheduledDate;
        private DateTime _endingDate;
        private string _userId =null!;

        public Activity() {}
        public Admin? Admin { get; set; }
        public Modo? Modo { get; set; }

        public Activity
        (
            string name,
            string description,
            DateTime creationDate,
            DateTime scheduledDate,
            DateTime endingDate,
            string userId
        )
        {
            _name = name;
            _description = description;
            _creationDate = creationDate;
            _scheduledDate = scheduledDate;
            _endingDate = endingDate;
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
            init => _scheduledDate = value;
        }
        public DateTime EndingDate
        {
            get => _endingDate;
            set => _endingDate = value;
        }
    }
}