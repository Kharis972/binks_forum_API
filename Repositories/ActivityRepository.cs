using binks_forum_API.Data;
using binks_forum_API.DTOs.Activities;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;

namespace binks_forum_API.Repositories
{
    public class ActivityRepository : Repository<Activity, int>
    {
        public ActivityRepository(ApplicationDataBaseContext context) : base(context) {}

        public async Task<Activity> AddNewActivityAsync(string modoId, string userId, string adminId, NewActivity newActivity)
        {
            Activity? activity;
            try
            {
                //Vérifie si l'Id admin et l'Id modo sont existant dans la DB
                Admin? admin = await _context.Admins.FindAsync(adminId);
                Modo? modo = await _context.Modos.FindAsync(modoId);

                //Si l'
                if(admin != null && modo != null)
                {
                    if(admin.Id == userId)
                    {
                        Activity newActivityInstance = new Activity()
                        {
                            Name = newActivity.Name,
                            Description = newActivity.Description,
                            CreationDate = DateTime.Now, // Propriété init
                            ScheduledDate = newActivity.ScheduledDate,
                            EndingDate = newActivity.EndingDate
                        };

                        // 4. Sauvegarde
                        await _context.Activities.AddAsync(newActivityInstance);
                        await _context.SaveChangesAsync();

                        return newActivityInstance;
                    }
                    else
                    {
                        throw new ForbiddenException();
                    }
                }
                else
                {
                    throw new ForbiddenException();
                }
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }
    }
}