using Pizza_Shop_.Models;
using Pizza_Shop_.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCrypt.Net;
using Pizza_Shop_.Repositories;
using Pizza_Shop_.ViewModels;
// using Pizza_Shop_.ViewModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Pizza_Shop_.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // ✅ Get all users
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        // ✅ Get user by email
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        // ✅ Get user by ID
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User?> GetActiveUserByEmailAsync(string email)
        {
            return await _userRepository.GetActiveUserByEmailAsync(email);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        // ✅ New Method: Get Logged-in User Profile for MyProfile Page
        public async Task<User?> GetLoggedInUserAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<bool> UpdateUserPasswordAsync(User user, string newPassword)
        {
            if (user == null)
                return false;

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.password_hash = hashedPassword;
            user.password = newPassword; // ✅ Store plain password for login

            await _userRepository.UpdateUserAsync(user); // ✅ Call repository to update
            return true;
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(User loggedInUser)
        {
            var query = _userRepository.GetAllUsersAsync();

            if (loggedInUser.user_type == "Super Admin")
            {
                return await query;
            }
            else if (loggedInUser.user_type == "Account Manager")
            {
                return (await query).Where(u => u.status == "Active");
            }
            else if (loggedInUser.user_type == "Chef")
            {
                return (await query).Where(u => u.user_id == loggedInUser.user_id);
            }
            return new List<User>();
        }

        public async Task CreateUserAsync(User user)
        {
            await _userRepository.CreateUserAsync(user);
        }

        public async Task DeactivateUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user != null)
            {
                user.status = "Inactive"; // ✅ Mark user as Inactive instead of deleting
                await _userRepository.UpdateUserAsync(user);
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(user.user_id);
            if (existingUser == null) return;
            // ✅ Update all fields including the new ones
            existingUser.name = user.name;
            existingUser.Email = user.Email;
            existingUser.user_type = user.user_type;
            existingUser.status = user.status;
            if (!string.IsNullOrEmpty(user.password))
            {
                existingUser.password = user.password; // ✅ Assign new plain password
                existingUser.password_hash = BCrypt.Net.BCrypt.HashPassword(user.password); // ✅ Hash new password
                Console.WriteLine($"✅ Storing Plain Password: {existingUser.password}");
                Console.WriteLine($"✅ Hashed New Password: {existingUser.password_hash}");
            }
            else
            {
                Console.WriteLine("⚠️ No new password entered. Keeping old password.");
            }
            await _userRepository.UpdateUserAsync(existingUser); // ✅ Call repository to update user
        }

        // ✅ New Method: Update Logged-in User Profile (For MyProfile Page)
        public async Task<bool> UpdateUserProfileAsync(User user)
        {
            Console.WriteLine($"🔸 UpdateUserProfileAsync called for user email: {user.Email}");
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser == null)
            {
                Console.WriteLine("❌ User not found in database, update aborted.");
                return false; // User not found
            }
            Console.WriteLine($"✅ User found (ID: {existingUser.user_id}), updating profile fields...");
            // Update only profile-related fields
            existingUser.name = user.name;
            existingUser.password = user.password;
            existingUser.phone_no = user.phone_no;
            existingUser.country = user.country;
            existingUser.state = user.state;
            existingUser.city = user.city;
            existingUser.address = user.address;
            existingUser.zip_code = user.zip_code;
            Console.WriteLine("🔸 Calling _userRepository.UpdateUserAsync() to save updated user...");
            await _userRepository.UpdateUserAsync(existingUser);
            Console.WriteLine("✅ User profile update committed to database.");
            return true;
        }
        public async Task<bool> UpdatePassword(string email, string newPassword)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                Console.WriteLine("User Not Found");
                return false; // User not found
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.password_hash = hashedPassword;
            Console.WriteLine($"✅ New Hashed Password: {user.password_hash}");
            await _userRepository.UpdateUserAsync(user); // ✅ Use repository to update user
            Console.WriteLine("✅ Password update request sent to repository.");
            return true;
        }

        public async Task UpdateProfileAsync(User user)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(user.user_id);
            if (existingUser == null) return;

            // ✅ Update only MyProfile fields
            existingUser.name = user.name;
            existingUser.phone_no = user.phone_no;
            existingUser.country = user.country;
            existingUser.state = user.state;
            existingUser.city = user.city;
            existingUser.address = user.address;
            existingUser.zip_code = user.zip_code;
            await _userRepository.UpdateUserAsync(existingUser);
        }
        public async Task<int> GetCountryIdByNameAsync(string name)
        {
            return await _userRepository.GetCountryIdByNameAsync(name);
        }
        public async Task<int> GetStateIdByNameAsync(string name)
        {
            return await _userRepository.GetStateIdByNameAsync(name);
        }
        public async Task<int> GetCityIdByNameAsync(string name)
        {
            return await _userRepository.GetCityIdByNameAsync(name);
        }
        public async Task EditUserAsync(EditUserViewModel model)
        {
            await _userRepository.EditUserAsync(model);
        }
    }
}
