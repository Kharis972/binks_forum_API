using System.Text.Json.Serialization;

namespace binks_forum_API.Models
{
    public class TopicMessages 
    {
        private int _id;
        private int _topicId;
        private string _userId = null!;
        private DateTime _date;
        private int _score;
        private string _content = null!;

        public Topic? Topic { get; set; }

        [JsonIgnore]
        public User User { get; private set; } = null!;

        public TopicMessages() {}

        public TopicMessages
        (
            int topicId,
            string userId,
            DateTime date,
            int score,
            string content
        )
        {
            _topicId = topicId;
            _userId = userId;
            _date = date;
            _score = score;
            _content = content;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int TopicId
        {
            get => _topicId;
            set => _topicId = value;
        }

        public string UserId
        {
            get => _userId;
            set => _userId = value;
        }
        
        public DateTime Date
        {
            get => _date;
        }

        public int Score
        {
            get => _score;
            set => _score = value;
        }

        public string Content
        {
            get => _content;
            set =>  _content = value;
        }
    }
}