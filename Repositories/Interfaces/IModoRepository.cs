using binks_forum_API.DTOs.Modo;
using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    public interface IModoRepository : IRepository<Modo, string>
    {
        Task<Modo> AddNewModoAsync(string userId, string adminId, NewModo newModo, string userIdToPromote);
        Task DeleteModoAsync(string userId, string adminId, string modoId);
    }
}