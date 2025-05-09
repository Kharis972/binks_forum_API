namespace binks_forum_API.Models
{
    public class AnswerInMessage
    {
        private int _answerInMessageId;
        private int _answerId;
        private int _answeredMessageId;

        private AnswerInMessage() {}

        public AnswerInMessage
        (
            int answerId,
            int answeredMessageId
        )
        {
            _answerId = answerId;
            _answeredMessageId = answeredMessageId;
        }

        public int AnswerInMessageId
        {
            get => _answerInMessageId;
            set => _answerInMessageId = value;
        }
        public int AnswerId
        {
            get => _answerId;
            set => _answerId = value;
        }
        public int AnsweredMessageId
        {
            get => _answeredMessageId;
            set => _answeredMessageId = value;
        }
    }
}