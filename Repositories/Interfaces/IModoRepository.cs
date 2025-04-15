using binks_forum_API.DTOs.Modo;
using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    public interface IModoRepository : IRepository<Modo, string>
    {
        Task<Modo> AddNewModoAsync(string userId, string adminId, NewModo newModo, string userIdToPromote);
        Task<Modo> EditModoAsync(string userId, string adminId, EditModo editModo);
        Task DeleteModoAsync(string userId, string adminId);
    }
}