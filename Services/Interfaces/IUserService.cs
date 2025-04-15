using binks_forum_API.DTOs.User;
using binks_forum_API.Models;


namespace binks_forum_API.Service.Interfaces
{
    public interface IUserService : IService <User, string>
    {
        Task<User> SignupAsync(NewUserRequest newUserRequest);
        Task<User> LoginAsync(LoginRequest loginRequest);
        Task DeleteAsync(string Id);
        Task<User> EditAsync(string id, EditRequest editRequest);
    }
}