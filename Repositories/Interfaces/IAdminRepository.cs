using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    public interface IAdminRepository : IRepository<Admin, string>
    {
        Task<Admin> GetAdminIdAsync(string adminId, string userId);
        
    }
}