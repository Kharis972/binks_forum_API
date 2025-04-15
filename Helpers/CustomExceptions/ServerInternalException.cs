namespace binks_forum_API.Helpers.CustomExceptions
{
    public class ServerInternalException : Exception
    {
        public ServerInternalException(string message) : base(message) {}
        public ServerInternalException() {}
    }
}