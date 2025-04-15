namespace binks_forum_API.Helpers.CustomExceptions
{
    public class TopicMessagesNotFoundException : Exception
    {
        public TopicMessagesNotFoundException(string message) : base(message) {}

        public TopicMessagesNotFoundException() {}
    }
}