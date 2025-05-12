using binks_forum_API.Data;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Services.Interfaces;

namespace binks_forum_API.Repositories
{
    public class PrivateDiscussionRepository : Repository<PrivateDiscussion, int>
    {
        public PrivateDiscussionRepository(ApplicationDataBaseContext context) : base(context) {}

        public async Task<PrivateDiscussion> AddNewPrivateDiscussionAsync(string userId, string title)
        {
            User? user = await _context.Users.FindAsync(userId);
            try
            {
                if(user != null)
                {
                    PrivateDiscussion privateDiscussion = new PrivateDiscussion
                    (
                        userId,
                        title,
                        DateTime.Now,
                        DateTime.Now
                    );
                    try
                    {
                        await _dbSet.AddAsync(privateDiscussion);
                        await _context.SaveChangesAsync();
                        return privateDiscussion;
                    }
                    catch (DatabaseUpdateException)
                    {
                        throw new DatabaseUpdateException();
                    }
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }
    }
}