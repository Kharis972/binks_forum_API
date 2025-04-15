using binks_forum_API.Data;
using binks_forum_API.DTOs.Topic;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service.Interfaces;

namespace binks_forum_API.Service
{
    public class TopicService : Service<Topic, int>, ITopicService
    {
        private readonly ApplicationDataBaseContext _context;
        private readonly ITopicRepository _topicRepository;
        public TopicService(ITopicRepository topicRepository, ApplicationDataBaseContext context) : base(topicRepository)
        {
            _topicRepository = topicRepository;
            _context = context;
        }

        public async Task<Topic> AddNewTopicAsync(string userId, NewTopic newTopic)
        {
            try
            { 
                return await _topicRepository.AddNewTopicAsync(userId, newTopic);
            }

            catch (DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(TopicAlreadyExistsException)
            {
                throw new TopicAlreadyExistsException();
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch(UnauthorizedException)
            {
                throw new UnauthorizedException();
            }
            catch
            {
                throw new UserNotFoundException();
            }
        }

        public async Task<Topic> EditTopicAsync(string userId, int topicId, EditTopic editTopic)
        {
            try
            {
                return await _topicRepository.EditTopicAsync(userId, topicId, editTopic);
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(TopicNotFoundException)
            {
                throw new TopicNotFoundException();
            }
            catch(DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch(UnauthorizedException)
            {
                throw new UnauthorizedException();
            }
            catch(UserNotFoundException)
            {
                throw new UserNotFoundException();
            }
        }

        public async Task<Topic> DeleteTopicAsync(string userId, int topicId, DeleteTopic deleteTopic)
        {
            try 
            {
                return await _topicRepository.DeleteTopicAsync(userId, topicId, deleteTopic);
            }
            catch(DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(UnauthorizedException)
            {
                throw new UnauthorizedException();
            }
            catch(TopicNotFoundException)
            {
                throw new TopicNotFoundException();
            }
            catch(UserNotFoundException)
            {
                throw new UserNotFoundException();
            }
        }

    }
}