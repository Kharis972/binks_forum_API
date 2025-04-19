namespace binks_forum_API.Helpers.CustomExceptions
{
    public class RankNotFoundException : Exception
    {
        public RankNotFoundException(string message) : base(message) {}
        public RankNotFoundException() {}
    }
}