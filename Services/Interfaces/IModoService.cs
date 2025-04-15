using binks_forum_API.Models;

namespace binks_forum_API.Service.Interfaces
{
    public interface IModoService : IService<Modo, string>
    {
        Task<Modo> AddNewModoAsync(string userId, string adminId, NewModo newModo);
        Task<Modo> EditModoAsync(string userId, string adminId, EditModo editModo);
        Task DeleteModoAsync(string userId, string adminId);
    }
}