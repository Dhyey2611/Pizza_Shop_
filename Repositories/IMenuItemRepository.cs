using System.Collections.Generic;
using System.Threading.Tasks;
using Pizza_Shop_.Models;
using Pizza_Shop_.Data;
using Pizza_Shop_.ViewModels;
using static Pizza_Shop_.ViewModels.AddMenuItemViewModel;
namespace Pizza_Shop_.Repositories
{
    public interface IMenuItemRepository
    {
        List<MenuItem> GetAllMenuItems();
        List<ModifierGroup> GetAllModifierGroups();
        List<Modifier> GetModifiersByGroupId(int groupId);
        void InsertMenuItem(MenuItem menuItem);
        void UpdateMenuItem(MenuItem menuItem);
        MenuItem GetMenuItemById(int id);
        void SoftDeleteMenuItem(int id);
        List<ModifierViewModel.ModifierDetailViewModel> GetAllModifierDetails();
        void InsertMenuItemModifiers(int menuItemId, int modifierId);
        void InsertModifierGroupAndItems(string name, string description, List<int> modifierIds);

    }
}