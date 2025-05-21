using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Pizza_Shop_.Models;// Assuming a User model exists
using Pizza_Shop_.Data;
using BCrypt.Net;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Pizza_Shop_.Repositories;
using Pizza_Shop_.Services;
using Pizza_Shop_.ViewModels;
// Assuming you have a DB context

namespace Pizza_Shop_.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context; // Database context
        private readonly IUserService _userService;

        public AccountController(DatabaseContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }


        // GET: /Account/Login
        public IActionResult Login()
        {
        if (User.Identity?.IsAuthenticated == true) // ✅ Check if user is already logged in
         {
           return RedirectToAction("Dashboard", "SuperAdmin"); // ✅ Redirect logged-in users
          }
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            // var pashash = BCrypt.Net.BCrypt.HashPassword(Password);
        // Fetch user from the database
        // var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == Email && u.status == "Active" && u.status != "Inactive");
        var user = await _userService.GetActiveUserByEmailAsync(Email);
        if (user == null) // Verify password with hash
        {  
        ViewBag.Error = "Invalid credentials. Try again.";
        return View();
        }
        if (user.password != Password) // Wrong password
        {
        ViewBag.Error = "Invalid credentials. Try again.";
        return View(); // Return to login page with message
        }
        //     if (user.Email == Email && user.password == Password)
        // {
        var claims = new List<Claim>
        {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim(ClaimTypes.Role, user.user_type)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties { IsPersistent = true };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        return RedirectToAction("Dashboard", "SuperAdmin");
        }
        // // else
        //     {
        //         ViewBag.Error = "Invalid credentials. Try again.";
        //         return View();
        //     }
        // }

        //GET: /Account/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // GET: /Account/ForgotPassword
        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
        if (string.IsNullOrEmpty(Email))
        {   
        ViewBag.Error = "Please enter your email.";
        return View("ForgotPassword");
        }

        // ✅ Simulate finding user (DUMMY DATA)
        // var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == Email);
        var user = await _userService.GetUserByEmailAsync(Email);
        if (user == null || user.Email != Email)
        {
        ViewBag.Error = "Email not found!";
        return View("ForgotPassword");
        }
        // ✅ Ensure Email is not null before generating the link
        if (string.IsNullOrEmpty(Email))
        {
        return View("ForgotPassword"); // Redirect if email is empty
        }

        // ✅ Generate Dummy Reset Link
        string token = "dummy-reset-token"; // For now, we use a static token

        // ✅ Ensure the correct action name is used
        string resetLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token = token }, Request.Scheme) ?? "https://yourdomain.com/error";
        // ✅ Generate Dummy Reset Link
        //string token = "dummy-reset-token"; // For now, we use a static token
        //string ? resetLink = Url.Action("ResetPassword", "Account", new { email = Email, token = token }, Request.Scheme);
        //resetLink ??= "https://yourdomain.com/error"; // Default fallback if null
            // ✅ Dummy Email Sending Simulation

         // Simulate sending the email

         var emailSettings = new EmailSettings
        {
        Smtpserver = "smtp.gmail.com",  // Replace with actual value
        Smtpport = 587,                   // Replace with actual port
        SenderEmail = "test@example.com",  // Replace with actual email
        SenderPassword = "password123"  // Replace with actual password
        };
        var smtpClient = new SmtpClient(emailSettings.Smtpserver)
            {
                Port = emailSettings.Smtpport,
                Credentials = new NetworkCredential(emailSettings.SenderEmail, emailSettings.SenderPassword),
                EnableSsl = true
            };

        string emailBody = $@"
        <h3></h3>
        <p>Pizza shop,</p>
        <p>Please <a href='{resetLink}'>click here</a> to reset your account Password.</p>
        <p>If you encounter any issues or have any question, please do not hesitate to contact our support team.</p>
        <p><strong>Important Note:</strong> For security reasons, the link will expire in 24 hours. If u did not request  a password reset, please ignore this email or contact our support team immediatley.</p>
        ";

        // ✅ Simulate Sending Email (Log it or Show it)
        ViewBag.EmailBody = emailBody; // We just display it in View for now.

        return View("ResetpasswordEmail"); // ✅ Load the email confirmation page
        }


        // GET: /Account/ResetPassword
        public IActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login"); // Redirect if invalid access
            }

            ViewBag.Email = email;
            ViewBag.Token = token; 
            return View("ResetPasswordSend");
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        public async Task<IActionResult> ResetPasswordSend([FromForm]string Email, [FromForm]string NewPassword,[FromForm] string ConfirmPassword)
        {
        if (NewPassword != ConfirmPassword)
        {
        ViewBag.Error = "Passwords do not match!";
        return View();
        }
        if (string.IsNullOrEmpty(Email))
        {
        Console.WriteLine("❌ Error: Email is NULL or empty.");
        return View("ResetPasswordSend");
        }
        // var user = await _context.Users
        // .Where(u => u.Email != null && u.Email.ToLower() == Email.ToLower()) // ✅ Prevents null error
        // .FirstOrDefaultAsync();
        var user = await _userService.GetUserByEmailAsync(Email);
        if (user == null)
        {
        Console.WriteLine($"❌ Error: No user found with email {Email}");
        return View("ResetPasswordSend");
        }

        // var user = await _context.Users.Where(u => u.Email.ToLower() == Email.ToLower()).FirstOrDefaultAsync(); // ✅ Fix case sensitivity
        // var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
        Console.WriteLine(user != null ? $"✅ User found: {user.Email}" : "❌ Error: User not found.");
        if (user == null)
        {
        ViewBag.Error = "User not found.";
        return View();
        }

        // Hash the new password before saving to the database
        // string newPassword = NewPassword;
        // string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword); 
        // user.password_hash = NewPassword;
        //needed part
        // user.password = NewPassword; // ✅ Store the new password in 'password' column
        // user.password_hash = Guid.NewGuid().ToString();// ✅ Generate a new password_hash and store it in 'password_hash' column
        // _context.Users.Update(user); // ✅ Ensure Entity Framework tracks the change
        // Console.WriteLine($"📝 Updating password for: {Email}");
        // Console.WriteLine("🔄 Saving changes to the database...");
        await _userService.UpdateUserPasswordAsync(user, NewPassword);
        // // await _context.SaveChangesAsync();
        // Console.WriteLine("✅ Password successfully updated in the database!");

        // Auto-login the user after password reset
        var claims = new List<Claim>
        {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim(ClaimTypes.Role, user.user_type ?? "User") // Assuming you have UserType for roles
        };
        Console.WriteLine($"Updating password for: {Email}");
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        Console.WriteLine("Password reset successful! Redirecting to Dashboard...");
        // return RedirectToAction("Dashboard", "Superadmin");
        return RedirectToAction("Dashboard","SuperAdmin");
        }

        // GET: /Account/ResetPasswordSend (For testing purpose)
        public IActionResult ResetPasswordSend()
        {
            return View("ResetPasswordSend");
        }

        public IActionResult ManageUsers()
        {
        return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> MyProfile()
        {
        Console.WriteLine("🔸 MyProfile called.");
        var userEmail = User.FindFirstValue(ClaimTypes.Name);
        Console.WriteLine($"🔸 Logged in user email: {userEmail}");
        if (string.IsNullOrEmpty(userEmail))
        {
        Console.WriteLine("❌ Session expired or email empty.");
        TempData["ErrorMessage"] = "Session expired. Please log in again.";
        return RedirectToAction("Login"); // Redirect if session expired
        }
            User? user = await _userService.GetLoggedInUserAsync(userEmail);
        if (user == null)
        {
        Console.WriteLine("❌ User profile not found in database.");
        TempData["ErrorMessage"] = "User profile not found.";
        return RedirectToAction("Login");
        }
        //Newly added
        Console.WriteLine($"✅ User profile fetched successfully for user ID: {user.user_id}");
        Console.WriteLine("🔹 Rendering MyProfile View with user data.");
        // Map names back to IDs for dropdowns (only if names are not null)
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
        // Create MyProfileViewModel and pass it to the View
        var profileViewModel = new MyProfileViewModel
        {
        Name = user.name,
        UserType = user.user_type,
        EmailId = user.Email,
        Phone = user.phone_no ?? string.Empty,
        Country = user.country ?? string.Empty,
        State = user.state ?? string.Empty,
        City = user.city ?? string.Empty,
        Address = user.address ?? string.Empty,
        ZipCode = user.zip_code ?? string.Empty,
        CountryId = user.country_id,
        StateId = user.state_id,
        CityId = user.city_id
        };
        return View(profileViewModel); // ✅ Pass user data to MyProfile.cshtml
        }
        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordViewModel();
            return View(model);
            // return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
        Console.WriteLine("🔹 Change Password Method in Controller is Called");
        var userEmail = User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(userEmail))
        {
        Console.WriteLine("❌ Error: No logged-in user found.");
        ViewBag.Error = "User session expired. Please log in again.";
        return RedirectToAction("Login"); // Redirect to login if session is lost
        }
        Console.WriteLine($"🔹 Logged-in user email: {userEmail}");  // ✅ Debugging line
        // var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        var user = await _userService.GetUserByEmailAsync(userEmail);
        Console.WriteLine(user != null ? $"✅ User found: {user.Email}" : "❌ Error: User not found.");
        Console.WriteLine("🔍 Debug: Verifying current password...");
        if (user == null)
        {
        Console.WriteLine("❌ Error: User not found.");
        ViewBag.Error = "User not found.";
        return View();
        }

        // Verify current password
        if (model.CurrentPassword != user.password)
        {
        Console.WriteLine("❌ Error: Current password is incorrect.");
        ViewBag.Error = "Current password is incorrect.";
        return View();
        }

        // Check if new and confirm password match
        if (model.NewPassword != model.ConfirmNewPassword)
        {
        Console.WriteLine("New password and confirm password do not match ");
        ViewBag.Error = "New password and confirm password do not match.";
        return View();
        }

        // Hash the new password
        Console.WriteLine("🔍 Debug: Hashing and updating password...");
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
        user.password_hash = hashedPassword;
        user.password = model.NewPassword; // ✅ Store the new password in plain text (for login)
        // ✅ Redirect to show the pop-up confirmation
        TempData["PasswordChanged"] = "True";
        TempData.Keep("PasswordChanged");
        Console.WriteLine("Password Changed");
        Console.WriteLine($"🔍 Pre-Update Check: User {user.Email}, New Password Hash: {user.password_hash}");
        // _context.Entry(user).Property(x => x.password_hash).IsModified = true;
        // _context.Entry(user).State = EntityState.Modified;
        // Console.WriteLine($"🔄 Entity State: {_context.Entry(user).State} (Should be 'Modified')");
        // // Update the user in the database
        // Console.WriteLine("🔍 Debug: Saving changes to database...");
        // // _context.Users.Update(user);
        // await _context.SaveChangesAsync();
        // Console.WriteLine("Changes made in Database");
        await _userService.UpdateUserPasswordAsync(user, model.NewPassword);
        // 🔍 Fetch user again from database to verify update
        // var updatedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        var updatedUser = await _userService.GetUserByEmailAsync(user.Email);
        if (updatedUser != null)
        {
        Console.WriteLine($"🔍 Post-Update Check: User {updatedUser.Email}, New Password Hash: {updatedUser.password_hash}");
        }
        else
        {
        Console.WriteLine("❌ ERROR: User update not found after SaveChanges!");
        }
        // return RedirectToAction("ChangePassword");
        return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(User model,IFormCollection form)
        {
        Console.WriteLine("🔸 UpdateProfile called.");
        var userEmail = User.FindFirstValue(ClaimTypes.Name);
        Console.WriteLine($"🔸 Logged in user email: {userEmail}");
        if (string.IsNullOrWhiteSpace(userEmail))
        {
        Console.WriteLine("❌ Session expired or email empty.");
        TempData["ErrorMessage"] = "Session expired. Please log in again.";
        return RedirectToAction("Login");
        }
        // if (!ModelState.IsValid)
        // {
        // Console.WriteLine("❌ Model state invalid.");
        // TempData["ErrorMessage"] = "Please fill in all required fields.";
        // // return View("MyProfile", model);
        // return RedirectToAction("MyProfile");
        // }
        if (!ModelState.IsValid)
        {
        foreach (var error in ModelState)
        {
        var key = error.Key;
        foreach (var err in error.Value.Errors)
        {
            Console.WriteLine($"❌ Error for {key}: {err.ErrorMessage}");
        }
        }
        }
        var user = await _userService.GetLoggedInUserAsync(userEmail);
        if (user == null)
        {
        Console.WriteLine("❌ User not found in database.");
        TempData["ErrorMessage"] = "User not found. Please log in again.";
        return RedirectToAction("Login");
        }
        Console.WriteLine($"✅ User found with ID: {user.user_id}");
        // ✅ Update user details
        user.name = model.name;
        user.phone_no = model.phone_no;
        Console.WriteLine($"🔸 Updating Name: {model.name}, Phone: {model.phone_no}");
        // user.country = model.country;
        // Convert country_id (string) to int, then get name
        if (int.TryParse(model.country, out int countryId)) 
        {
        var countryName = await _context.Country
        .Where(c => c.Id == countryId)
        .Select(c => c.CountryName)
        .FirstOrDefaultAsync();
        user.country = countryName ?? "";
        Console.WriteLine($"✅ Country updated to: {user.country}");
        }
        else
        {
        Console.WriteLine("❌ Invalid country ID.");
        user.country = "";
        }

        // user.state = model.state;
        if (int.TryParse(model.state, out int stateId))
        {
        Console.WriteLine($"🔸 Fetching state name for state ID: {stateId}");
        var stateName = await _context.State
        .Where(s => s.Id == stateId)
        .Select(s => s.StateName)
        .FirstOrDefaultAsync();
        user.state = stateName ?? "";
        Console.WriteLine($"✅ State updated to: {user.state}");
        }
        else
        {
        Console.WriteLine("❌ Invalid state ID.");
        user.state = "";
        }
        // user.city = model.city;
        if (int.TryParse(model.city, out int cityId))
        {
        Console.WriteLine($"🔸 Fetching city name for city ID: {cityId}");
        var cityName = await _context.City
        .Where(c => c.Id == cityId)
        .Select(c => c.CityName)
        .FirstOrDefaultAsync();
         user.city = cityName ?? "";
        Console.WriteLine($"✅ City updated to: {user.city}");
        }
        else
        {
        Console.WriteLine("❌ Invalid city ID.");
        user.city = "";
        }
        user.address = model.address;
        user.zip_code = model.zip_code;
        Console.WriteLine($"🔸 Address: {model.address}, Zip Code: {model.zip_code}");
        await _userService.UpdateProfileAsync(user);
        Console.WriteLine("✅ User profile updated successfully in database.");
        TempData["SuccessMessage"] = "Profile updated successfully!";
        return RedirectToAction("MyProfile");
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
    }
}