using binks_forum_API.Data;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace binks_forum_API.Repositories
{

    public class RankRepository : Repository<Rank, int>, IRankRepository
    {
        public RankRepository(ApplicationDataBaseContext context) : base(context) { }

        public async Task<List<Rank>> GetRanksByUserIdAsync(string userId)
        {
            try
            {
                return await _dbSet.Where(r => r.UserId == userId).ToListAsync();
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

        public async Task<Rank> AddNewRankAsync(Rank rank)
        {
            try
            {
                if(rank != null)
                {
                    rank.UserId = null!; // Set UserId to null when creating a new rank
                    if (rank.Id == 0)
                    {
                        rank.Id = await _dbSet.MaxAsync(r => r.Id) + 1; // Assign a new ID based on the maximum existing ID
                    }
                    rank.Name = rank.Name.Trim(); // Trim whitespace from the name
                    rank.Description = rank.Description.Trim(); // Trim whitespace from the description
                    rank.RankIcon = rank.RankIcon.Trim(); // Trim whitespace from the rank icon

                    rank.MinXp = rank.MinXp; // Set the minimum XP value
                    


                }
                else
                {
                    throw new RankNotFoundException();
                }
                await _dbSet.AddAsync(rank);
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
        public async Task<Rank> EditRankAsync(Rank rank)
        {
            try
            {
                if(rank != null)
                {
                    
                }
                else
                {
                    throw new RankNotFoundException();
                }
                
                rank.Name = rank.Name.Trim(); // Trim whitespace from the name
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