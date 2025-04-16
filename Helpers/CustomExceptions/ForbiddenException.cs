namespace binks_forum_API.Helpers.CustomExceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message) {}
        public ForbiddenException() {}
    }
}