namespace binks_forum_API.Helpers.CustomExceptions
{
    public class RankAlreadyExistsException : Exception
    {
        public RankAlreadyExistsException(string message) : base(message) {}
        public RankAlreadyExistsException() {}
    }
}