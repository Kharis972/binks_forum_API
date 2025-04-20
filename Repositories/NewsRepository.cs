using binks_forum_API.Data;
using binks_forum_API.DTOs.News;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using Microsoft.EntityFrameworkCore;

namespace binks_forum_API.Repositories
{
    public class NewsRepository : Repository<News, int>
    {
        public NewsRepository(ApplicationDataBaseContext context) : base(context) {}

        public async Task<News> AddNewsAsync(AddNews addNews, string userId, string role)
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

                News? doesNewsAlreadyExists = await _context.News.FirstOrDefaultAsync(n => n.Name == addNews.Name);
                if(doesNewsAlreadyExists == null)
                {
                    News news = new News
                    (
                        addNews.Name,
                        addNews.Description,
                        addNews.Body,
                        DateTime.Now
                    );

                    try
                    {
                        await _dbSet.AddAsync(news);
                        await _context.SaveChangesAsync();
                        return news;
                    }
                    catch(Exception)
                    {
                        throw new DatabaseUpdateException();
                    }
                }
                else
                {
                    throw new NewsAlreadyExistsException();
                }              
            } 
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }  
        }

        public async Task<News> EditNewsAsync(EditNews editNews, string userId, string role, int newsId)
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

                News? news = await _context.News.FindAsync(newsId);
                if(news == null)
                {
                    throw new NewsNotFoundException();
                }
                else
                {
                    if
                    (
                        editNews.Name != editNews.Name &&
                        editNews.Description != editNews.Description &&
                        editNews.Body != editNews.Body &&
                        editNews.ReleaseDate != editNews.ReleaseDate
                    )
                    {
                        if(news.Name != editNews.Name)
                        {
                            news.Name = editNews.Name.Trim();
                        }
                        if(news.Description != editNews.Description)
                        {
                            news.Description = editNews.Description.Trim();
                        }
                        if(news.Body != editNews.Body)
                        {
                            news.Body = editNews.Body.Trim();
                        }

                        news.ReleaseDate = DateTime.Now;
                        
                        if(await _dbSet.FirstOrDefaultAsync(n => n.Name == editNews.Name || n.Description == editNews.Description) != null)
                        {
                            throw new NewsAlreadyExistsException();
                        }
                        try
                        {
                            _dbSet.Update(news);
                            await _context.SaveChangesAsync();
                            return news;
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
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }

        public async Task DeleteNewsAsync(int newsId, string role, string userId)
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

                News? newsToDelete = await _dbSet.FindAsync(newsId);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new DatabaseGlobalException();
            }
        }
    }
}