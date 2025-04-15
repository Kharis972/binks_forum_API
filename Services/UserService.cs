using binks_forum_API.DTOs.User;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service.Interfaces;

namespace binks_forum_API.Service
{
    public class UserService : Service<User, string>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> SignupAsync(NewUserRequest newUserRequest)
        {
            try
            {
                return await _userRepository.SignupAsync(newUserRequest);
            }
            //Renvoi les erreurs aux controllers
            catch(DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch(UserAlreadyExistsException)
            {
                throw new UserAlreadyExistsException();
            }
            catch(DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch(Exception)
            {
                throw new ServerInternalException();
            }
        }

        public async Task<User> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                return await _userRepository.LoginAsync(loginRequest);
            }
            catch(DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch(UserNotFoundException)
            {
                throw new UserNotFoundException();
            }
            catch(IdentifiersMissMatchException)
            {
                throw new IdentifiersMissMatchException();
            }
            catch(Exception)
            {
                throw new ServerInternalException();
            }
        }

        public async Task DeleteAsync(string Id)
        {
            try
            {
                // Appel au dépôt pour supprimer l'utilisateur
                await _userRepository.DeleteAsync(Id);
            }
            catch (UserNotFoundException)
            {
                throw new UserNotFoundException();
            }
            catch (DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch (Exception)
            {
                throw new ServerInternalException();
            }
        }

        public async Task<User> EditAsync(string id, EditRequest editRequest)
        {
            try
            {

                // Appel au dépôt pour modifier l'utilisateur
                return await _userRepository.EditAsync(id, editRequest);
            }
            catch (UserNotFoundException)
            {
                throw new UserNotFoundException();
            }
            catch (DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch (Exception)
            {
                throw new ServerInternalException();
            }
        }
    }
}