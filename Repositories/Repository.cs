// Importation de l'interface générique IRepository.
using binks_forum_API.Repositories.Interfaces;

// Importation de Entity Framework Core pour interagir avec la base de données.
using Microsoft.EntityFrameworkCore;

// Importation du contexte de base de données spécifique à votre application.
using binks_forum_API.Data;
using binks_forum_API.DTOs.User;
using binks_forum_API.Models;
using binks_forum_API.Helpers.CustomExceptions;

// Déclaration de l'espace de noms pour organiser les dépôts.
namespace binks_forum_API.Repositories
{
    // Déclaration de la classe Repository qui implémente l'interface générique IRepository.
    // Elle est générique pour s'adapter à n'importe quelle entité (T) et type de clé (TKey).
    public class Repository<T, TKey> : IRepository<T, TKey> where T : class
    {
        // Contexte de base de données (pour accéder et modifier les données).
        protected readonly ApplicationDataBaseContext _context;

        // Représente une table spécifique dans la base de données correspondant au type T.
        protected readonly DbSet<T> _dbSet;

        // Constructeur pour initialiser le contexte de base de données et le DbSet.
        public Repository(ApplicationDataBaseContext context)
        {
            // Assignation du contexte pour interagir avec la base de données.
            _context = context;

            // Initialisation du DbSet qui correspond à la table pour le type T.
            _dbSet = context.Set<T>();
        }

        // Méthode asynchrone pour récupérer une entité par son ID.
        public async Task<T?> GetByIdAsync(TKey id)
        {
           try 
           {
               // Recherche de l'entité par sa clé primaire (ID).
               return await _dbSet.FindAsync(id);
           } 
           catch (Exception e) 
           {
               // Affiche l'exception dans la console en cas d'erreur.
               Console.WriteLine(e);
               return null; // Retourne null en cas d'échec.
           }
        }

        // Méthode asynchrone pour récupérer toutes les entités.
        public async Task<List<T>?> GetAllAsync()
        {
            try 
            {
                // Fait une liste de la table en fonction de dbSet<T>
                return await _dbSet.ToListAsync();
            } 
            catch (Exception e) 
            {
                
                Console.WriteLine(e);
                return null; 
            }
        }      
    }
}