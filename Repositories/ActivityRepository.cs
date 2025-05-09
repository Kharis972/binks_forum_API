using binks_forum_API.Data;
using binks_forum_API.DTOs.Activities;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using Microsoft.EntityFrameworkCore;

namespace binks_forum_API.Repositories
{
    public class ActivityRepository : Repository<Activity, int>
    {
        public ActivityRepository(ApplicationDataBaseContext context) : base(context) {}

        public async Task<Activity> AddNewActivityAsync(string userId, NewActivity newActivity, string role)
        { 
            try
            {       
                if(role == "Admin")
                {
                Admin? admin = await _context.Admins.FindAsync(userId);
                }
                else if(role == "Modo")
                {
                Modo? modo = await _context.Modos.FindAsync(userId);
                }
                else
                {
                    throw new UserNotFoundException();
                }  
                    //Création de l'activité
                    Activity activity = new Activity
                    (newActivity.Name, newActivity.Description, DateTime.Now, newActivity.ScheduledDate, newActivity.EndingDate, newActivity.Activity_type, newActivity.Is_featured, userId);
                    try
                    { 
                        await _dbSet.AddAsync(activity);
                        await _context.SaveChangesAsync();
                        return activity;
                    }
                    catch (DatabaseUpdateException)
                    {
                        throw new DatabaseUpdateException();
                    }                  
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }

        public async Task<Activity> EditActivityAsync(int id, string userId, EditActivity editActivity, string role)
        {
            try
            {
                // 1. Trouver l'activité
                Activity? activity = await _dbSet.FindAsync(id);
                if (activity == null) throw new ActivityNotFoundException();

                // 2. Valider le rôle
                if (role == "Admin")
                {
                    if (await _context.Admins.FindAsync(userId) == null)
                        throw new UnauthorizedAccessException();
                }
                else if (role == "Modo")
                {
                    if (await _context.Modos.FindAsync(userId) == null)
                        throw new UnauthorizedAccessException();
                }
                else throw new UserNotFoundException();

                if(activity.Name != editActivity.Name)
                    {
                        activity.Name = editActivity.Name;
                    }
                    if(activity.Description != editActivity.Description)
                    {
                        activity.Description = editActivity.Description;
                    }
                    if(activity.ScheduledDate != editActivity.ScheduledDate)
                    {
                        activity.ScheduledDate = editActivity.ScheduledDate;
                    }
                    if(activity.EndingDate != editActivity.EndingDate)
                    {
                        activity.EndingDate = editActivity.EndingDate;
                    }
                    if(activity.Activity_type != editActivity.Activity_type)
                    {
                        activity.Activity_type = editActivity.Activity_type;
                    }
                    if(activity.Is_featured != editActivity.Is_featured)
                    {
                        activity.Is_featured = editActivity.Is_featured;
                    }
                // 3. Mettre à jour l'activité
                activity.Name = editActivity.Name;
                activity.Description = editActivity.Description;
                activity.ScheduledDate = editActivity.ScheduledDate;
                activity.EndingDate = editActivity.EndingDate;
                activity.Activity_type = editActivity.Activity_type;
                activity.Is_featured = editActivity.Is_featured;

                await _context.SaveChangesAsync();
                return activity;
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
        }

        public async Task<Activity> DeleteActivityAsync(int id, string userId, string role)
        {
            try
            {
                // 1. Trouver l'activité
                Activity? activity = await _dbSet.FindAsync(id);
                if (activity == null) throw new ActivityNotFoundException();

                // 2. Valider le rôle
                if (role == "Admin")
                {
                    if (await _context.Admins.FindAsync(userId) == null)
                        throw new UnauthorizedAccessException();
                }
                else if (role == "Modo")
                {
                    if (await _context.Modos.FindAsync(userId) == null)
                        throw new UnauthorizedAccessException();
                }
                else throw new UserNotFoundException();

                // 3. Supprimer l'activité
                _dbSet.Remove(activity);
                await _context.SaveChangesAsync();
                return activity;
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
        }
        
        public async Task<List<Activity>> GetActivitiesByUserIdAsync(string userId)
        {
            try
            {
                return await _dbSet.Where(a => a.UserId == userId).ToListAsync();
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
        }

        public async Task<Activity> GetActivityByIdAsync(int id)
        {
            try
            {
                Activity? activity = await _dbSet.FindAsync(id);
                if (activity == null) throw new ActivityNotFoundException();
                return activity;
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
        }
    }
}