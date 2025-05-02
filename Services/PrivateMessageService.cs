using binks_forum_API.DTOs.PrivateMessage;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service;
using binks_forum_API.Services.Interfaces;

namespace binks_forum_API.Services
{
    public class PrivateMessageService : Service<PrivateMessage, int>, IPrivateMessageService 
    {
        private readonly IPrivateMessageRepository _privateMessageRepository;

        public PrivateMessageService(IPrivateMessageRepository privateMessageRepository) : base(privateMessageRepository)
        {
            _privateMessageRepository = privateMessageRepository;
        }

        public async Task<PrivateMessage> AddNewPrivateMessageAsync(NewPrivateMessage newPrivateMessage, string userId)
        {
            try
            {
                return await _privateMessageRepository.AddNewPrivateMessageAsync(newPrivateMessage, userId);
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(UserNotFoundException)
            {
                throw new UserNotFoundException();
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }

        public async Task<PrivateMessage> EditPrivateMessageAsync(int id, string userId, EditPrivateMessage editPrivateMessage)
        {
            try
            {
                return await _privateMessageRepository.EditPrivateMessageAsync(id, userId, editPrivateMessage);
            }
            catch(PrivateMessageNotFoundException)
            {
                throw new PrivateMessageNotFoundException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(UserNotFoundException)
            {
                throw new UserNotFoundException();
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }

        public async Task DeletePrivateMessage(int privateMessageId, string userId)
        {
            try
            {
                await _privateMessageRepository.DeletePrivateMessageAsync(privateMessageId, userId);
            }
            catch(PrivateMessageNotFoundException)
            {
                throw new PrivateMessageNotFoundException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }

    }
}