namespace binks_forum_API.Helpers.CustomExceptions
{
    public class ActivityNotFoundException : Exception
    {
        public ActivityNotFoundException(string message) : base(message) {}
        public ActivityNotFoundException() {}
    }
}