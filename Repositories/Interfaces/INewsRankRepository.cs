using binks_forum_API.DTOs.NewsRank;
using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    public interface INewsRankRepository : IRepository<NewsRank, int>
    {
        Task<NewsRank> AddNewsRankAsync(AddNewsRank addNewsRank, int rankId, string userId, int newsId);
        Task DeleteNewsRankAsync(int newsRankId, string role, string userId);
    }
}