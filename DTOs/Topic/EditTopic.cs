using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.Topic
{
    public class EditTopic
    {
        [Required(ErrorMessage = "La description est requise.")]
        [RegularExpression("^[a-zA-Z0-9.,;:!?\\s]+$", ErrorMessage = "La description doit contenir uniquement des caractères alphanumériques, des espaces et des ponctuations comme .,;:!?")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Le nom est requis.")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Le nom doit contenir uniquement des lettres et des espaces.")]
        public string Name { get; set;}

        [Required(ErrorMessage = "Les sujets sont requis.")]
        [RegularExpression("^[a-zA-Z0-9,\\s]+$", ErrorMessage = "Les sujets doivent contenir uniquement des caractères alphanumériques, des espaces et des virgules.")]
        public string Subjects { get; set; }

        [Required(ErrorMessage = "L'icône du sujet est requise.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "L'icône du sujet doit contenir uniquement des caractères alphanumériques.")]
        public string TopicIcon { get; set; }
    } 
}