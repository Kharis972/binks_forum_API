namespace binks_forum_API.Helpers.CustomExceptions
{
    public class AdminNotFoundException : Exception
    {
        public AdminNotFoundException(string message) : base(message) {}
        public AdminNotFoundException() {}
    }
}