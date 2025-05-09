using binks_forum_API.Models;
using binks_forum_API.Service.Interfaces;

namespace binks_forum_API.Services.Interfaces
{
    public interface INewsTopicService : IService<NewsTopics, int>
    {
        Task<NewsTopics> AddNewNewsTopicAsync(string userId, string role, int newsId, int topicId);
        Task DeleteNewsTopicAsync(int newsId, string role, string userId);
    }
}