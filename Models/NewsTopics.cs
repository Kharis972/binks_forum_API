namespace binks_forum_API.Models
{
    public class NewsTopics : News
    {
        // Relation avec News
        public int NewsId { get; set; }
        public News News { get; set; }
        // Relation avec Topic
        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        private int _id;

        private NewsTopics() {}

        public NewsTopics
        (
            int id
        )
        {
            _id = id;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        
    }
}