using binks_forum_API.DTOs.News;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service;
using binks_forum_API.Services.Interfaces;

namespace binks_forum_API.Services
{
    public class NewsService : Service<News, int>, INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository) : base(newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<News> AddNewsAsync(AddNews addNews, string userId, string role)
        {
            try
            {
                return await _newsRepository.AddNewsAsync(addNews, userId, role);
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

        public async Task<News> EditNewsAsync(EditNews editNews, string userId, string role, int newsId)
        {
            try
            {
                return await _newsRepository.EditNewsAsync(editNews, userId, role, newsId);
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

        public async Task DeleteNewsAsync(int newsId, string role, string userId)
        {
            try
            {
                await _newsRepository.DeleteNewsAsync(newsId, role, userId);
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