using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    public interface IRankRepository : IRepository<Rank, int>
    {
        Task<List<Rank>> GetRanksByUserIdAsync(string userId);
        Task<Rank> AddNewRankAsync(Rank rank);
        Task<Rank> EditRankAsync(Rank rank);
        Task DeleteRankAsync(int id);
    }
}