#pragma warning disable
using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.User
{
    public class EditRequest
    {
        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le prénom doit être compris entre 2 et 50 caractères")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Le prénom doit contenir uniquement des lettres.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le nom doit être compris entre 2 et 50 caractères")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Le nom doit contenir uniquement des lettres.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Le pseudo doit être compris entre 2 et 50 caractères")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le pseudo doit être compris entre 2 et 50 caractères")]
        [RegularExpression(@"^[A-Za-z0-9\s!#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]{2,50}$", ErrorMessage = "Le pseudo contient des caractères invalides.")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Le pays est obligatoire")]
        [StringLength(58, MinimumLength = 4, ErrorMessage = "le pays doit être compris entre 4 et 58 caractères")]
        [RegularExpression("^[A-Za-zÀ-ÖØ-öø-ÿ\\s\\-]{4,58}$", ErrorMessage = "le pays doit être compris entre 4 et 58 caractères")]
        public string Country { get; set; }
    }
}