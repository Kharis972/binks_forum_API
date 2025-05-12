using binks_forum_API.DTOs.Faction;
using binks_forum_API.Models;
using binks_forum_API.Service.Interfaces;

namespace binks_forum_API.Services.Interfaces
{
    public interface IFactionService : IService<Faction, int>
    {
        Task<Faction> AddNewFactionAsync(NewFaction newFaction, string adminId);
        Task<Faction> EditFactionAsync(EditFaction editfaction, int factionId, string adminId);
        Task DeleteFactionAsync(int factionId, string adminId);
    }
}