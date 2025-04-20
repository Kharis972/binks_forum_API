using binks_forum_API.DTOs.Rank;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service;
using binks_forum_API.Services.Interfaces;


namespace binks_forum_API.Services
{
    public class RankService : Service<Rank, int>, IRankService
    {
        private readonly IRankRepository _rankRepository;

        public RankService(IRankRepository rankRepository) : base(rankRepository)
        {
            _rankRepository = rankRepository;
        }

        public async Task<Rank> AddNewRankAsync(string userId, NewRank newRank)
        {
            try
            {
                return await _rankRepository.AddNewRankAsync(userId, newRank);
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

        public async Task<Rank> EditRankAsync(string userId, EditRank editRank, int rankId)
        {
            try
            {
                return await _rankRepository.EditRankAsync(userId, editRank, rankId);
            }
            catch(RankAlreadyExistsException)
            {
                throw new RankAlreadyExistsException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(NoChangesException)
            {
                throw new NoChangesException();
            }
            catch(DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
        }

        public async Task DeleteRankAsync(int rankId)
        {
            try
            {
                await _rankRepository.DeleteRankAsync(rankId);
            }
            catch(RankNotFoundException)
            {
                throw new RankNotFoundException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }


        }
    }
}