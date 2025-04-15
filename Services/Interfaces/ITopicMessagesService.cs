using binks_forum_API.DTOs.TopicMessages;
using binks_forum_API.Models;

namespace binks_forum_API.Service.Interfaces
{
    public interface ITopicMessagesService : IService<Topic, int>
    {
        Task<TopicMessages> AddNewTopicMessagesAsync(int topicId, string userId, AddNewTopicMessages newTopicMessages);
        Task<TopicMessages> EditTopicMessagesAsync(string userId, int topicId, EditTopicMessages editTopicMessages, int topicMessagesId);
        Task DeleteTopicAsync(string userId, int topicId, DeleteTopicMessages deleteTopicMessages);
    }
}