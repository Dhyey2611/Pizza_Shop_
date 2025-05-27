using Microsoft.EntityFrameworkCore;
using Pizza_Shop_.Models; // Ensure this matches your User model namespace

namespace Pizza_Shop_.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ModifierGroup> ModifierGroups { get; set; }
        public DbSet<Modifier> Modifiers { get; set; }
        public DbSet<ModifierGroupItem> ModifierGroupsItem { get; set; }
        public DbSet<MenuItemsModifierGroupMapper> MenuItemsModifierGroupMapper { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Tax_and_fee> Taxes_and_Fees { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>()
                .Property(t => t.Status)
                .HasConversion<int>();
            base.OnModelCreating(modelBuilder);
        }
    }
}