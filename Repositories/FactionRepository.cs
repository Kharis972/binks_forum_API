using binks_forum_API.Data;
using binks_forum_API.DTOs.Faction;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;

namespace binks_forum_API.Repositories
{
    public class FactionRepository : Repository<Faction, int>
    {
        public FactionRepository(ApplicationDataBaseContext context) : base(context) {}
        public async Task<Faction> AddNewFactionAsync(NewFaction newFaction, string adminId)
        {
            Admin? admin = await _context.Admins.FindAsync(adminId);
            try
            {
                if(admin != null)
                {
                    Faction faction = new Faction
                    (
                        newFaction.Name,
                        newFaction.Description,
                        newFaction.imageUrl
                    );
                    await _context.AddAsync(faction);
                    await _context.SaveChangesAsync();
                    return faction;
                }
                else
                {
                throw new AdminNotFoundException();
                }
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        } 

        public async Task<Faction> EditFactionAsync(EditFaction editfaction, int factionId, string adminId)
        {
            Admin? admin = await _context.Admins.FindAsync(adminId);
            try
            {
                if(admin != null)
                {
                    Faction? faction = await _context.Factions.FindAsync(factionId);
                    if(faction != null)
                    {
                        faction.Name = editfaction.Name;
                        faction.Description = editfaction.Description;
                        faction.ImageUrl = editfaction.ImageUrl;

                        try
                        {
                            _context.Update(faction);
                            await _context.SaveChangesAsync();
                            return faction;
                        }
                        catch(DatabaseUpdateException)
                        {
                            throw new DatabaseUpdateException();
                        }
                    }
                    else
                    {
                        throw new FactionNotFoundException();
                    }
                }
                else
                {
                    throw new AdminNotFoundException();
                }
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }

        public Task<Faction> DeleteFactionAsync(int factionId, string adminId)
        {
            Admin? admin = _context.Admins.Find(adminId);
            try
            {
                if(admin != null)
                {
                    Faction? faction = _context.Factions.Find(factionId);
                    if(faction != null)
                    {
                        _context.Remove(faction);
                        _context.SaveChanges();
                        return Task.FromResult(faction);
                    }
                    else
                    {
                        throw new FactionNotFoundException();
                    }
                }
                else
                {
                    throw new AdminNotFoundException();
                }
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        }
    }
}