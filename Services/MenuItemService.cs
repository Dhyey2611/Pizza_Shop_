using Pizza_Shop_.Models;
using Pizza_Shop_.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCrypt.Net;
using Pizza_Shop_.Repositories;
using Pizza_Shop_.ViewModels;
namespace Pizza_Shop_.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }
        public List<MenuItemViewModel> GetAllMenuItems()
        {
            var items = _menuItemRepository.GetAllMenuItems();
            return items.Select(m => new MenuItemViewModel
            {
                MenuItemId = m.MenuItemId,
                Name = m.Name,
                ItemTypeIcon = m.ItemType == "veg" ? "~/images/Veg_icon.png" : "~/images/Non-veg.png",
                Rate = m.Price,
                Quantity = m.Quantity,
                Available = m.IsAvailable
            }).ToList();
        }
        public List<ModifierGroup> GetAllModifierGroups()
        {
            return _menuItemRepository.GetAllModifierGroups();
        }
        public List<Modifier> GetModifiersByGroupId(int groupId)
        {
            return _menuItemRepository.GetModifiersByGroupId(groupId);
        }
        public void InsertMenuItem(MenuItem item)
        {
            _menuItemRepository.InsertMenuItem(item);
        }
        public void UpdateMenuItem(MenuItem menuItem)
        {
            _menuItemRepository.UpdateMenuItem(menuItem);
        }
        public MenuItem GetMenuItemById(int id)
        {
            return _menuItemRepository.GetMenuItemById(id);
        }
        public void SoftDeleteMenuItem(int id)
        {
            _menuItemRepository.SoftDeleteMenuItem(id);
        }
        public List<ModifierViewModel.ModifierDetailViewModel> GetAllModifierDetails()
        {
            return _menuItemRepository.GetAllModifierDetails();
        }
        public void InsertMenuItemModifiers(int menuItemId, int modifierId)
        {
            _menuItemRepository.InsertMenuItemModifiers(menuItemId, modifierId);
        }
        public void AddModifierGroup(string name, string description, List<int> modifierIds)
        {
        _menuItemRepository.InsertModifierGroupAndItems(name, description, modifierIds);
        }       
    }
}