namespace binks_forum_API.Helpers.CustomExceptions
{
    public class DatabaseUpdateException : Exception
    {
        public DatabaseUpdateException(string message) : base(message) {}
        public DatabaseUpdateException() {}
    }
}