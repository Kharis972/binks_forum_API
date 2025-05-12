using binks_forum_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using binks_forum_API.Data;

namespace binks_forum_API.Repositories
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : class
    {
        protected readonly ApplicationDataBaseContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(ApplicationDataBaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(TKey id)
        {
           try 
           {
               return await _dbSet.FindAsync(id);
           } 
           catch (Exception e) 
           {
               Console.WriteLine(e);
               return null;
           }
        }

        public async Task<List<T>?> GetAllAsync()
        {
            try 
            {
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