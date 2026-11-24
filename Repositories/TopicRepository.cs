#pragma warning disable

using binks_forum_API.Data;
using binks_forum_API.DTOs.Topic;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace binks_forum_API.Repositories
{
    public class TopicRepository : Repository<Topic, int>, ITopicRepository
    {
        public TopicRepository(ApplicationDataBaseContext context) : base(context)
        {
            
        }

        public async Task<Topic> AddNewTopicAsync(string userId, NewTopic newTopic)
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
                        var name = newTopic.Name.Trim();
                        if(await _dbSet.AnyAsync(t => t.Name == name))
                        {
                            throw new TopicAlreadyExistsException();
                        }

                        Topic topic = new Topic(newTopic.Description.Trim(), user.Id, name,
                            newTopic.Subjects.Trim(), 0, 1, newTopic.TopicIcon.Trim(), DateTime.UtcNow);
                        await _dbSet.AddAsync(topic);
                        await _context.SaveChangesAsync();
                        return topic;
                    }
                    catch (TopicAlreadyExistsException)
                    {
                        throw;
                    }
                    catch (DatabaseUpdateException)
                    {
                        throw;
                    }
                    catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                    {
                        throw new DatabaseUpdateException();
                    }
                    catch
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

        public async Task<Topic> EditTopicAsync(string userId, int topicId, EditTopic editTopic)
        {
            var user = await _context.Users.FindAsync(userId)
                ?? throw new UserNotFoundException();
            if (user.IsShadowBan) throw new UnauthorizedException();
            var topic = await _dbSet.FindAsync(topicId)
                ?? throw new TopicNotFoundException();
            var isAdmin = await _context.Admins.AnyAsync(a => a.Id == userId);
            if (topic.UserId != userId && !isAdmin) throw new UnauthorizedException();

            topic.Name = editTopic.Name.Trim();
            topic.Description = editTopic.Description.Trim();
            topic.Subjects = editTopic.Subjects.Trim();
            topic.TopicIcon = editTopic.TopicIcon.Trim();
            try
            {
                await _context.SaveChangesAsync();
                return topic;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                throw new DatabaseUpdateException();
            }
        }

        public async Task<Topic> DeleteTopicAsync(string userId, int topicId)
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

            if (user != null)
            {
                if (user.IsShadowBan == false)
                {
                    try
                    {
                        var topic = await _dbSet.FindAsync(topicId);
                        if (topic != null)
                        {
                            if (topic.UserId == user.Id)
                            {
                                _dbSet.Remove(topic);
                                try
                                {
                                    await _context.SaveChangesAsync();
                                    return topic;
                                }
                                catch
                                {
                                    throw new DatabaseUpdateException();
                                }
                            }
                            else
                            {
                                throw new UnauthorizedException();
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
                    throw new UnauthorizedException();
                }
            }
            else
            {
                throw new UserNotFoundException();
            }
        }
    } 
}
