using binks_forum_API.DTOs.News;
using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    public interface INewsRepository : IRepository<News, int>
    {
        Task<News> AddNewsAsync(AddNews addNews, string userId, string role);
        Task<News> EditNewsAsync(EditNews editNews, string userId, string role, int newsId);
        Task DeleteNewsAsync(int newsId, string role, string userId);
    }
}