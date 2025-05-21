using Pizza_Shop_.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pizza_Shop_.ViewModels;
// using Pizza_Shop_.ViewModels;

namespace Pizza_Shop_.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> UpdatePassword(string email, string newPassword);
        Task<User?> GetActiveUserByEmailAsync(string email);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> UpdateUserPasswordAsync(User user, string newPassword);
        Task<IEnumerable<User>> GetUsersByRoleAsync(User loggedInUser);
        Task CreateUserAsync(User user);
        Task DeactivateUserAsync(int id);
        Task UpdateUserAsync(User user);
        Task<User?> GetLoggedInUserAsync(string email);
        Task UpdateProfileAsync(User user);
        Task<int> GetCountryIdByNameAsync(string name);
        Task<int> GetStateIdByNameAsync(string name);
        Task<int> GetCityIdByNameAsync(string name);
        Task EditUserAsync(EditUserViewModel user);
    }
}
