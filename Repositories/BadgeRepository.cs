using binks_forum_API.Data;
using binks_forum_API.DTOs.Badge;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using Microsoft.EntityFrameworkCore;

namespace binks_forum_API.Repositories
{
    public class BadgeRepository : Repository<Badge, int>
    {
        public BadgeRepository(ApplicationDataBaseContext context) : base(context) {}

        public async Task<Badge> AddNewBadgeAsync(NewBadge newBadge, string userId)
        {
            Admin? admin = await _context.Admins.FindAsync(userId);
            try
            {
                if(admin != null)
                {
                    if(await _dbSet.FirstOrDefaultAsync(b => b.MinXp == newBadge.MinXp || b.ImageUrl == newBadge.ImageUrl) != null)
                    {
                        throw new BadgeAlreadyExistsException();
                    }
                    else
                    {
                        Badge badge = new Badge
                        (
                            newBadge.Name, 
                            newBadge.Description, 
                            newBadge.MinXp, 
                            newBadge.ImageUrl, 
                            newBadge.PointsRequired,
                            DateTime.Now,
                            DateTime.Now
                        );

                        try
                        {
                            await _dbSet.AddAsync(badge);
                            await _context.SaveChangesAsync();
                            return badge;
                        }
                        catch(DatabaseUpdateException)
                        {
                            throw new DatabaseUpdateException();
                        }
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

        public async Task<Badge> EditBadgeAsync(EditBadge editBadge, string userId, int badgeId)
        {
            Badge? badge;
            try
            {
                try
                {
                    Admin? admin = await _context.Admins.FindAsync(userId);
                    if(admin != null)
                    {
                        badge = await _dbSet.FindAsync(badgeId);
                        if(badge == null)
                        {
                            throw new BadgeNotFoundException();
                        }
                    }
                    else
                    {
                        throw new ForbiddenException();
                    }
                }
                catch(DatabaseGlobalException)
                {
                    throw new DatabaseGlobalException();
                }
                if
                (
                    editBadge.Name != editBadge.Name && 
                    editBadge.Description != editBadge.Description &&
                    editBadge.MinXp != editBadge.MinXp &&
                    editBadge.ImageUrl != editBadge.ImageUrl &&
                    editBadge.PointsRequired != editBadge.PointsRequired
                )
                {
                    if (editBadge.Name != editBadge.Name)
                    {
                        if (!string.IsNullOrEmpty(editBadge.Name))
                        {
                            editBadge.Name = editBadge.Name.Trim();
                        }
                    }
                    if (editBadge.Description != editBadge.Description)
                    {
                        if (!string.IsNullOrEmpty(editBadge.Description))
                        {
                            editBadge.Description = editBadge.Description.Trim();
                        }
                    }
                    if (editBadge.MinXp != editBadge.MinXp)
                    {
                        editBadge.MinXp = editBadge.MinXp;
                    }
                    if (editBadge.ImageUrl != editBadge.ImageUrl)
                    {
                        if (!string.IsNullOrEmpty(editBadge.ImageUrl))
                        {
                            editBadge.ImageUrl = editBadge.ImageUrl.Trim();
                        }
                    }
                    if (editBadge.PointsRequired != editBadge.PointsRequired)
                    {
                        editBadge.PointsRequired = editBadge.PointsRequired;
                    }
                    if (await _dbSet.FirstOrDefaultAsync(b => b.ImageUrl == editBadge.ImageUrl || b.MinXp == editBadge.MinXp) != null)
                    {
                        throw new BadgeAlreadyExistsException();
                    }
                    try
                    {
                        _dbSet.Update(badge);
                        await _context.SaveChangesAsync();
                        return badge;
                    }
                    catch(Exception)
                    {
                        throw new DatabaseUpdateException();
                    }
                }
                else
                {
                    throw new NoChangesException();
                }
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }

        public async Task DeleteBadgeAsync(string userId, int badgeId)
        {
            try
            {
               
                Admin? admin = await _context.Admins.FindAsync(userId);
                if(admin == null) throw new ForbiddenException();
                
                Badge? badge = await _dbSet.FindAsync(badgeId);
                if (badge != null)
                {
                    _dbSet.Remove(badge);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new BadgeNotFoundException();
                }
            }
            catch (DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch (Exception)
            {
                throw new DatabaseGlobalException();
            }
        }
    }
}