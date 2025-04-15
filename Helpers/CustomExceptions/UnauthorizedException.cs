namespace binks_forum_API.Helpers.CustomExceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) {}
        public UnauthorizedException() {}
    }
}