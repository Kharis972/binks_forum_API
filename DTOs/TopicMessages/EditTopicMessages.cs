using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.TopicMessages
{
    public class EditTopicMessages
    {
        [Required(ErrorMessage = "Le contenu du corps est requis.")]
        [RegularExpression("^[A-Za-z0-9\\s]+$", ErrorMessage = "Le corps doit contenir uniquement des lettres, des chiffres et des espaces.")]
        public string Content { get; set; }

    }
}