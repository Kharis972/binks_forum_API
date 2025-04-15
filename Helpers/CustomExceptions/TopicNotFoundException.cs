namespace binks_forum_API.Helpers.CustomExceptions
{
    public class TopicNotFoundException : Exception
    {
        public TopicNotFoundException(string message) : base(message) {}
        public TopicNotFoundException() {}
    }
}