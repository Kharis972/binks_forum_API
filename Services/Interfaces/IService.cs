using binks_forum_API.DTOs.User;
using binks_forum_API.Models;

namespace binks_forum_API.Service.Interfaces
{
    public interface IService<T, TKey> where T : class
    {
        // Méthode asynchrone pour récupérer une entité par son identifiant.
        // Retourne une entité de type T ou null si l'entité n'est pas trouvée.
        Task<T?> GetByIdAsync(TKey id);

        // Méthode asynchrone pour récupérer toutes les entités de type T.
        // Retourne une collection d'entités ou null si aucune entité n'est disponible.
        Task<IEnumerable<T>?> GetAllAsync();
    }
}