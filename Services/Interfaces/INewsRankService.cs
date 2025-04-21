using binks_forum_API.DTOs.NewsRank;
using binks_forum_API.Models;
using binks_forum_API.Service.Interfaces;

namespace binks_forum_API.Services.Interfaces
{
    public interface INewsRankService : IService<NewsRank, int>
    {
        Task<NewsRank> AddNewsRankAsync(AddNewsRank addNewsRank, int rankId, string userId, int newsId);
        Task DeleteNewsRankAsync(int newsRankId, string userId);
    }
}