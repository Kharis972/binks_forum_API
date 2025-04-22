using binks_forum_API.DTOs.NewsTopics;
using binks_forum_API.Models;
using binks_forum_API.Service.Interfaces;

namespace binks_forum_API.Services.Interfaces
{
    public interface INewsTopicService : IService<NewsTopics, int>
    {
        Task<NewsTopics> AddNewTopicAsync(AddNewsTopic addNewsTopic, string userId, string role);
        Task<News> EditNewsTopicAsync(EditNewsTopic editNewsTopic, string userId, string role, int newsId);
        Task DeleteNewsTopicAsync(int newsId, string role, string userId);
    }
}