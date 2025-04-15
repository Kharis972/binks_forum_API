namespace binks_forum_API.Helpers.CustomExceptions
{
    public class IdentifiersMissMatchException : Exception
    {
        public IdentifiersMissMatchException(string message) : base(message) {}

        public IdentifiersMissMatchException() {}
    }
}