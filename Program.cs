using Microsoft.EntityFrameworkCore;
using Pizza_Shop_.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Pizza_Shop_.Services; 
using Pizza_Shop_.Repositories;
using Pizza_Shop_.Models;
using Pizza.Shop_.Services;
using Pizza_Shop_.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<ITableSectionRepository, TableSectionRepository>();
builder.Services.AddScoped<ITableSectionService, TableSectionService>();
builder.Services.AddScoped<IViewRenderService, ViewRenderService>();

// ✅ Add Authentication & Authorization Here
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; 
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); 
        options.SlidingExpiration = false; 
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "user",
    pattern: "User/{action=Index}/{id?}",
    defaults: new { controller = "User" }
);

app.MapControllerRoute(
    name: "SuperAdmin",
    pattern: "SuperAdmin/{action=Dashboard}/{id?}",
    defaults: new { controller = "SuperAdmin", action = "Dashboard" }
);

app.MapControllerRoute(
    name: "Account",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();