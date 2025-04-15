#pragma warning disable
using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.User
{
    public class DeleteRequest
    {
        [Required(ErrorMessage = "L'identifiant est obligatoire.")]
        [StringLength(36, ErrorMessage = "L'identifiant doit être un GUID valide.")]
        [RegularExpression(@"^[a-fA-F0-9\-]{36}$", ErrorMessage = "L'identifiant doit être au format GUID.")]
        public string Id { get; set; }

    }
}