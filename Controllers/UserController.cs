// 🔄 Updated UserController.cs using ViewModels (except Delete remains unchanged)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizza_Shop_.Data;
using Pizza_Shop_.Models;
using System.Security.Claims;
using Pizza_Shop_.Services;
using Pizza_Shop_.ViewModels;
using Pizza.Shop_.Services;

namespace Pizza_Shop_.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly DatabaseContext _context;
        private readonly IPermissionService _permissionService; 
        public UserController(DatabaseContext context, IUserService userService, IPermissionService permissionService)
        {
            _context = context;
            _userService = userService;
            _permissionService = permissionService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(string searchTerm, string sortField, string sortOrder, int page = 1, int pageSize = 5)
        {
        var userEmail = User.FindFirstValue(ClaimTypes.Name);
        if (string.IsNullOrEmpty(userEmail)) return Unauthorized();
        var loggedInUser = await _userService.GetUserByEmailAsync(userEmail);
        if (loggedInUser == null) return Unauthorized();
        var users = await _userService.GetUsersByRoleAsync(loggedInUser);
        if (!string.IsNullOrEmpty(searchTerm))
        {
        users = users.Where(u => u.name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                 u.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        users = sortField switch
        {
        "Name" => sortOrder == "desc" ? users.OrderByDescending(u => u.name).ToList() : users.OrderBy(u => u.name).ToList(),
        "UserType" => sortOrder == "desc" ? users.OrderByDescending(u => u.user_type).ToList() : users.OrderBy(u => u.user_type).ToList(),
        _ => users
        };
        var paginated = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var viewModel = new UsersPageViewModel
        {
        Users = paginated.Select(u => new UserListViewModel
        {
            Id = u.user_id,
            Name = u.name,
            Email = u.Email,
            PhoneNo = u.phone_no ?? "",
            UserType = u.user_type,
            Status = u.status
        }).ToList(),
        CurrentPage = page,
        TotalUsers = users.Count(),
        SearchTerm = searchTerm ?? ""
        };
        return View(viewModel);
        }


        // GET: User/CreateEmail
        [HttpGet]
        // [Route("User/CreateEmail")]
        public IActionResult Create()
        {
            var model = new CreateUserViewModel(); // ensure model is not null
            return View(model);
        }

        //New Code
        // GET: User/CreateEmail/{userId}
        [HttpGet]
        [Route("User/CreateEmail/{userId}")]
        public IActionResult CreateEmail(int userId)
        {
        Console.WriteLine($"Attempting to fetch user with user_id: {userId}");
        // Retrieve the user by ID
        var user = _userService.GetUserByIdAsync(userId).Result;
        if (user == null)
        {
        Console.WriteLine($"User with user_id {userId} not found.");
        return NotFound("User not found.");
        }
        var emailModel = new CreateEmailViewModel
        {
        Id = user.user_id,
        Email = user.Email,
        TemporaryPassword = "TemporaryPassword" // Replace this with actual logic
        };
        return View(emailModel); // Return the CreateEmail view with the user data
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmail(CreateEmailViewModel model)
        {
        Console.WriteLine($"Attempting to fetch user with user_id: {model.Id}");
        // Retrieve the user by ID
        var user = await _userService.GetUserByIdAsync(model.Id);
        if (user == null)
        {
        Console.WriteLine($"User with user_id {model.Id} not found.");
        return NotFound("User not found.");
        }
        // Logic to generate a temporary password (replace with your logic)
        // string temporaryPassword = "TemporaryPassword";
        // Send email logic can be added here (e.g., sending the email to the user)
        TempData["EmailSuccessMessage"] = "Email successfully sent to the user!";
        return RedirectToAction("Index", "User");  // Redirect back to the User list
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ ModelState is invalid.");
                TempData["ErrorMessage"] = "Validation failed. Please check your inputs.";
                return View(model);
            }
            Console.WriteLine($"📧 Checking if email already exists: {model.Email}");
            var existingUser = await _userService.GetUserByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(model);
            }
            Console.WriteLine($"🆔 CountryId: {model.CountryId}, StateId: {model.StateId}, CityId: {model.CityId}");
            var countryName = await _context.Country.Where(c => c.Id == model.CountryId).Select(c => c.CountryName).FirstOrDefaultAsync() ?? "";
            Console.WriteLine($"🌍 Resolved Country Name: {countryName}");
            var stateName = await _context.State.Where(s => s.Id == model.StateId).Select(s => s.StateName).FirstOrDefaultAsync() ?? "";
            Console.WriteLine($"🌎 Resolved State Name: {stateName}");
            var cityName = await _context.City.Where(c => c.Id == model.CityId).Select(c => c.CityName).FirstOrDefaultAsync() ?? "";
            Console.WriteLine($"🏙️ Resolved City Name: {cityName}");
            var user = new User
            {
                name = model.FirstName + " " + model.LastName,
                Username = string.IsNullOrWhiteSpace(model.Username)
                ? model.Email.Split('@')[0]  // or any logic you prefer
                : model.Username,
                Email = model.Email,
                password = model.Password,
                password_hash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                user_type = model.UserType,
                status = "Active",
                created_at = DateTime.UtcNow,
                phone_no = model.PhoneNo,
                country = countryName,
                state = stateName,
                city = cityName,
                zip_code = model.ZipCode,
                address = model.Address,
                country_id = model.CountryId,
                state_id = model.StateId,
                city_id = model.CityId,
            };
             // Log user details before saving to DB
            Console.WriteLine("📝 User object created. Attempting to save...");
            Console.WriteLine($"➡️ Saving: {user.name}, {user.country}, {user.state}, {user.city}");
            Console.WriteLine($"Before Save - user_id: {user.user_id}");
            await _userService.CreateUserAsync(user);
            // Log user details after saving to DB (to check if the user ID is created)
            Console.WriteLine($"After Save - user_id: {user.user_id}");
            TempData["EmailSuccessMessage"] = "Email successfully sent to the user!";
            // return RedirectToAction(nameof(Index));
            return RedirectToAction("CreateEmail","User", new { userId = user.user_id });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null) return NotFound();

             // Map Country, State, City names back to IDs (needed for dropdown pre-select)
            if (!string.IsNullOrEmpty(user.country))
            {
            user.country_id = await _userService.GetCountryIdByNameAsync(user.country);
            }
            if (!string.IsNullOrEmpty(user.state))
            {
            user.state_id = await _userService.GetStateIdByNameAsync(user.state);
            }
            if (!string.IsNullOrEmpty(user.city))
            {
            user.city_id = await _userService.GetCityIdByNameAsync(user.city);
            }
            // 🔹 Safely split full name into first and last name
            //Newly Added
            var fullNameParts = (user.name ?? "").Trim().Split(' ', 2);
            var firstName = fullNameParts.Length > 0 ? fullNameParts[0] : "";
            var lastName = fullNameParts.Length > 1 ? fullNameParts[1] : "";
            var viewModel = new EditUserViewModel
            {
                Id = user.user_id,
                // FirstName = user.name.Split(' ').FirstOrDefault() ?? "",
                // LastName = user.name.Split(' ').Skip(1).FirstOrDefault() ?? "",
                FirstName = firstName,
                LastName = lastName,
                Username = user.Username ?? string.Empty, 
                Email = user.Email,
                // Password = user.password ?? "",
                UserType = user.user_type,
                PhoneNo = user.phone_no ?? "",
                Country = user.country ?? "",
                State = user.state ?? "",
                City = user.city ?? "",
                ZipCode = user.zip_code ?? "",
                Address = user.address ?? "" ,
                Status = user.status,
                //Newly Added
                CountryId = user.country_id,
                StateId = user.state_id,
                CityId = user.city_id
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            return View(model);
            await _userService.EditUserAsync(model); 
            return RedirectToAction("Index");  
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Console.WriteLine($"Delete method called with id: {id}"); 
            if (id == 0) return NotFound();

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return View(user); 
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Console.WriteLine($"Delete request received with userId: {id}"); 
            await _userService.DeactivateUserAsync(id);
            // return RedirectToAction("Index", "User");
            Console.WriteLine($"User {id} deactivated successfully."); 
            return Json(new { success = true });
            }

        private async Task<bool> UserExists(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user != null;
        }
        [HttpGet]
        public IActionResult GetCountries()
        {
        Console.WriteLine("🔹 Fetching countries from database...");       
        var countryList = _context.Country
            .OrderBy(c => c.CountryName )
            .Select(c => new { id = c.Id, name = c.CountryName})
            .ToList();

        Console.WriteLine($"✅ Retrieved {countryList.Count} countries from database.");
        return Json(countryList);
        }

        [HttpGet]
        public IActionResult GetStates(int countryId)
        {
        Console.WriteLine($"🔹 Fetching states from database for country ID: {countryId}");
        if (countryId <= 0)
        {
        Console.WriteLine("⚠️ Invalid country ID. Returning empty state list.");
        return Json(new List<string>());
        }
        // using (var context = new DatabaseContext())  // ✅ Replace with your actual DbContext name
        
        var stateList = _context.State
            .Where(s => s.countryId == countryId)
            .OrderBy(s => s.StateName)
            .Select(s => new { id = s.Id, name = s.StateName })
            .ToList();

        Console.WriteLine($"✅ Retrieved {stateList.Count} states for country ID: {countryId}.");
        return Json(stateList);
        }


        [HttpGet]
        public IActionResult GetCities(int stateId)
        {
        Console.WriteLine($"🔹 Fetching cities from database for state ID: {stateId}");
        if (stateId <= 0)
        {
        Console.WriteLine("⚠️ Invalid state ID. Returning empty city list.");
        return Json(new List<string>());
        }
        // using (var context = new DatabaseContext())  // ✅ Replace with your actual DbContext name
        var cityList = _context.City
            .Where(c => c.StateId == stateId)
            .OrderBy(c => c.CityName)
            .Select(c => new { id = c.Id, name = c.CityName })
            .ToList();
        Console.WriteLine($"✅ Retrieved {cityList.Count} cities for state ID: {stateId}.");
        return Json(cityList);
        }
        public IActionResult Role()
        {
        var roles = _context.Roles
        .Select(r => new RoleItemViewModel
        {
            Id = r.Id,
            Name = r.Name
        })
        .ToList();
        var viewModel = new RoleListViewModel
        {
        Roles = roles
        };
        return View(viewModel);
        }

        public IActionResult Permissions(int roleId)
        {
        Console.WriteLine("=== GET Permissions ===");
        Console.WriteLine("RoleId: " + roleId);
        var role = _context.Roles.FirstOrDefault(r => r.Id == roleId);
        if (role == null)
        return NotFound();
        var permissions = _context.Permissions
        .Where(p => p.RoleId == roleId)
        .Select(p => new PermissionItemViewModel
        {
            Id = p.Id,
            Module = p.Module,
            CanView = p.CanView,
            CanAddEdit = p.CanAddEdit,
            CanDelete = p.CanDelete,
            IsSelected = true
        })
        .ToList();
        Console.WriteLine("Permissions fetched: " + permissions.Count);
        foreach (var p in permissions)
        {
        Console.WriteLine($"[GET] PermissionId: {p.Id}, Module: {p.Module}, View: {p.CanView}, Edit: {p.CanAddEdit}, Delete: {p.CanDelete}");
        }
        var viewModel = new EditPermissionsViewModel
        {
        RoleId = roleId,
        RoleName = role.Name,
        Permissions = permissions
        };
        return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Permissions(EditPermissionsViewModel model)
        {
        Console.WriteLine("=== POST Permissions ===");
        Console.WriteLine("RoleId: " + model.RoleId);
        Console.WriteLine("Permissions submitted: " + model.Permissions.Count);
        await _permissionService.UpdatePermissionsAsync(model);
        TempData["Success"] = "Permissions updated successfully!";
        return RedirectToAction("Permissions", new { roleId = model.RoleId });
        }
    }
}
