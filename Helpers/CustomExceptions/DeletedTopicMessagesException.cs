namespace binks_forum_API.Helpers.CustomExceptions
{
    public class DeletedTopicMessagesException : Exception
    {
        public DeletedTopicMessagesException(string message) : base(message) {}
        public DeletedTopicMessagesException() {}
    }
}