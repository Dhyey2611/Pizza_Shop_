using System.Collections.Generic;
using System.Threading.Tasks;
using Pizza_Shop_.Models;
using Pizza_Shop_.ViewModels;

namespace Pizza_Shop_.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetUsersByRoleAsync(string role);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task<User?> GetUserByIdAsync(int id);
        Task DeactivateUserAsync(int id);
        Task CreateUserAsync(User user);
        Task<User?> GetLoggedInUserAsync(string email);
        Task UpdateProfileAsync(User user);
        Task<int> GetCountryIdByNameAsync(string name);
        Task<int> GetStateIdByNameAsync(string name);
        Task<int> GetCityIdByNameAsync(string name);
        User? GetUserById(int userId);
        Task<User?> GetActiveUserByEmailAsync(string email);
        Task EditUserAsync(EditUserViewModel model);
    }
}
