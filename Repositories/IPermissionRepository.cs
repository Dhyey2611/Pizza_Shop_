using System.Collections.Generic;
using System.Threading;
using Pizza_Shop_.ViewModels;
namespace Pizza_Shop_.Repositories
{
    public interface IPermissionRepository
    {
    Task UpdatePermissionsAsync(EditPermissionsViewModel model);
    }
}