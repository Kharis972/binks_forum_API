using binks_forum_API.Data;
using binks_forum_API.DTOs.TopicMessages;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace binks_forum_API.Repositories
{
    public class TopicMessagesRepository : Repository<TopicMessages, int>, ITopicMessagesRepository
    {
        public TopicMessagesRepository(ApplicationDataBaseContext context) : base(context)
        {

        }

        public async Task<TopicMessages> AddNewTopicMessagesAsync(int topicId, string userId, AddNewTopicMessages newTopicMessages)
        {
            User? user;
            try
            {
                user = await _context.Users.FindAsync(userId);
            }
            catch
            {
                throw new DatabaseGlobalException();
            }

            if(user != null)
            {
                if(user.IsShadowBan == false)
                {
                    try
                    {
                        if(await _dbSet.FirstOrDefaultAsync(t => t.Body == newTopicMessages.Body.Trim()) == null)
                        {
                            TopicMessages topicMessages = new TopicMessages(topicId, userId, DateTime.Now, 0, newTopicMessages.Body.Trim());

                            try
                            {
                                await _dbSet.AddAsync(topicMessages);
                                await _context.SaveChangesAsync();
                                return topicMessages;
                            }
                            catch(DatabaseUpdateException)
                            {
                                throw new DatabaseUpdateException();
                            }
                        }
                        else
                        {
                            throw new TopicMessagesAlreadyExistsException();
                        }
                    }
                    catch(Exception)
                    {
                        throw new DatabaseGlobalException();
                    }
                }
                else
                {
                    throw new UnauthorizedException();
                }
            }
            else
            {
                throw new UserNotFoundException();
            }
        }

        public async Task<TopicMessages> EditTopicMessagesAsync(string userId, int topicId, EditTopicMessages editTopicMessages, int topicMessagesId)
        {
            User? user;
            try
            {
                user = await _context.Users.FindAsync(userId);
            }
            catch
            {
                throw new DatabaseGlobalException();
            }

            if(user != null)
            {
                if(user.IsShadowBan == false)
                {
                    
                    if(await _context.Topics.AnyAsync(t => t.TopicId == topicId))
                    {
                        try
                        {
                            TopicMessages? topicMessages = await _dbSet.FindAsync(topicMessagesId);
                            if (topicMessages != null)
                            {
                                if(topicMessages.Body != editTopicMessages.Body)
                                {
                                    topicMessages.Body = editTopicMessages.Body;
                                } 
                                else
                                {
                                    throw new NoChangesException();
                                }

                                try
                                {
                                    _dbSet.Update(topicMessages);
                                    await _context.SaveChangesAsync();
                                    return topicMessages;
                                }
                                catch
                                {
                                    throw new DatabaseUpdateException();
                                }
                            }
                            else
                            {
                                throw new TopicNotFoundException();
                            }
                        }
                        catch
                        {
                            throw new DatabaseGlobalException();
                        }
                    }
                    else
                    {
                        throw new TopicNotFoundException();
                    }
                }
                else
                {
                    throw new UnauthorizedException();
                }
            }
            else
            {
                throw new UserNotFoundException();
            }
        }

        public async Task DeleteTopicMessagesAsync(string userId, int topicId, int topicMessagesId)
        {
            try
            {
                TopicMessages? topicMessages = await _dbSet.FindAsync(topicMessagesId);
                if (topicMessages == null) 
                {
                    throw new TopicMessagesNotFoundException();
                }
                else if(topicMessages.TopicId != topicId)
                {
                    throw new TopicNotFoundException();
                }
                else if(topicMessages.UserId != userId)
                {
                    throw new UnauthorizedException();
                }
                    try
                    {
                        _dbSet.Remove(topicMessages);
                        await _context.SaveChangesAsync();
                    }      
                    catch(DatabaseUpdateException)
                    {
                        throw new DatabaseUpdateException();
                    }       
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
        }
    }
}