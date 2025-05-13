using binks_forum_API.DTOs.Badge;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service;

namespace binks_forum_API.Services
{
    public class BadgeService : Service<Badge, int>
    {
        private readonly IBadgeRepository _badgeRepository;

        public BadgeService(IBadgeRepository badgeRepository) : base(badgeRepository)
        {
            _badgeRepository = badgeRepository;
        }

        public async Task<Badge> AddNewBadgeAsync(NewBadge newBadge, string userId)
        {
            try
            {
                return await _badgeRepository.AddNewBadgeAsync(newBadge, userId);
            }
            catch(BadgeAlreadyExistsException)
            {
                throw new BadgeAlreadyExistsException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
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

        public async Task<Badge> EditBadgeAsync(EditBadge editBadge, string userId, int badgeId)
        {
            try
            {
                return await _badgeRepository.EditBadgeAsync(editBadge, userId, badgeId);
            }
            catch(BadgeNotFoundException)
            {
                throw new BadgeNotFoundException();
            }
            catch(ForbiddenException)
            {
                throw new ForbiddenException();
            }
            catch(DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch(BadgeAlreadyExistsException)
            {
                throw new BadgeAlreadyExistsException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(NoChangesException)
            {
                throw new NoChangesException();
            }
        }

        public async Task DeleteBadgeAsync(string userId, int badgeId)
        {
            try
            {
                await _badgeRepository.DeleteBadgeAsync(userId, badgeId);
            }
            catch(ForbiddenException)
            {
                throw new ForbiddenException();
            }
            catch(BadgeNotFoundException)
            {
                throw new BadgeNotFoundException();
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