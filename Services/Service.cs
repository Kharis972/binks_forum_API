using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service.Interfaces;

namespace binks_forum_API.Service
{
    public class Service<T, TKey> : IService<T, TKey> where T : class
    {
        private readonly IRepository<T, TKey> _repository;

        public Service(IRepository<T, TKey> repository)
        {
            // Initialisation de _repository avec l'instance injectée.
            _repository = repository;
        }

        //Méthode asyncrone pour récupérer une entity par son id via le dépôt.
        public async Task<T?> GetByIdAsync(TKey id)
        {
            try
            {
                //Appelle le dépôt pour récupérer l'entity par son id.
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                // Affiche l'exception dans la console en cas d'erreur.
                Console.WriteLine(e);

                // Retourne null en cas de problème.
                return null;
            }
        }

        // Méthode asynchrone pour récupérer toutes les entités via le dépôt.
        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            try
            {
                //Appelle le dépôt pour récupérer toutes les entity.
                return await _repository.GetAllAsync();
            }
            catch (Exception e)
            {
                // Affiche l'exception dans la console en cas d'erreur.
                Console.WriteLine(e);

                // Retourne null en cas de problème.
                return null;
            }
        }
    }
}