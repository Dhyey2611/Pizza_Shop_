using Pizza_Shop_.ViewModels;
using Pizza_Shop_.Repositories;
using Pizza.Shop_.Services;
namespace Pizza_Shop_.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task UpdatePermissionsAsync(EditPermissionsViewModel model)
        {
            await _permissionRepository.UpdatePermissionsAsync(model);
        }
    }
}