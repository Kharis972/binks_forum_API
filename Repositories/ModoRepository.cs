using binks_forum_API.DTOs.Modo;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using Microsoft.EntityFrameworkCore;

namespace binks_forum_API.Repositories
{
    public class ModoRepository : Repository<ModoRepository, string>
    {
        public async Task<Modo> AddNewModoAsync(string userId, string adminId, NewModo newModo)
        {
            User? user;
            if(await _context.Users.FindAsync(userId) != null)
            {
                
            }
            else
            {
                throw new UserNotFoundException();
            }
        }
        public async Task<Modo> EditModoAsync(string userId, string adminId, EditModo editModo)
        {

        }
        public async Task DeleteModoAsync(string userId, string adminId)
        {

        }
    }
}