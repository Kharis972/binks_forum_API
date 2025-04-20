using binks_forum_API.Data;
using binks_forum_API.DTOs.NewsRank;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using Microsoft.EntityFrameworkCore;


namespace binks_forum_API.Repositories
{
    public class NewsRankRepository : Repository<NewsRank, int>
    {
        public NewsRankRepository(ApplicationDataBaseContext context) : base(context) {}

        public async Task<NewsRank> AddNewsRankAsync(AddNewsRank addNewsRank, int rankId, string userId, int newsId)
        {
            Admin? admin;
            try
            {
                admin = await _context.Admins.FindAsync(userId);
                if(admin == null)
                {
                    throw new ForbiddenException();
                }
                else
                {
                    Rank? rank = await _context.Ranks.FindAsync(rankId);
                    if(rank == null)
                    {
                        throw new RankNotFoundException();
                    }
                    else
                    {
                        NewsRank? doesNewsRankExists = await _context.NewsRanks.FirstOrDefaultAsync(nr => nr.NewsId == newsId && nr.RankId == rankId);
                        if(doesNewsRankExists != null)
                        {
                            throw new NewsRankAlreadyExistsException();
                        }
                        else
                        {
                            NewsRank newsRank = new NewsRank
                            (
                                addNewsRank.Name,
                                addNewsRank.Description,
                                addNewsRank.MinXp,
                                addNewsRank.RankIcon,
                                rankId, 
                                newsId
                            );

                            try
                            {
                                await _dbSet.AddAsync(newsRank);
                                await _context.SaveChangesAsync();
                                return newsRank;
                            }
                            catch (Exception)
                            {
                                throw new DatabaseUpdateException();
                            }
                        }
                    }
                }
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }
        
        public async Task DeleteNewsRankAsync(int newsRankId, string role, string userId)
        {
            try
            {
                    Admin? admin = await _context.Admins.FindAsync(userId);
                    if(admin == null) throw new ForbiddenException();
                
                NewsRank? newsRank = await _dbSet.FindAsync(newsRankId);
                if (newsRank != null)
                {
                    _dbSet.Remove(newsRank);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new RankNotFoundException();
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