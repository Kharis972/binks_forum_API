using binks_forum_API.Data;
using binks_forum_API.DTOs.Rank;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace binks_forum_API.Repositories
{

    public class RankRepository : Repository<Rank, int>, IRankRepository
    {
        public RankRepository(ApplicationDataBaseContext context) : base(context) { }

        public async Task<Rank> AddNewRankAsync(string userId, NewRank newRank)
        {
            try
            {
                Admin? admin;
                
                admin = await _context.Admins.FindAsync(userId);
                if (admin == null)
                {
                    throw new ForbiddenException();
                }

                if(await _dbSet.FirstOrDefaultAsync(r => r.RankIcon == newRank.RankIcon || r.MinXp == newRank.MinXp) != null)
                {
                throw new RankAlreadyExistsException();
                }
                else
                {  
                    Rank rank = new Rank
                    (
                        newRank.Name.Trim(), // Trim whitespace from the name
                        newRank.Description.Trim(), // Trim whitespace from the description
                        newRank.MinXp,
                        newRank.RankIcon.Trim()
                    );

                    try
                    {
                        await _dbSet.AddAsync(rank);
                        await _context.SaveChangesAsync();
                        return rank;
                    }
                    catch (DatabaseUpdateException)
                    {
                        throw new DatabaseUpdateException();
                    }
                }  
            }
            catch (RankNotFoundException)
            {
                throw new RankNotFoundException();
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
        }
        public async Task<Rank> EditRankAsync(string userId, EditRank editRank)
        {
            Rank? rank;
            try
            {
                try
                {
                    rank = await _dbSet.FindAsync(editRank); // Find the rank by its ID
                    if (rank == null)
                    {
                        throw new RankNotFoundException();
                    }
                }
                catch
                {
                    throw new DatabaseGlobalException();
                }
                // Update the properties of the rank
                if (editRank.Name != editRank.Name)
                {
                    editRank.Name = editRank.Name.Trim();
                }
                if (editRank.Description != editRank.Description)
                {
                    editRank.Description = editRank.Description.Trim();
                }
                if (editRank.MinXp != editRank.MinXp)
                {
                    editRank.MinXp = editRank.MinXp;
                }
                if (editRank.RankIcon != editRank.RankIcon)
                {
                    editRank.RankIcon = editRank.RankIcon.Trim();
                }
                if (await _dbSet.FirstOrDefaultAsync(r => r.RankIcon == editRank.RankIcon || r.MinXp == editRank.MinXp) != null)
                {
                    throw new RankAlreadyExistsException();
                }

                editRank.Name = editRank.Name.Trim(); // Trim whitespace from the name
                rank.Name = editRank.Name.Trim();
                rank.Description = editRank.Description.Trim();
                rank.MinXp = editRank.MinXp;
                rank.RankIcon = editRank.RankIcon.Trim();

                _dbSet.Update(rank);
                await _context.SaveChangesAsync();
                return rank;
            }
            catch (DbUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch (Exception)
            {
                throw new DatabaseGlobalException();
            }
        }
        public async Task DeleteRankAsync(int id)
        {
            try
            {
                Rank? rank = await _dbSet.FindAsync(id);
                if (rank != null)
                {
                    _dbSet.Remove(rank);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new RankNotFoundException();
                }
            }
            catch (DbUpdateException)
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