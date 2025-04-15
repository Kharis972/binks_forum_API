using binks_forum_API.DTOs.Topic;
using binks_forum_API.Models;

namespace binks_forum_API.Service.Interfaces
{
    public interface ITopicService : IService<Topic, int>
    {
        Task<Topic> AddNewTopicAsync(string userId, NewTopic newTopic);
        Task<Topic> EditTopicAsync(string userId, int topicId, EditTopic editTopic);
        Task<Topic> DeleteTopicAsync(string userId, int topicId);
    }
}