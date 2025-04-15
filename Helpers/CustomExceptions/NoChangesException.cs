namespace binks_forum_API.Helpers.CustomExceptions
{
    public class NoChangesException : Exception
    {
        public NoChangesException(string message) : base(message) {}

        public NoChangesException() {}
    }
}