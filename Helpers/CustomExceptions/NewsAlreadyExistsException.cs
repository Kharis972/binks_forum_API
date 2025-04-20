namespace binks_forum_API.Helpers.CustomExceptions
{
    public class NewsAlreadyExistsException : Exception
    {
        public NewsAlreadyExistsException(string message) : base(message) {}
        public NewsAlreadyExistsException() {}
    }
}