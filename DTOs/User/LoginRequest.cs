#pragma warning disable

using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.User
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "L'adresse e-mail est obligatoire.")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "L'adresse email doit être comprise entre 10 et 150 caractères")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+.[^@\s]+$", ErrorMessage = "L'adresse email doit contenir un '@' et un '.'")]
        [EmailAddress(ErrorMessage = "L'adresse email doit contenir un '@' et un '.'")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "Le mot de passe doit être compris entre 12 et 100 caractères")]
        [RegularExpression(@"^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[!@#$%^&(),.?:{}|<>])[A-Za-z\d!@#$%^&(),.?:{}|<>]{8,}$", 
        ErrorMessage = "Le mot de passe doit contenir au moins 12 caractères, dont une majuscule, une minuscule, un chiffre et un caractère spécial.")]
        public string Password { get; set; }
    }
}