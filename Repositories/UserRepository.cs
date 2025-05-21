using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pizza_Shop_.Data;
using Pizza_Shop_.Models;
using Pizza_Shop_.ViewModels;
namespace Pizza_Shop_.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.user_id == id);
        }

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // ✅ New Method: Fetch Logged-in User Profile (for MyProfile Page)
        public async Task<User?> GetLoggedInUserAsync(string email)
        {
            return await _context.Users
            .Where(u => u.Email == email)
            .Select(u => new User
            {
                user_id = u.user_id,
                name = u.name,
                Email = u.Email,
                phone_no = u.phone_no,
                country = u.country,
                state = u.state,
                city = u.city,
                address = u.address,
                zip_code = u.zip_code
            })
            .FirstOrDefaultAsync();
        }

        public async Task<User?> GetActiveUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.status == "Active");
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<User>> GetUsersByRoleAsync(string role)
        {
            return await _context.Users.Where(u => u.user_type == role).ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            Console.WriteLine("🔹 Entering UpdateUserAsync...");
            Console.WriteLine($"🔸 Attempting to find existing user with ID: {user.user_id}");
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.user_id == user.user_id);
            if (existingUser != null)
            {
                Console.WriteLine($"✅ Existing user found for update (ID: {existingUser.user_id}, Email: {existingUser.Email})");
                // ✅ Update all fields including new ones
                Console.WriteLine($"➡️ New Name: {user.name}");
                Console.WriteLine($"➡️ New Email: {user.Email}");
                Console.WriteLine($"➡️ New Username: {user.Username}");
                Console.WriteLine($"➡️ New Phone: {user.phone_no}");
                Console.WriteLine($"➡️ New Zip: {user.zip_code}");
                Console.WriteLine($"➡️ New Address: {user.address}");
                Console.WriteLine($"➡️ New UserType: {user.user_type}");
                Console.WriteLine($"➡️ New Status: {user.status}");
                Console.WriteLine($"➡️ New Country: {user.country}");
                Console.WriteLine($"➡️ New State: {user.state}");
                Console.WriteLine($"➡️ New City: {user.city}");
                existingUser.name = user.name;
                existingUser.Email = user.Email;
                existingUser.password_hash = user.password_hash ?? existingUser.password_hash;
                existingUser.user_type = user.user_type;
                existingUser.status = user.status;
                existingUser.Username = user.Username;
                existingUser.phone_no = user.phone_no;
                existingUser.zip_code = user.zip_code;
                existingUser.address = user.address;
                existingUser.country = user.country;
                existingUser.state = user.state;
                existingUser.city = user.city;
                var entry = _context.Entry(existingUser);
                entry.Property(x => x.name).IsModified = true;
                entry.Property(x => x.Email).IsModified = true;
                entry.Property(x => x.Username).IsModified = true;
                entry.Property(x => x.phone_no).IsModified = true;
                entry.Property(x => x.zip_code).IsModified = true;
                entry.Property(x => x.address).IsModified = true;
                entry.Property(x => x.user_type).IsModified = true;
                entry.Property(x => x.status).IsModified = true;
                entry.Property(x => x.country).IsModified = true;
                entry.Property(x => x.state).IsModified = true;
                entry.Property(x => x.city).IsModified = true;
                if (!string.IsNullOrEmpty(user.password))
                {
                    Console.WriteLine($"🔍 Before Assignment: user.password = {user.password}");
                    existingUser.password = user.password ?? existingUser.password; ; // ✅ Store plain password (for login if needed)
                    Console.WriteLine($"✅ Storing Plain Password: {existingUser.password}");
                    existingUser.password_hash = BCrypt.Net.BCrypt.HashPassword(user.password);
                    Console.WriteLine($"✅ Hashed New Password: {existingUser.password_hash}");
                    // ✅ Ensure EF tracks these fields as modified
                    _context.Entry(existingUser).Property(x => x.password).IsModified = true;
                    _context.Entry(existingUser).Property(x => x.password_hash).IsModified = true;
                }
                else
                {
                    Console.WriteLine("⚠️ No new password entered. Keeping old hash.");
                }
                _context.Users.Update(existingUser);
                Console.WriteLine("🔹 EF Tracking Changes: User marked as 'Modified'");
                await _context.SaveChangesAsync();
                Console.WriteLine($"✅ User {existingUser.Email} updated successfully in DB.");
            }
            else
            {
                Console.WriteLine($"❌ Error: User {user.user_id} not found in DB.");
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.status = "Inactive"; // Soft delete
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeactivateUserAsync(int id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.user_id == id);
            if (user != null)
            {
                user.status = "Inactive"; // ✅ Mark as inactive
                _context.Entry(user).State = EntityState.Modified; // ✅ Ensure only this entity is updated
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateUserAsync(User user)
        {
            var existingUser = await _context.Users.AnyAsync(u => u.Email == user.Email);
            if (!existingUser)
            {
                await _context.Users.AddAsync(user);  // ✅ Add user only if they don't exist
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateProfileAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.user_id);
            if (existingUser == null) return;
            // ✅ Only update MyProfile fields
            existingUser.name = user.name;
            existingUser.phone_no = user.phone_no;
            existingUser.country = user.country;
            existingUser.state = user.state;
            existingUser.city = user.city;
            existingUser.address = user.address;
            existingUser.zip_code = user.zip_code;
            await _context.SaveChangesAsync();
        }
        public async Task<int> GetCountryIdByNameAsync(string name)
        {
            return await _context.Country
            .Where(c => c.CountryName == name)
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        }
        public async Task<int> GetStateIdByNameAsync(string name)
        {
            return await _context.State
            .Where(s => s.StateName == name)
            .Select(s => s.Id)
            .FirstOrDefaultAsync();
        }
        public async Task<int> GetCityIdByNameAsync(string name)
        {
            return await _context.City
            .Where(c => c.CityName == name)
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        }
        public User? GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.user_id == userId);
        }
         public async Task EditUserAsync(EditUserViewModel model)
        {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.user_id == model.Id);
        if (user == null) return;

        user.name = model.FirstName + " " + model.LastName;
        user.Email = model.Email;
        user.Username = string.IsNullOrWhiteSpace(model.Username)
            ? model.Email.Split('@')[0]
            : model.Username;
        user.phone_no = model.PhoneNo;
        user.zip_code = model.ZipCode;
        user.address = model.Address;
        user.status = model.Status;
        user.user_type = model.UserType;

        user.country_id = model.CountryId;
        user.state_id = model.StateId;
        user.city_id = model.CityId;

        user.country = await _context.Country.Where(c => c.Id == model.CountryId).Select(c => c.CountryName).FirstOrDefaultAsync();
        user.state = await _context.State.Where(s => s.Id == model.StateId).Select(s => s.StateName).FirstOrDefaultAsync();
        user.city = await _context.City.Where(c => c.Id == model.CityId).Select(c => c.CityName).FirstOrDefaultAsync();

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        }
        }
}
