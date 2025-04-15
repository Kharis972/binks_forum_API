// Déclare l'espace de noms pour organiser les interfaces des dépôts.
using binks_forum_API.DTOs.User;
using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    // Définition de l'interface générique IRepository qui peut être utilisée avec n'importe quelle classe (T) et type de clé (TKey).
    public interface IRepository<T, TKey> where T : class
    {
        // Méthode asynchrone pour récupérer une entité par son identifiant (TKey).
        // Retourne une entité de type T ou null si l'entité n'est pas trouvée.
        Task<T?> GetByIdAsync(TKey id);

        // Méthode asynchrone pour récupérer toutes les entités de type T.
        // Retourne une collection d'entités ou null si aucune entité n'est trouvée.
        Task<List<T>?> GetAllAsync();

    }
}