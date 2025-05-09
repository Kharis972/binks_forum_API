using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.Topic
{
    public class NewTopic
    {
        [Required(ErrorMessage = "L'identifiant du sujet est requis.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "L'identifiant du sujet doit contenir uniquement des chiffres.")]
        public int TopicId { get; set; }

        [Required(ErrorMessage = "La description est requise.")]
        [RegularExpression("^[a-zA-Z0-9.,;:!?\\s]+$", ErrorMessage = "La description doit contenir uniquement des caractères alphanumériques, des espaces et des ponctuations comme .,;:!?")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "L'identifiant de l'utilisateur est requis.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "L'identifiant de l'utilisateur doit contenir uniquement des chiffres.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Le nom est requis.")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Le nom doit contenir uniquement des lettres et des espaces.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Les sujets sont requis.")]
        [RegularExpression("^[a-zA-Z0-9,\\s]+$", ErrorMessage = "Les sujets doivent contenir uniquement des caractères alphanumériques, des espaces et des virgules.")]
        public required string Subjects { get; set; }

        [Required(ErrorMessage = "Les vues sont requises.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Les vues doivent contenir uniquement des chiffres.")]
        public int Views { get; set; }

        [Required(ErrorMessage = "Les participants sont requis.")]
        [RegularExpression("^[a-zA-Z0-9,\\s]+$", ErrorMessage = "Les participants doivent contenir uniquement des caractères alphanumériques, des espaces et des virgules.")]
        public required string Participants { get; set; }

        [Required(ErrorMessage = "L'icône du sujet est requise.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "L'icône du sujet doit contenir uniquement des caractères alphanumériques.")]
        public required string TopicIcon { get; set; }
        
        [Required(ErrorMessage = "La date de création est obligatoire.")]
        [DataType(DataType.Date, ErrorMessage = "La date doit être valide.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "La date doit être au format AAAA-MM-JJ.")]
        public DateTime CreationDate { get; set; }

    }
}