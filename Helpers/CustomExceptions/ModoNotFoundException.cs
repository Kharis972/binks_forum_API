namespace binks_forum_API.Helpers.CustomExceptions
{
    public class ModoNotFoundException : Exception
    {
        public ModoNotFoundException(string message) : base(message) {}
        public ModoNotFoundException() {}
    }
}