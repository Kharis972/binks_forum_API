using binks_forum_API.DTOs.Rank;
using binks_forum_API.Models;
using binks_forum_API.Service.Interfaces;

namespace binks_forum_API.Services.Interfaces
{
    public interface IRankService : IService<Rank, int>
    {
        Task<Rank> AddNewRankAsync(string userId, NewRank newRank);
        Task<Rank> EditRankAsync(string userId, EditRank editRank, int rankId);
        Task DeleteRankAsync(int rankId, string userId);
    }
}