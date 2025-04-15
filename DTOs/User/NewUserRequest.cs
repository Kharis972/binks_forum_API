#pragma warning disable
using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.User
{
    public class NewUserRequest
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

        [Required(ErrorMessage = "Le mot de passe obligatoire.")]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "Le mot de passe doit être compris entre 12 et 100 caractères")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?:{}|<>])[A-Za-z\d!@#$%^&*(),.?:{}|<>]{8,}$", 
        ErrorMessage = "Le mot de passe doit contenir au moins 12 caractères, dont une majuscule, une minuscule, un chiffre et un caractère spécial.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le pays est obligatoire")]
        [StringLength(58, MinimumLength = 4, ErrorMessage = "le pays doit être compris entre 4 et 58 caractères")]
        [RegularExpression("^[A-Za-zÀ-ÖØ-öø-ÿ\\s\\-]{4,58}$", ErrorMessage = "le pays doit être compris entre 4 et 58 caractères")]
        public string Country { get; set; }

        [Required(ErrorMessage = "L'adresse e-mail est obligatoire.")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "L'adresse email doit être comprise entre 10 et 150 caractères")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "L'adresse email doit contenir un '@' et un '.'")]
        [EmailAddress(ErrorMessage = "L'adresse email doit contenir un '@' et un '.'")]
        public string Mail { get; set; }
        
        [Required(ErrorMessage = "La date de création est obligatoire.")]
        [DataType(DataType.Date, ErrorMessage = "La date doit être valide.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "La date doit être au format AAAA-MM-JJ.")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "l'age doit être compris entre 0 et 120")]
        [Range(0, 120, ErrorMessage = "L'âge doit être compris entre 0 et 120.")]
        [RegularExpression("^(0|[1-9][0-9]?|1[01][0-9]|120)$", ErrorMessage = "l'age contient uniquement des chiffres de 0 à 9 et des nombres de 10 à 120")]
        public int Age { get; set; }


        
        }
    }

