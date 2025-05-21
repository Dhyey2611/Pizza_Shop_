using Pizza_Shop_.Models;
using Pizza_Shop_.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Pizza_Shop_.ViewModels.AddMenuItemViewModel;
namespace Pizza_Shop_.Services
{
    public interface IMenuItemService
    {
        List<MenuItemViewModel> GetAllMenuItems();
        List<ModifierGroup> GetAllModifierGroups();
        List<Modifier> GetModifiersByGroupId(int groupId);
        void InsertMenuItem(MenuItem item);
        void UpdateMenuItem(MenuItem menuItem);
        MenuItem GetMenuItemById(int id);
        void SoftDeleteMenuItem(int id);
        List<ModifierViewModel.ModifierDetailViewModel> GetAllModifierDetails();
        void InsertMenuItemModifiers(int menuItemId, int modifierId);
        void AddModifierGroup(string name, string description, List<int> modifierIds);
    }
}