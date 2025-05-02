namespace binks_forum_API.Models
{
    public class PrivateMessage
    {
        private int _privateMessageId;
        private string? _userId;
        private string? _messageContent;
        private DateTime _sentDate;
        private bool _isRead;
        private string? _senderId;
        private string? _receiverId;
        private string? _subject;  

        public PrivateMessage() {}
        
        public PrivateMessage
        (
            string? userId, 
            string? messageContent, 
            DateTime sentDate, 
            bool isRead, 
            string? senderId, 
            string? receiverId, 
            string? subject
        )
        {
            _userId = userId;
            _messageContent = messageContent;
            _sentDate = sentDate;
            _isRead = isRead;
            _senderId = senderId;
            _receiverId = receiverId;
            _subject = subject;
        }
        
        public int PrivateMessageId
        {
            get => _privateMessageId;
            set => _privateMessageId = value;
        }
        public string? UserId
        {
            get => _userId;
            set => _userId = value;
        }
        public string? MessageContent
        {
            get => _messageContent;
            set => _messageContent = value;
        }
        public DateTime SentDate
        {
            get => _sentDate;
            set => _sentDate = value;
        }
        public bool IsRead
        {
            get => _isRead;
            set => _isRead = value;
        }
        public string? SenderId
        {
            get => _senderId;
            set => _senderId = value;
        }
        public string? ReceiverId
        {
            get => _receiverId;
            set => _receiverId = value;
        }
        public string? Subject
        {
            get => _subject;
            set => _subject = value;
        }
        
    }
}