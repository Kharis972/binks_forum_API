namespace binks_forum_API.DTOs.PrivateMessage
{
    public class NewPrivateMessage
    {
        public string? UserId { get; set; }
        public string? MessageContent { get; set; }
        public DateTime SentDate { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public string? Subject { get; set; }
    }
}