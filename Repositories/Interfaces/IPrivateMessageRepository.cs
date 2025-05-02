using binks_forum_API.DTOs.PrivateMessage;
using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    public interface IPrivateMessageRepository : IRepository<PrivateMessage, int>
    {
        Task<PrivateMessage> AddNewPrivateMessageAsync(NewPrivateMessage newPrivateMessage, string userId);
        Task DeletePrivateMessageAsync(int id, string userId);
        Task<PrivateMessage> EditPrivateMessageAsync(int id, string userId, EditPrivateMessage editPrivateMessage);
    }
}