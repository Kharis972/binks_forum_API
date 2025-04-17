using binks_forum_API.DTOs.Modo;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service.Interfaces;

namespace binks_forum_API.Service
{
    public class ModoService : Service<Modo, string>, IModoService
    {
        private readonly IModoRepository _modoRepository;
        public ModoService(IModoRepository modoRepository) : base(modoRepository)
        {
            _modoRepository = modoRepository;
        }
        public async Task<Modo> AddNewModoAsync(string userId, string adminId, NewModo newModo, string userIdToPromote)
        {
            try
            {
                return await _modoRepository.AddNewModoAsync(userId, adminId, newModo, userIdToPromote);
            }
            catch(DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch(UserNotFoundException)
            {
                throw new UserNotFoundException();
            }
            catch(ForbiddenException)
            {
                throw new ForbiddenException();
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

        public async Task DeleteModoAsync(string userId, string adminId, string modoId)
        {
            try
            {
                await _modoRepository.DeleteModoAsync(userId, adminId, modoId);
            }
            catch(DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch(ModoNotFoundException)
            {
                throw new ModoNotFoundException();
            }
            catch(ForbiddenException)
            {
                throw new ForbiddenException();
            }
            catch
            {
                throw new ServerInternalException();
            }
        }
    }
}