using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.TopicMessages;

public sealed class AddNewTopicMessages
{
    [Required, StringLength(3000, MinimumLength = 2)]
    public required string Body { get; set; }
}
