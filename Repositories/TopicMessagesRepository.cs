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
            var user = await _context.Users.FindAsync(userId)
                ?? throw new UserNotFoundException();
            if (user.IsShadowBan) throw new UnauthorizedException();
            if (!await _context.Topics.AnyAsync(topic => topic.TopicId == topicId))
                throw new TopicNotFoundException();

            var message = new TopicMessages(topicId, userId, DateTime.UtcNow, 0, newTopicMessages.Body.Trim());
            await _dbSet.AddAsync(message);
            try
            {
                await _context.SaveChangesAsync();
                return message;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                throw new DatabaseUpdateException();
            }
        }

        public async Task<TopicMessages> EditTopicMessagesAsync(string userId, int topicId, EditTopicMessages editTopicMessages, int topicMessagesId)
        {
            var user = await _context.Users.FindAsync(userId)
                ?? throw new UserNotFoundException();
            if (user.IsShadowBan) throw new UnauthorizedException();
            var message = await _dbSet.FindAsync(topicMessagesId)
                ?? throw new TopicMessagesNotFoundException();
            if (message.TopicId != topicId) throw new TopicNotFoundException();
            if (message.UserId != userId && !await _context.Admins.AnyAsync(a => a.Id == userId))
                throw new UnauthorizedException();
            var content = editTopicMessages.Content.Trim();
            if (message.Content == content) throw new NoChangesException();
            message.Content = content;
            try
            {
                await _context.SaveChangesAsync();
                return message;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                throw new DatabaseUpdateException();
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
