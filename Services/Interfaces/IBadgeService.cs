using binks_forum_API.DTOs.Badge;
using binks_forum_API.Models;
using binks_forum_API.Service.Interfaces;

namespace binks_forum_API.Services.Interfaces
{
    public interface IBadgeService : IService<Badge, int>
    {
        Task<Badge> AddNewBadgeAsync(NewBadge newBadge, string userId);
        Task<Badge> EditBadgeAsync(EditBadge editBadge, string userId, int badgeId);
        Task DeleteBadgeAsync(string userId, int badgeId);
    }
}