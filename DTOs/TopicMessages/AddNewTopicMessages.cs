using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.TopicMessages
{
    public class AddNewTopicMessages
    {
        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Les noms doivent avoir un minimum de 2, et un maximum de 50 caractères.")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Le nom doit contenir uniquement des lettres et des espaces.")]
        public string Name { get; set; }

        [StringLength(300, MinimumLength = 10, ErrorMessage = "Les sujets doivent avoir un minimum de 10, et un maximum de 300 caractères.")]
        [RegularExpression("^[a-zA-Z0-9,\\s]+$", ErrorMessage = "Les sujets doivent contenir uniquement des caractères alphanumériques, des espaces et des virgules.")]
        public string Subjects { get; set; }

        [Required(ErrorMessage = "Le contenu du corps est requis.")]
        [StringLength(3000, MinimumLength = 10, ErrorMessage = "Le corps doit avoir un minimum de 10, et un maximum de 3000 caractères.")]
        [RegularExpression("^[A-Za-z0-9\\s]+$", ErrorMessage = "Le corps doit contenir uniquement des lettres, des chiffres et des espaces.")]
        public string Body { get; set; }
    }
}