namespace binks_forum_API.Helpers.CustomExceptions
{
    public class TopicMessagesAlreadyExistsException : Exception
    {
        public TopicMessagesAlreadyExistsException(string message) : base(message) {}
        public TopicMessagesAlreadyExistsException() {}
    }
}