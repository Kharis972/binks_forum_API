namespace binks_forum_API.Helpers.CustomExceptions
{
    public class AnswerInMessageNotFoundException : Exception
    {
        public AnswerInMessageNotFoundException(string message) : base(message) {}
        public AnswerInMessageNotFoundException() {}
    }
}