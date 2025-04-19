using binks_forum_API.DTOs.Activities;
using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    public interface IActivityRepository : IRepository<Activity, int>
    {
        Task<Activity> AddNewActivityAsync(string userId, NewActivity newActivity, string role);
        Task<Activity> EditActivityAsync(int id, string userId, NewActivity newActivity, string role);
        Task<bool> DeleteActivityAsync(int id, string userId, string role);
        Task<Activity> GetActivityByIdAsync(int id);
        Task<List<Activity>> GetActivitiesByUserIdAsync(string userId);
    }
}