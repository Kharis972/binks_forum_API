using binks_forum_API.Data;
using binks_forum_API.DTOs.Modo;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace binks_forum_API.Repositories
{
    public class ModoRepository : Repository<Modo, string>, IModoRepository
    {
        public ModoRepository(ApplicationDataBaseContext context) : base(context) {}
        public async Task<Modo> AddNewModoAsync(string userId, string adminId, NewModo newModo, string userIdToPromote)
        {
            Modo? modo;

            try
            {
                //Vérifie si l'Id admin est existant dans la DB
                Admin? admin = await _context.Admins.FindAsync(adminId);

                //Si l'admin n'est pas null
                if (admin != null)
                {
                    //Vérifier si la foreign key de ce admin à user = à l'id stocké dans le JWT
                    if(admin.Id == userId)
                    {
                        //Vérifie si l'useridtopromote reçu existe dans la DB
                        User? user = await _context.Users.FindAsync(userIdToPromote);
                        if (user != null)
                        {
                            string generatedId;
                            int tries = 0;
                            while(true)
                            {
                                tries++;  
                                //GlobalUniqueIDentifier crée 2 token de 32 caractères + la signature "-bf"
                                generatedId = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N") + "-bf"; 

                                if(!await _dbSet.AnyAsync(u => u.ModoId == generatedId))
                                {
                                    break;
                                }
                                if(tries == 35)
                                {
                                    throw new InvalidOperationException();
                                }
                            }

                            modo = new Modo
                            (
                                generatedId
                            );
                            modo.User = user;

                            try
                            {
                                await _context.Modos.AddAsync(modo);
                                await _context.SaveChangesAsync();
                                return modo;
                            }
                            catch(Exception)
                            {
                                throw new DatabaseUpdateException();
                            }
                        }
                        else
                        {
                            throw new UserNotFoundException();
                        }
                    }
                    else
                    {
                        throw new ForbiddenException();
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
            
        
        public async Task<Modo> EditModoAsync(string userId, string adminId, EditModo editModo)
        {

        }
        public async Task DeleteModoAsync(string userId, string adminId)
        {

        }
    }
}