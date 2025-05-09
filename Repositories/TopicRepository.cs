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
                        if(await _dbSet.FirstOrDefaultAsync(t => t.Name == newTopic.Name) != null)
                        {
                            Topic topic = new Topic
                            (
                                newTopic.Description, 
                                user.Id, 
                                newTopic.Name, 
                                newTopic.Subjects, 
                                0, 
                                1, 
                                newTopic.TopicIcon, 
                                DateTime.Now 
                            );

                            try
                            {
                                await _dbSet.AddAsync(topic);
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
                            throw new TopicAlreadyExistsException();
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

        public async Task<Topic> EditTopicAsync(string userId, int topicId, EditTopic editTopic)
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
                            if(topic.Name != editTopic.Name)
                            {
                                topic.Name = editTopic.Name;
                            } 
                            if(topic.Description != editTopic.Description)
                            {
                                topic.Description = editTopic.Description;
                            }
                            if(topic.Subjects != editTopic.Subjects)
                            {
                                topic.Subjects = editTopic.Subjects;
                            }
                            if(topic.TopicIcon != editTopic.TopicIcon)
                            {
                                topic.TopicIcon = editTopic.TopicIcon;
                            }

                            try
                            {
                                _dbSet.Update(topic);
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