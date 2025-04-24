using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    public interface INewsTopicRepository : IRepository<NewsTopics, int>
    {
        Task<NewsTopics> AddNewNewsTopicAsync(string userId, string role, int newsId, int topicId);
        Task DeleteNewsTopicAsync(int newsTopicId, string role, string userId);
    }
}