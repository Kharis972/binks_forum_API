using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.News 
{
    public class EditNews
    {
        [Required(ErrorMessage = "Le nom est requis.")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Le nom doit contenir uniquement des lettres et des espaces.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "La description est requise.")]
        [RegularExpression("^[a-zA-Z0-9.,;:!?\\s]+$", ErrorMessage = "La description doit contenir uniquement des caractères alphanumériques, des espaces et des ponctuations comme .,;:!?")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Le contenu du corps est requis.")]
        [StringLength(3000, MinimumLength = 10, ErrorMessage = "Le corps doit avoir un minimum de 10, et un maximum de 3000 caractères.")]
        [RegularExpression("^[A-Za-z0-9\\s]+$", ErrorMessage = "Le corps doit contenir uniquement des lettres, des chiffres et des espaces.")]
        public required string Body { get; set; }

        [Required(ErrorMessage = "La date de sortie est obligatoire.")]
        [DataType(DataType.Date, ErrorMessage = "La date de sortie doit être valide.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "La date de sortie doit être au format AAAA-MM-JJ.")]
        public DateTime ReleaseDate { get; set; }
    }
}