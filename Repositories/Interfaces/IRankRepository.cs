using binks_forum_API.DTOs.Rank;
using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    public interface IRankRepository : IRepository<Rank, int>
    {
        Task<Rank> AddNewRankAsync(string userId, NewRank newRank);
        Task<Rank> EditRankAsync(string userId, EditRank editRank, int rankId);
        Task DeleteRankAsync(int rankId, string userId);
    }
}