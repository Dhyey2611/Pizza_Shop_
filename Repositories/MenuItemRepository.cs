using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pizza_Shop_.Data;
using Pizza_Shop_.Models;
using Pizza_Shop_.ViewModels;
namespace Pizza_Shop_.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly DatabaseContext _context;
        public MenuItemRepository(DatabaseContext context)
        {
            _context = context;
        }
        public List<MenuItem> GetAllMenuItems()
        {
            return _context.MenuItems.Where(m => m.IsAvailable == true).ToList();
        }
        public List<ModifierGroup> GetAllModifierGroups()
        {
            return _context.ModifierGroups.Where(mg => !mg.IsDeleted).ToList();
        }
        public List<Modifier> GetModifiersByGroupId(int groupId)
        {
            var modifiers = (from m in _context.Modifiers
                             join mgi in _context.ModifierGroupsItem on m.ModifierId equals mgi.ModifierId
                             where mgi.ModifierGroupId == groupId && mgi.IsDeleted == false
                             select new Modifier
                             {
                                 ModifierId = m.ModifierId,
                                 Name = m.Name,
                                 Price = m.Price
                             }).ToList();
            return modifiers;
        }
        public void InsertMenuItem(MenuItem item)
        {
            _context.MenuItems.Add(item);
            _context.SaveChanges();
        }
        public MenuItem GetMenuItemById(int id)
        {
            var menuItem = _context.MenuItems.FirstOrDefault(m => m.MenuItemId == id);
            return menuItem!;
        }
        public void UpdateMenuItem(MenuItem item)
        {
            _context.MenuItems.Update(item);
            _context.SaveChanges();
        }
        public void SoftDeleteMenuItem(int id)
        {
            var item = _context.MenuItems.FirstOrDefault(m => m.MenuItemId == id);
            if (item != null)
            {
                item.IsAvailable = false;
                _context.SaveChanges();
            }
        }
        public List<ModifierViewModel.ModifierDetailViewModel> GetAllModifierDetails()
        {
            var result = (from mgi in _context.ModifierGroupsItem
                          join m in _context.Modifiers
                          on mgi.ModifierId equals m.ModifierId
                          where !mgi.IsDeleted
                          select new ModifierViewModel.ModifierDetailViewModel
                          {
                              Name = mgi.Name,
                              Unit = mgi.Unit,
                              Quantity = mgi.Quantity,
                              Price = m.Price
                          }).ToList();
            return result;
        }
        // public void InsertMenuItemModifiers(int menuItemId, List<ModifierSelectionViewModel> selectedModifiers)
        // {
        //     var mappings = selectedModifiers.Select(m => new MenuItemsModifierGroupMapper
        //     {
        //         ItemId = menuItemId,
        //         // ModifierId = m.ModifierId,
        //         Mean = m.Mean,
        //         Max = m.Max
        //     }).ToList();
        //     _context.MenuItemsModifierGroupMapper.AddRange(mappings);
        //     _context.SaveChanges();
        // }
        public void InsertMenuItemModifiers(int menuItemId, int modifierId)
        {
            var mapping = new MenuItemsModifierGroupMapper
            {
                ItemId = menuItemId,
                ModifierId = modifierId,
            };
            _context.MenuItemsModifierGroupMapper.Add(mapping);
            _context.SaveChanges();
        }
        public void InsertModifierGroupAndItems(string name, string description, List<int> modifierIds)
        {
        var group = new ModifierGroup
        {
        ModifierGroupName = name,
        Description = description,
        IsDeleted = false
        };
        _context.ModifierGroups.Add(group);
        _context.SaveChanges(); 
        var items = modifierIds.Select(modifierId => new ModifierGroupItem
        {
        ModifierGroupId = group.ModifierGroupId,
        ModifierId = modifierId,
        Quantity = 1,
        Unit = group.ModifierGroupName.ToLower().Contains("size") ? "pcs" : "gms",
        IsDeleted = false
        }).ToList();
        _context.ModifierGroupsItem.AddRange(items);
        _context.SaveChanges();
        }
    }
}