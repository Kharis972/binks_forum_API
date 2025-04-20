using binks_forum_API.DTOs.NewsRank;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service;
using binks_forum_API.Services.Interfaces;

namespace binks_forum_API.Services
{
    public class NewsRankService : Service<NewsRank, int>, INewsRankService
    {
        private readonly INewsRankRepository _newsRankRepository;

        public NewsRankService(INewsRankRepository newsRankRepository) : base(newsRankRepository)
        {
            _newsRankRepository = newsRankRepository;
        }
        public async Task<NewsRank> AddNewsRankAsync(AddNewsRank addNewsRank, int rankId, string userId, int newsId)
        {
            try
            {
                return await _newsRankRepository.AddNewsRankAsync(addNewsRank,rankId, userId, newsId);
            }
            catch(ForbiddenException)
            {
                throw new ForbiddenException();
            }
            catch(RankNotFoundException)
            {
                throw new RankNotFoundException();
            }
            catch(NewsRankAlreadyExistsException)
            {
                throw new NewsRankAlreadyExistsException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }
        
        public async Task DeleteNewsRankAsync(int newsRankId)
        {
            try
            {
                await _newsRankRepository.DeleteNewsRankAsync(newsRankId);
            }
            catch(RankNotFoundException)
            {
                throw new RankNotFoundException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }

        }
    }
}