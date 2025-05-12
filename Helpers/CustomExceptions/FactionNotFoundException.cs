namespace binks_forum_API.Helpers.CustomExceptions
{
    public class FactionNotFoundException : Exception
    {
        public FactionNotFoundException(string message) : base(message) {}
        public FactionNotFoundException() {}
    }
}