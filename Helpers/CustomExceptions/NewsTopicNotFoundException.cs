namespace binks_forum_API.Helpers.CustomExceptions
{
    public class NewsTopicNotFoundException : Exception
    {
        public NewsTopicNotFoundException(string message) : base(message) {}
        public NewsTopicNotFoundException() {}
    }
}