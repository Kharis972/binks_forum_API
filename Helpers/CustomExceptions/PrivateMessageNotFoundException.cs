namespace binks_forum_API.Helpers.CustomExceptions
{
    public class PrivateMessageNotFoundException : Exception
    {
        public PrivateMessageNotFoundException(string message) : base(message) {}
        public PrivateMessageNotFoundException() {}
    }
}