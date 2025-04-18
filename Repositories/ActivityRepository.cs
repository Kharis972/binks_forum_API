using binks_forum_API.Data;
using binks_forum_API.DTOs.Activities;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;

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
                    (newActivity.Name, newActivity.Description, DateTime.Now, newActivity.ScheduledDate, newActivity.EndingDate, userId);
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
    }
}