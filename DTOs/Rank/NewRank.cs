using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.Rank
{
    public class NewRank
    {
        
        [Required(ErrorMessage = "Le nom est requis.")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Le nom doit contenir uniquement des lettres et des espaces.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "La description est requise.")]
        [RegularExpression("^[a-zA-Z0-9.,;:!?\\s]+$", ErrorMessage = "La description doit contenir uniquement des caractères alphanumériques, des espaces et des ponctuations comme .,;:!?")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "L'XP minimum est requise.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "L'XP minimum doit contenir uniquement des chiffres.")]
        public int MinXp { get; set; }

        [Required(ErrorMessage = "L'icône du rank est requise.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "L'icône du rank doit contenir uniquement des caractères alphanumériques.")]
        public required string RankIcon { get; set; }
    }
}