namespace binks_forum_API.Helpers.CustomExceptions
{
    public class NewsNotFoundException : Exception
    {
        public NewsNotFoundException(string message) : base(message) {}
        public NewsNotFoundException() {}
    }
}