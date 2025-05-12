namespace binks_forum_API.Models
{
    public class PrivateDiscussion
    {
        private int _privateDiscussionId;
        private string? _userId;
        private string _title = null!;
        private DateTime _creation;
        private DateTime _lastMessageAt;

        public User? User { get; set; }

        private PrivateDiscussion() {}

        public PrivateDiscussion
        (
            string userId,
            string title,
            DateTime creation,
            DateTime lastMessageAt
        )
        {
            _userId = userId;
            _title = title;
            _creation = creation;
            _lastMessageAt = lastMessageAt;
        }

        public int PrivateDiscussionId 
        {
            get => _privateDiscussionId;
            set => _privateDiscussionId = value;
        }

        public string? UserId
        {
            get => _userId;
            set => _userId = value;
        }

        public string Title 
        {
            get => _title;
            set => _title = value;
        }

        public DateTime Creation
        {
            get => _creation;
            set => _creation = value;
        }

        public DateTime LastMessageAt
        {
            get => _lastMessageAt;
            set => _lastMessageAt = value;
        }

    }
}