using binks_forum_API.DTOs.TopicMessages;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service.Interfaces;

namespace binks_forum_API.Service
{
    public class TopicMessagesService : Service<TopicMessages, int>, ITopicMessagesService, IService<Topic, int>
    {
        private readonly ITopicMessagesRepository _topicMessagesRepository;
        public TopicMessagesService(ITopicMessagesRepository topicMessagesRepository) : base(topicMessagesRepository)
        {
            _topicMessagesRepository = topicMessagesRepository;
        }

        public async Task<TopicMessages> AddNewTopicMessagesAsync(int topicId, string userId, AddNewTopicMessages newTopicMessages)
        {
            try
            {
                return await _topicMessagesRepository.AddNewTopicMessagesAsync(topicId, userId, newTopicMessages);
            }
            catch (DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch
            {
                throw new UserNotFoundException();
            }
        }
        public async Task<TopicMessages> EditTopicMessagesAsync(string userId, int topicId, EditTopicMessages editTopicMessages, int topicMessagesId)
        {
            try
            {
                return await _topicMessagesRepository.EditTopicMessagesAsync(userId, topicId, editTopicMessages, topicMessagesId);
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch(UserNotFoundException)
            {
                throw new UserNotFoundException();
            }
        }
        public async Task DeleteTopicMessagesAsync(string userId, int topicId, int topicMessagesId)
        {
            try
            {
                await _topicMessagesRepository.DeleteTopicMessagesAsync(userId, topicId, topicMessagesId);
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch(UserNotFoundException)
            {
                throw new UserNotFoundException();
            }
            catch(DeletedTopicMessagesException)
            {
                throw new DeletedTopicMessagesException();
            }
        }
    }
}