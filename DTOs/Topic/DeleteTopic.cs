using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.Topic
{
    public class DeleteTopic
    {
        [Required(ErrorMessage = "L'identifiant du sujet est requis.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "L'identifiant du sujet doit contenir uniquement des chiffres.")]
        public int TopicId { get; set; }

        [Required(ErrorMessage = "L'identifiant de l'utilisateur est requis.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "L'identifiant de l'utilisateur doit contenir uniquement des chiffres.")]
        public int UserId { get; set; }
    }
}