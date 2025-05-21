using Pizza_Shop_.ViewModels;
using Pizza_Shop_.Data;
namespace Pizza_Shop_.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly DatabaseContext _context;

        public PermissionRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task UpdatePermissionsAsync(EditPermissionsViewModel model)
        {
        // ✅ Conversion method declared before usage
        bool ConvertOnOffToBool(object value)
        {
        if (value is string str)
        {
        str = str.ToLower();
        return str == "on";
        }
        else if (value is bool b)
        {
        return b;
        }
        return false;
        }
        var existingPermissions = _context.Permissions.Where(p => p.RoleId == model.RoleId).ToList();
        foreach (var submitted in model.Permissions)
        {
        if (!submitted.IsSelected)
            continue;
        var existing = existingPermissions.FirstOrDefault(p => p.Id == submitted.Id);
        if (existing != null)
        {
            existing.CanView = ConvertOnOffToBool(submitted.CanView);
            existing.CanAddEdit = ConvertOnOffToBool(submitted.CanAddEdit);
            existing.CanDelete = ConvertOnOffToBool(submitted.CanDelete);
        }
        }
        await _context.SaveChangesAsync();
        }  
    }
}
