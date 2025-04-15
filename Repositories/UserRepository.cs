//#pragma warning disable

using Microsoft.EntityFrameworkCore;
using binks_forum_API.Data;
using binks_forum_API.DTOs.User;
using binks_forum_API.Helpers;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Helpers.CustomExceptions;


namespace binks_forum_API.Repositories
{
    public class UserRepository : Repository<User, string>, IUserRepository
    {
        //Constructeur du UserRepository
        public UserRepository(ApplicationDataBaseContext context) : base(context)
        {

        } 

        public async Task<User> SignupAsync(NewUserRequest newUserRequest)
        {
            bool userExists;

            //Tente d'aller chercher l'entity dans la db
            try
            {
                userExists = await _dbSet.AnyAsync(u => u.Mail == newUserRequest.Mail.Trim());
            }
            //Si il y a y a une exception, il renverait DatabaseGlobalException
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }

            if(userExists == true)
            {
                throw new UserAlreadyExistsException();
            }
            else
            {
                string generatedId;
                int tries = 0;
                while(true)
                {
                    tries++;  
                    generatedId = Guid.NewGuid().ToString("N") + "-mms"; 

                    try
                    {
                        if(!await _dbSet.AnyAsync(u => u.Id == generatedId))
                        {
                            break;
                        }
                    }
                    catch(Exception)
                    {
                        throw new DatabaseGlobalException();
                    }
                    if(tries == 36)
                    {
                        throw new ServerInternalException();
                    }
                }


                User newUser = new User
                (
                    generatedId,
                    newUserRequest.FirstName,
                    newUserRequest.LastName,
                    newUserRequest.NickName,
                    0,
                    1,  
                    Encryption.HashPassword(newUserRequest.Password), // Assuming this is the encryptedPassword        
                    newUserRequest.Country,
                    newUserRequest.Mail,
                    DateTime.Now,
                    true,
                    false,
                    null,
                    newUserRequest.Age
                );

                //Cible une possible exception lors de la sauvegarde
                try
                {
                    await _dbSet.AddAsync(newUser);
                    await _context.SaveChangesAsync();
                    return newUser;
                }
                catch
                {
                    throw new DatabaseUpdateException();
                }

            }
        }
        public async Task<User> LoginAsync(LoginRequest loginRequest)
        {
            
            User? user;

            // Tente d'aller chercher l'entity dans la db
            try
            {
                user = await _dbSet
                .FirstOrDefaultAsync(u => u.Mail == loginRequest.Mail.Trim());
            }
            //Si il y a y a une exception, il renverait DatabaseGlobalException
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }

            // Vérification des résultats de recherche et du mot de passe
            if (user == null)
            {                
                throw new UserNotFoundException();
            }
            if (!Encryption.VerifyPassword(loginRequest.Password, user.EncryptedPassword))
            {                
                throw new IdentifiersMissMatchException();
            }

            //Tente d'aller chercher l'entity dans la db
            try
            {
                Admin? admin = await _context.Admins.FirstOrDefaultAsync(a => a.Id == user.Id);
                if(admin != null)
                {
                    return admin;
                }

                //Vérifie si l'user est modo
                Modo? modo = await _context.Modos.FirstOrDefaultAsync(m => m.Id == user.Id);
                if(modo != null)
                {
                    return modo;
                }
            }
            //Si il y a une exception, il renverait DatabaseGlobalException
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }

                // Retourne l'utilisateur si toutes les vérifications sont réussies
                return user;
            
        }

        public async Task DeleteAsync(string Id)
        {
            try
            {
                var user = await _dbSet.FindAsync(Id); // Recherche le user dans la db
                if (user == null)
                {
                    throw new UserNotFoundException();
                }

                _dbSet.Remove(user); // Suppression du user
                await _context.SaveChangesAsync(); // Sauvegarder les modifs
            }
            catch (Exception)
            {
                // Gérer les erreurs génériques ou spécifiques (exemple : log des erreurs)
                throw new DatabaseGlobalException();
            }

        }
        public async Task<User> EditAsync(string id, EditRequest editRequest)
        {
            User? user;

            try
            {
                user = await _dbSet.FindAsync(editRequest); // Rechercher l'utilisateur par son identifiant
            }
            catch
            {
                throw new DatabaseGlobalException();
            }
                if (user == null)
                {
                    throw new UserNotFoundException();
                }
                else
                {
                    // Mettre à jour les propriétés de l'utilisateur
                    if(user.FirstName != editRequest.FirstName)
                    {
                        user.FirstName = editRequest.FirstName;
                    }
                    if(user.LastName != editRequest.LastName)
                    {
                        user.LastName = editRequest.LastName;
                    }
                    if(user.NickName != editRequest.NickName)
                    {
                        user.NickName = editRequest.NickName;
                    }
                    if(user.Country != editRequest.Country)
                    {
                        user.Country = editRequest.Country;
                    }

                    _dbSet.Update(user); // Marquer l'objet comme modifié
                    await _context.SaveChangesAsync(); // Sauvegarder les modifications
                    return user; // Retourner l'utilisateur modifié
                }

                
                
                
        }
    }
}