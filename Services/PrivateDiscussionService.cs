using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service;
using binks_forum_API.Services.Interfaces;

namespace binks_forum_API.Services
{
    public class PrivateDiscussionService : Service<PrivateDiscussion, int>, IPrivateDiscussionService
    {
        private readonly IPrivateDiscussionRepository _privateDiscussiongeRepository;
        public PrivateDiscussionService(IPrivateDiscussionRepository privateDiscussinRepository) : base(privateDiscussinRepository)
        {
            _privateDiscussiongeRepository = privateDiscussinRepository;
        }

        public async Task<PrivateDiscussion> AddNewPrivateDiscussionAsync(string userId, string title)
        {
            try
            {
                return await _privateDiscussiongeRepository.AddNewPrivateDiscussionAsync(userId, title);
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
    }
}