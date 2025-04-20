using binks_forum_API.DTOs.Rank;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service;

namespace binks_forum_API.Services
{
    public class RankService : Service<Rank, int>, IRankService
    {
        private readonly IRankRepository _rankRepository;

        public RankService(IRankRepository rankRepository) : base(rankRepository)
        {
            _rankRepository = rankRepository;
        }

        public async Task<Rank> AddNewRankAsync(string userId, NewRank newRank, string role)
        {
            try
            {
                return await _rankRepository.AddNewRankAsync(userId, newRank, role);
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch (UserNotFoundException)
            {
                throw new UserNotFoundException();
            }
            catch (ForbiddenException)
            {
                throw new ForbiddenException();
            }
            catch (DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
        } 
    }
}