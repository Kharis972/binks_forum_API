using binks_forum_API.DTOs.NewsTopics;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service;

namespace binks_forum_API.Services
{
    public class NewsTopicService : Service<NewsTopics, int>
    {
        private readonly INewsTopicRepository _newsTopicRepository;

        public NewsTopicService(INewsTopicRepository newsTopicRepository) : base(newsTopicRepository)
        {
            _newsTopicRepository = newsTopicRepository;
        }
        public async Task<NewsTopics> AddNewNewsTopicAsync(string userId, string role, int newsId, int topicId)
        {
            try
            {
                return await _newsTopicRepository.AddNewNewsTopicAsync(userId, role, newsId, topicId);
            }
            catch(ForbiddenException)
            {
                throw new ForbiddenException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(NewsAlreadyExistsException)
            {
                throw new NewsAlreadyExistsException();
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }

        public async Task DeleteNewsTopicAsync(int newsTopicId, string role, string userId)
        {
            try
            {
                await _newsTopicRepository.DeleteNewsTopicAsync(newsTopicId, role, userId);
            }
            catch(ForbiddenException)
            {
                throw new ForbiddenException();
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }
    }
}