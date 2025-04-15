using binks_forum_API.Data;
using binks_forum_API.Models;


namespace binks_forum_API.Repositories
{
    public class AdminRepository : Repository<Admin, string>
    {
        public AdminRepository(ApplicationDataBaseContext context) : base(context) {}

       
    }
}