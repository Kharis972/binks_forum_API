namespace binks_forum_API.Helpers.CustomExceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message) : base(message) {}
        public UserAlreadyExistsException() {}
    }
}