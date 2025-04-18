using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.Activities
{
    public class NewActivity
    {
        [Required(ErrorMessage = "Le nom est requis.")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Le nom doit contenir uniquement des lettres et des espaces.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La description est requise.")]
        [RegularExpression("^[a-zA-Z0-9.,;:!?\\s]+$", ErrorMessage = "La description doit contenir uniquement des caractères alphanumériques, des espaces et des ponctuations comme .,;:!?")]
        public string Description { get; set; }

        [Required(ErrorMessage = "La date de création est obligatoire.")]
        [DataType(DataType.Date, ErrorMessage = "La date doit être valide.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "La date doit être au format AAAA-MM-JJ.")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "La date de programmation est obligatoire.")]
        [DataType(DataType.Date, ErrorMessage = "La date doit être valide.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "La date doit être au format AAAA-MM-JJ.")]
        public DateTime ScheduledDate { get; set; }

        [Required(ErrorMessage = "La date de fin est obligatoire.")]
        [DataType(DataType.Date, ErrorMessage = "La date doit être valide.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "La date doit être au format AAAA-MM-JJ.")]
        public DateTime EndingDate { get; set; }
    }
}