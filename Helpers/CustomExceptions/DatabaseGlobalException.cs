namespace binks_forum_API.Helpers.CustomExceptions
{
    public class DatabaseGlobalException : Exception
    {
        public DatabaseGlobalException(string message) : base(message) {}
        public DatabaseGlobalException() {}
    }
}