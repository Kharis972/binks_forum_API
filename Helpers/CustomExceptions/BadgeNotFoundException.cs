namespace binks_forum_API.Helpers.CustomExceptions
{
    public class BadgeNotFoundException : Exception
    {
        public BadgeNotFoundException(string message) : base(message) {}
        public BadgeNotFoundException() {} 
    }
}