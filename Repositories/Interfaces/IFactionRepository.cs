using binks_forum_API.DTOs.Faction;
using binks_forum_API.Models;

namespace binks_forum_API.Repositories.Interfaces
{
    public interface IFactionRepository : IRepository<Faction, int>
    {
        Task<Faction> AddNewFactionAsync(NewFaction newFaction, string adminId);
        Task<Faction> EditFactionAsync(EditFaction editfaction, int factionId, string adminId);
        Task<bool> DeleteFactionAsync(int factionId, string adminId);
    }
}