using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.Topic;

public sealed class NewTopic
{
    [Required, StringLength(120, MinimumLength = 5)]
    public required string Name { get; set; }

    [Required, StringLength(2000, MinimumLength = 10)]
    public required string Description { get; set; }

    [Required, StringLength(120)]
    public required string Subjects { get; set; }

    [StringLength(40)]
    public string TopicIcon { get; set; } = "discussion";
}
