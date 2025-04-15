using binks_forum_API.DTOs.TopicMessages;
using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    public interface ITopicMessagesRepository : IRepository<TopicMessages, int>
    {
        Task<TopicMessages> AddNewTopicMessagesAsync(int topicId, string userId, AddNewTopicMessages newTopicMessages);
        Task<TopicMessages> EditTopicMessagesAsync(string userId, int topicId, EditTopicMessages editTopicMessages, int topicMessagesId);
        Task DeleteTopicMessagesAsync(string userId, int topicId, int topicMessagesId);
    }
}