using Pizza_Shop_.ViewModels;
namespace Pizza.Shop_.Services
{
    public interface IPermissionService
    {
        Task UpdatePermissionsAsync(EditPermissionsViewModel model);
    }
}