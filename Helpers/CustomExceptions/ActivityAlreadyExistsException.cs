namespace binks_forum_API.Helpers.CustomExceptions
{
    public class ActivityAlreadyExistsException : Exception
    {
        public ActivityAlreadyExistsException(string message) : base(message) {}
        public ActivityAlreadyExistsException() {}
    }
}