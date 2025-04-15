namespace binks_forum_API.Helpers.CustomExceptions
{
    public class TopicAlreadyExistsException : Exception
    {
        public TopicAlreadyExistsException(string message) : base(message) {}
        public TopicAlreadyExistsException() {}
    }
}