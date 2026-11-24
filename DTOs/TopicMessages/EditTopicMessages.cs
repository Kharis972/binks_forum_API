using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.TopicMessages
{
    public class EditTopicMessages
    {
        [Required(ErrorMessage = "Le contenu du corps est requis.")]
        [StringLength(3000, MinimumLength = 2)]
        public required string Content { get; set; }

    }
}
