namespace binks_forum_API.Services
{
    using binks_forum_API.DTOs.Activities;
    using binks_forum_API.Helpers.CustomExceptions;
    using binks_forum_API.Models;
    using binks_forum_API.Repositories.Interfaces;
    using binks_forum_API.Service;
    using binks_forum_API.Service.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ActivityService : Service<Activity, int>, IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository) : base(activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<Activity> AddNewActivityAsync(string userId, NewActivity newActivity, string role)
        {
            try
            {
                return await _activityRepository.AddNewActivityAsync(userId, newActivity, role);
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
    
        public async Task<Activity> EditActivityAsync(int id, string userId, NewActivity newActivity, string role)
        {
            try{
                return await _activityRepository.EditActivityAsync(id, userId, newActivity, role);
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch (ActivityNotFoundException)
            {
                throw new ActivityNotFoundException();
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

        public async Task<bool> DeleteActivityAsync(int id, string userId, string role)
        {
            try
            {
                return await _activityRepository.DeleteActivityAsync(id, userId, role);
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch (ActivityNotFoundException)
            {
                throw new ActivityNotFoundException();
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

        public async Task<Activity> GetActivityByIdAsync(int id)
        {
            try
            {
                return await _activityRepository.GetActivityByIdAsync(id);
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch (ActivityNotFoundException)
            {
                throw new ActivityNotFoundException();
            }
        }
        public async Task<List<Activity>> GetActivitiesByUserIdAsync(string userId)
        {
            try
            {
                return await _activityRepository.GetActivitiesByUserIdAsync(userId);
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch (ActivityNotFoundException)
            {
                throw new ActivityNotFoundException();
            }
            catch (UserNotFoundException)
            {
                throw new UserNotFoundException();
            }
            catch (DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }

        }   
    }
}
