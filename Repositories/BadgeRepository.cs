using binks_forum_API.Data;
using binks_forum_API.DTOs.Badge;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using Microsoft.EntityFrameworkCore;
using binks_forum_API.Repositories.Interfaces;

namespace binks_forum_API.Repositories
{
    public class BadgeRepository : Repository<Badge, int>, IBadgeRepository
    {
        public BadgeRepository(ApplicationDataBaseContext context) : base(context) {}

        public async Task<Badge> AddNewBadgeAsync(NewBadge newBadge, string userId)
        {
            try
            {
                Admin? admin = await _context.Admins.FindAsync(userId);
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
                        catch(Microsoft.EntityFrameworkCore.DbUpdateException)
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
            catch (BadgeAlreadyExistsException) { throw; }
            catch (ForbiddenException) { throw; }
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
                if (await _context.Admins.FindAsync(userId) == null) throw new ForbiddenException();
                badge = await _dbSet.FindAsync(badgeId);
                if (badge == null) throw new BadgeNotFoundException();

                var name = string.IsNullOrWhiteSpace(editBadge.Name) ? badge.Name : editBadge.Name.Trim();
                var description = string.IsNullOrWhiteSpace(editBadge.Description) ? badge.Description : editBadge.Description.Trim();
                var imageUrl = string.IsNullOrWhiteSpace(editBadge.ImageUrl) ? badge.ImageUrl : editBadge.ImageUrl.Trim();
                if (badge.Name == name && badge.Description == description && badge.MinXp == editBadge.MinXp &&
                    badge.ImageUrl == imageUrl && badge.PointsRequired == editBadge.PointsRequired)
                    throw new NoChangesException();

                if (await _dbSet.AnyAsync(b => b.BadgeId != badgeId && (b.ImageUrl == imageUrl || b.MinXp == editBadge.MinXp)))
                    throw new BadgeAlreadyExistsException();

                badge.Name = name;
                badge.Description = description;
                badge.MinXp = editBadge.MinXp;
                badge.ImageUrl = imageUrl;
                badge.PointsRequired = editBadge.PointsRequired;
                badge.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return badge;
            }
            catch (BadgeNotFoundException) { throw; }
            catch (BadgeAlreadyExistsException) { throw; }
            catch (ForbiddenException) { throw; }
            catch (NoChangesException) { throw; }
            catch (DatabaseUpdateException) { throw; }
            catch(Exception)
            {
                throw new DatabaseUpdateException();
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
