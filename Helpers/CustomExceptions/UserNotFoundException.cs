namespace binks_forum_API.Helpers.CustomExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message) {}
        public UserNotFoundException() {}
    }
}