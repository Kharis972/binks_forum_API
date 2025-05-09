namespace binks_forum_API.Models
{
    public class NewsTopics : News
    {
        // Relation avec News
        public News News { get; set; } = null!;
        // Relation avec Topic
        public Topic Topic { get; set; } = null!;

        private int _newsTopicId;
        private int _newsId;
        private int _topicId;

        private NewsTopics() {}

        public NewsTopics
        (
            int newsId,
            int topicId
        )
        {
            _newsId = newsId;
            _topicId = topicId;
        }

        public int NewsTopicId
        {
            get => _newsTopicId;
            set => _newsTopicId = value;
        }
        public int NewsId => _newsId;
    
        public int TopicId => _topicId;
    }
}