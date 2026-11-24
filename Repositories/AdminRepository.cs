using binks_forum_API.Data;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using binks_forum_API.Helpers.CustomExceptions;


namespace binks_forum_API.Repositories
{
    public class AdminRepository : Repository<Admin, string>, IAdminRepository
    {
        public AdminRepository(ApplicationDataBaseContext context) : base(context) {}

        public async Task<Admin> GetAdminIdAsync(string adminId, string userId)
        {
            var admin = await _dbSet.FirstOrDefaultAsync(a => a.AdminId == adminId && a.Id == userId);
            return admin ?? throw new AdminNotFoundException();
        }

       
    }
}
