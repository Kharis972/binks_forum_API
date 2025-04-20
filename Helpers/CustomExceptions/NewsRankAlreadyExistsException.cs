namespace binks_forum_API.Helpers.CustomExceptions
{
    public class NewsRankAlreadyExistsException : Exception
    {
        public NewsRankAlreadyExistsException(string message) : base(message) {}
        public NewsRankAlreadyExistsException() {}
    }
}