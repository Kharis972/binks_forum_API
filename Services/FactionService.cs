using binks_forum_API.DTOs.Faction;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service;
using binks_forum_API.Services.Interfaces;

namespace binks_forum_API.Services
{
    public class FactionService : Service<Faction, int>, IFactionService
    {
        private readonly IFactionRepository _factionRepository;
        public FactionService(IFactionRepository factionRepository) : base(factionRepository)
        {
            _factionRepository = factionRepository;
        }
        public async Task<Faction> AddNewFactionAsync(NewFaction newFaction, string adminId)
        {
            try
            {
                return await _factionRepository.AddNewFactionAsync(newFaction, adminId);
            }
            catch(DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch(AdminNotFoundException)
            {
                throw new AdminNotFoundException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch
            {
                throw new ServerInternalException();
            }
        }
        public async Task<Faction> EditFactionAsync(EditFaction editfaction, int factionId, string adminId)
        {
            try
            {
                return await _factionRepository.EditFactionAsync(editfaction, factionId, adminId);
            }
            catch(DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch(AdminNotFoundException)
            {
                throw new AdminNotFoundException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch
            {
                throw new ServerInternalException();
            }
        }
        public async Task DeleteFactionAsync(int factionId, string adminId)
        {
            try
            {
                await _factionRepository.DeleteFactionAsync(factionId, adminId);
            }
            catch(DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch(AdminNotFoundException)
            {
                throw new AdminNotFoundException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch
            {
                throw new ServerInternalException();
            }
        }
    }
}