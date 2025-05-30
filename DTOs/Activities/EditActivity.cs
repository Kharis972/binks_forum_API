using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.Activities
{
    public class EditActivity
    {
        [Required(ErrorMessage = "Le nom est requis.")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Le nom doit contenir uniquement des lettres et des espaces.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "La description est requise.")]
        [RegularExpression("^[a-zA-Z0-9.,;:!?\\s]+$", ErrorMessage = "La description doit contenir uniquement des caractères alphanumériques, des espaces et des ponctuations comme .,;:!?")]
        public required string Description { get; set; }

        public DateTime ScheduledDate { get; set; }
        public DateTime EndingDate { get; set; }

        [Required(ErrorMessage = "Le type d'activité est requis.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Le type d'activité doit contenir uniquement des lettres.")]
        public required string Activity_type { get; set; }

        [Required(ErrorMessage = "Le statut de mise en avant est requis.")]
        public bool Is_featured { get; set; }
    }
}