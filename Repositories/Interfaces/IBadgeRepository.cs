using binks_forum_API.DTOs.Badge;
using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    public interface IBadgeRepository : IRepository<Badge, int>
    {
        Task<Badge> AddNewBadgeAsync(NewBadge newBadge, string userId);
        Task<Badge> EditBadgeAsync(EditBadge editBadge, string userId, int badgeId);
        Task DeleteBadgeAsync(string userId, int badgeId);
    }
}