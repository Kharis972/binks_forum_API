using binks_forum_API.Data;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace binks_forum_API.Repositories
{
    public class NewsTopicRepository : Repository<NewsTopics, int>, INewsTopicRepository
    {
        public NewsTopicRepository(ApplicationDataBaseContext context) : base(context) { }
        public async Task<NewsTopics> AddNewNewsTopicAsync(string userId, string role, int newsId, int topicId)
        {
            try
            {
                if(role == "Admin")
                {
                    Admin? admin = await _context.Admins.FindAsync(userId);
                    if(admin == null) throw new ForbiddenException();
                }
                else if(role == "Modo")
                {
                    Modo? modo = await _context.Modos.FindAsync(userId);
                    if(modo == null) throw new ForbiddenException();
                }
                else
                {
                    throw new ForbiddenException();
                }

                NewsTopics? doesNewsTopicAlreadyExists = await _dbSet.FirstOrDefaultAsync(nt => nt.NewsId == newsId && nt.TopicId == topicId);
                if(doesNewsTopicAlreadyExists == null)
                {
                    NewsTopics newsTopics = new NewsTopics
                    (
                        newsId,
                        topicId
                    );

                    try 
                    {
                        await _dbSet.AddAsync(newsTopics);
                        await _context.SaveChangesAsync();
                        return newsTopics;
                    }
                    catch(Exception)
                    {
                        throw new DatabaseUpdateException();
                    }
                }
                else
                {
                    throw new EntityAlreadyExists();
                }
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }

        public async Task DeleteNewsTopicAsync(int newsTopicId, string role, string userId)
        {
            try
            {
                if(role == "Admin")
                {
                    Admin? admin = await _context.Admins.FindAsync(userId);
                    if(admin == null) throw new ForbiddenException();
                }
                else if(role == "Modo")
                {
                    Modo? modo = await _context.Modos.FindAsync(userId);
                    if(modo == null) throw new ForbiddenException();
                }
                else
                {
                    throw new ForbiddenException();
                }

                NewsTopics? newsTopicToDelete = await _dbSet.FindAsync(newsTopicId);
                await _context.SaveChangesAsync();

            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }
    }   
}