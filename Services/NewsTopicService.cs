using binks_forum_API.DTOs.NewsTopics;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Service;

namespace binks_forum_API.Services
{
    public class NewsTopicServices : Service<NewsTopics, int>
    {
        public async Task<NewsTopics> AddNewTopicAsync(AddNewsTopic addNewsTopic, string userId, string role)
        {
            try
            {
                return await _newsTopicRepository.AddNewsTopicAsync(addNewsTopic, userId, role);
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

        public async Task<News> EditNewsTopicAsync(EditNewsTopic editNewsTopic, string userId, string role, int newsId)
        {
            try
            {
                return await _newsTopicRepository.EditNewsTopicAsync(editNewsTopic, userId, role, newsId);
            }
            catch(ForbiddenException)
            {
                throw new ForbiddenException();
            }
            catch(NewsNotFoundException)
            {
                throw new NewsNotFoundException();
            }
            catch(NewsAlreadyExistsException)
            {
                throw new NewsAlreadyExistsException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(NoChangesException)
            {
                throw new NoChangesException();
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }

        public async Task DeleteNewsTopicAsync(int newsId, string role, string userId)
        {
            try
            {
                await _newsTopicRepository.DeleteNewsTopicAsync(newsId, role, userId);
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