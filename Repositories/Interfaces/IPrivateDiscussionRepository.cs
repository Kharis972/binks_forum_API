using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    public interface IPrivateDiscussionRepository : IRepository<PrivateDiscussion, int>
    {
        Task<PrivateDiscussion> AddNewPrivateDiscussionAsync(string userId, string title);
    }
}