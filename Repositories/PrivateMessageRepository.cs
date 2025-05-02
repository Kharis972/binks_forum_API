using binks_forum_API.Data;
using binks_forum_API.DTOs.PrivateMessage;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;

namespace binks_forum_API.Repositories
{
    public class PrivateMessageRepository : Repository<PrivateMessage, int>, IPrivateMessageRepository
    {
        private readonly ApplicationDataBaseContext _context;

        public PrivateMessageRepository(ApplicationDataBaseContext context) : base(context) {}

        public async Task<PrivateMessage> AddNewPrivateMessageAsync(NewPrivateMessage newPrivateMessage, string userId)
        {
            try
            {
                User? user = await _context.Users.FindAsync(userId);
                if(user != null)
                {
                    PrivateMessage? privateMessage = new PrivateMessage
                    (
                        newPrivateMessage.UserId,
                        newPrivateMessage.MessageContent,
                        newPrivateMessage.SentDate,
                        newPrivateMessage.IsRead,
                        newPrivateMessage.SenderId,
                        newPrivateMessage.ReceiverId,
                        newPrivateMessage.Subject
                    );
                    try
                    {
                        await _dbSet.AddAsync(privateMessage);
                        await _context.SaveChangesAsync();
                        return privateMessage;
                    }
                    catch (DatabaseUpdateException)
                    {
                        throw new DatabaseUpdateException();
                    }
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
            catch (Exception)
            {
                throw new DatabaseGlobalException();
            }
        }

        public async Task<PrivateMessage> EditPrivateMessageAsync(int id, string userId, EditPrivateMessage editPrivateMessage)
        {
            try
            {
                User? user = await _context.Users.FindAsync(userId);
                if (user != null)
                {
                    PrivateMessage? privateMessage = await _context.PrivateMessages.FindAsync(id);
                    if(privateMessage != null)
                    {
                        privateMessage.MessageContent = editPrivateMessage.MessageContent;
                        privateMessage.SentDate = editPrivateMessage.SentDate;
                        privateMessage.IsRead = editPrivateMessage.IsRead;
                        privateMessage.SenderId = editPrivateMessage.SenderId;
                        privateMessage.ReceiverId = editPrivateMessage.ReceiverId;
                        privateMessage.Subject = editPrivateMessage.Subject;
                    }
                    else
                    {
                        throw new PrivateMessageNotFoundException();
                    }

                    try
                    {
                        _context.PrivateMessages.Update(privateMessage);
                        await _context.SaveChangesAsync();
                        return privateMessage;
                    }
                    catch (DatabaseUpdateException)
                    {
                        throw new DatabaseUpdateException();
                    }
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
            catch (Exception)
            {
                throw new DatabaseGlobalException();
            }
        }

        public async Task DeletePrivateMessageAsync(int privateMessageId, string userId)
        {
            try
            {
                PrivateMessage? privateMessage = await _context.PrivateMessages.FindAsync(privateMessageId);
                if(privateMessage != null)
                {
                    User? user = await _context.Users.FindAsync(userId);
                    if(user != null)
                    {
                        _dbSet.Remove(privateMessage);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new DatabaseUpdateException();
                    }
                }
                else
                {
                    throw new PrivateMessageNotFoundException();
                }
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }
    }
}