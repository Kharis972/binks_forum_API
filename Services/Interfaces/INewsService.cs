using binks_forum_API.DTOs.News;
using binks_forum_API.Models;
using binks_forum_API.Service.Interfaces;

namespace binks_forum_API.Services.Interfaces
{
    public interface INewsService : IService<News, int>
    {
        Task<News> AddNewsAsync(AddNews addNews, string userId, string role);
        Task<News> EditNewsAsync(EditNews editNews, string userId, string role, int newsId);
        Task DeleteNewsAsync(int newsId, string role, string userId);
    }
}