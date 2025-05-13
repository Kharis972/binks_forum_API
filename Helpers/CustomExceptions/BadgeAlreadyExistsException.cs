namespace binks_forum_API.Helpers.CustomExceptions
{
    public class BadgeAlreadyExistsException : Exception
    {
        public BadgeAlreadyExistsException(string message) : base(message) {}
        public BadgeAlreadyExistsException() {}
    }
}